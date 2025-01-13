using UnclewoodCleanArchitectur.Presentation.DTOs;

namespace UnclewoodCleanArchitectur.Presentation.User;

public record UserResponse(Guid Id, string Email, string FirstName, string LastName);