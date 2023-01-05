﻿using Microsoft.AspNetCore.Identity;

namespace movieStorage.Identity.Exceptions;

public class RegistrationFailedException : Exception
{
    public readonly IEnumerable<IdentityError> Errors;
    public RegistrationFailedException(IEnumerable<IdentityError> errors)
    {
        Errors = errors;
    }

    public override string Message => $"Registration failed: {Errors}";
}