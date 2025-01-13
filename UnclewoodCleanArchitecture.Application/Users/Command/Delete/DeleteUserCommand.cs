using UnclewoodCleanArchitecture.Application.Common.Interfaces.Command;
using UnclewoodCleanArchitecture.Application.Users.Command.Login;

namespace UnclewoodCleanArchitecture.Application.Users.Command.Delete;

public record DeleteUserCommand( Guid UserId) : ICommand;