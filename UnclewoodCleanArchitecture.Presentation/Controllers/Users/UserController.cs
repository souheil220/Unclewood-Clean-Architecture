using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnclewoodCleanArchitectur.Presentation.User.Login;
using UnclewoodCleanArchitectur.Presentation.User.Register;
using UnclewoodCleanArchitecture.Application.Users.Command.Delete;
using UnclewoodCleanArchitecture.Application.Users.Command.Login;
using UnclewoodCleanArchitecture.Application.Users.Command.Register;
using UnclewoodCleanArchitecture.Application.Users.Queries.GetLoggedInUser;
using UnclewoodCleanArchitecture.Application.Users.Queries.ListUsers;
using UnclewoodCleanArchitecture.Domain.Permission;
using UnclewoodCleanArchitecture.Infrastructure.Authorization;
using UserResponse = UnclewoodCleanArchitectur.Presentation.User.UserResponse;

namespace UnclewoodCleanArchitectur.Presentation.Controllers.Users;

public class UserController : BaseApiController
{
    private readonly ISender _mediator;

    public UserController(ISender mediator)
    {
        _mediator = mediator;
    }
    [HasPermission(Permissions.UserAdd)]
    [HttpPost("register")]
    public async Task<ActionResult> CreateUser(RegisterUserRequest request,
        CancellationToken concellationToken)
    {
        var command = new RegisterUserCommand(
            request.Email, request.FirstName, request.LastName,request.Password,request.Role);
        var result = await _mediator.Send(command, concellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken concellationToken)
    {
        var command = new LoginUserCommand(request.Email, request.Password);
        var result = await _mediator.Send(command, concellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(result.Value);
    }

    [HasPermission(Permissions.UserRead)]
    [HttpGet]
    public async Task<IActionResult> GetUsers(CancellationToken concellationToken)
    {
        var command = new ListUsersQuery();
        var result = await _mediator.Send(command, concellationToken);
        List<UserResponse> users = new();
        foreach (var user in result.Value)
        {
            users.Add(new UserResponse(Id: user.Id, 
                Email: user.Email.Value, 
                FirstName: user.FirstName.Value,
                LastName: user.LastName.Value));
        }
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(users);
    }
    
    [HasPermission(Permissions.UserDelete)]
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(Guid guid,CancellationToken concellationToken)
    {
        var command = new DeleteUserCommand(guid);
        var result = await _mediator.Send(command, concellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return NoContent();
    }

    //[Authorize(Roles = $"{Roles.Manager} ,{Roles.Admin}")]
    [HasPermission(Permissions.UserRead)]
    [HttpGet("me")]
    public async Task<IActionResult> GetLoggedInUser(CancellationToken concellationToken)
    {
        var query = new GetLoggedInUserQuery();
        var result = await _mediator.Send(query,concellationToken);
        return Ok(result.Value);
    }
}