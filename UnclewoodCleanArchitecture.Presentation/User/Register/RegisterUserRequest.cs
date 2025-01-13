
namespace UnclewoodCleanArchitectur.Presentation.User.Register;

public record RegisterUserRequest(

        string Email,
        string FirstName,
        string LastName,
        string Password
);