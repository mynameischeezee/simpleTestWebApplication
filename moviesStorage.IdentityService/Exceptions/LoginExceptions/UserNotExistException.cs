namespace moviesStorage.IdentityService.Exceptions.LoginExceptions;

public class UserNotExistException : Exception
{
    private string Username { get; set; }
        
    public UserNotExistException(string username)
    {
        Username = username;
    }
    public override string Message => $"User: {Username} not exist.";
}