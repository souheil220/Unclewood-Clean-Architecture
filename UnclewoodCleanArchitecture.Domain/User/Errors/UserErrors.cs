﻿using UnclewoodCleanArchitecture.Domain.Common;

namespace UnclewoodCleanArchitecture.Domain.User.Errors;

public static class UserErrors
{
    public static Error NotFound = new(
        "User.Found",
        "The user with the specified identifier was not found");

    public static Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "The provided credentials were invalid");
}