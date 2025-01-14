using Microsoft.AspNetCore.Authorization;

namespace UnclewoodCleanArchitecture.Infrastructure.Authorization;

public sealed class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission);