using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnclewoodCleanArchitectur.Presentation.User.Login;
using UnclewoodCleanArchitectur.Presentation.User.Register;
using UnclewoodCleanArchitecture.Application.Users.Command.Login;
using UnclewoodCleanArchitecture.Application.Users.Command.Register;

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

}