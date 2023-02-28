namespace moviesStorage.Registration.Responses;

public class RegisterUserResponse : ApiCallResponseBase
{
    public string Id { get; init; }

    public string Username { get; init; } = default!;
    
    public string Email { get; init; } = default!;

    public string Status { get; init; } = default!;
}