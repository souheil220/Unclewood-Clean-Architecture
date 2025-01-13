using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;

namespace UnclewoodCleanArchitecture.Application.Users.Command.Login;

public record LoginUserCommand( string Email,
    string Password) : ICommand<AccessTokenResponse>;