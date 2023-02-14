using Microsoft.AspNetCore.Identity;

namespace moviesStorage.IdentityService.Exceptions.LoginExceptions;

public class WrongCredentialsException : Exception
{
    public override string Message => "Invalid username or password.";
}