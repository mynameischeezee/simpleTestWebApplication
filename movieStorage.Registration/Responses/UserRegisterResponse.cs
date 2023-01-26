namespace movieStorage.Registration.Responses;

public class RegisterUserResponse
{
    public string Id { get; init; }

    public string Username { get; init; } = default!;
    
    public string Email { get; init; } = default!;
}