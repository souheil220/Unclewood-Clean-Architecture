using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnclewoodCleanArchitectur.Presentation.User;
using UnclewoodCleanArchitectur.Presentation.User.Login;
using UnclewoodCleanArchitectur.Presentation.User.Register;
using UnclewoodCleanArchitecture.Application.Users.Command.Delete;
using UnclewoodCleanArchitecture.Application.Users.Command.Login;
using UnclewoodCleanArchitecture.Application.Users.Command.Register;
using UnclewoodCleanArchitecture.Application.Users.Queries.ListUsers;

namespace UnclewoodCleanArchitectur.Presentation.Controllers.Users;

public class UserController : BaseApiController
{
    private readonly ISender _mediator;

    public UserController(ISender mediator)
    {
        _mediator = mediator;
    }
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> CreateUser(RegisterUserRequest request,
        CancellationToken concellationToken)
    {
        var command = new RegisterUserCommand(
            request.Email, request.FirstName, request.LastName,request.Password);
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

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetUsers(CancellationToken concellationToken)
    {
        var command = new ListUsersQuery();
        var result = await _mediator.Send(command, concellationToken);
        List<UserResponse> users = new();
        foreach (var user in result.Value)
        {
            users.Add(new UserResponse(Id: user.Id, Email: user.Email.Value, FirstName: user.FirstName.Value, LastName: user.LastName.Value));
        }
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        return Ok(users);
    }

    [AllowAnonymous]
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
}