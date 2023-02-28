using MediatR;
using moviesStorage.IdentityService.Responses;

namespace moviesStorage.IdentityService.Commands;

public class LoginUserCommand : IRequest<LoginUserResponse>
{
    public readonly string Username;
    public readonly string Password;

    public LoginUserCommand(string username, string password)
    {
        Username = username;
        Password = password;
    }
}