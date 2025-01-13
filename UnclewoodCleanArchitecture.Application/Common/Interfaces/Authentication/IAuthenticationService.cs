using UnclewoodCleanArchitecture.Domain.User;

namespace UnclewoodCleanArchitecture.Application.Common.Interfaces.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterAsync(
        User user,
        string password,
        CancellationToken cancellationToken = default);
}