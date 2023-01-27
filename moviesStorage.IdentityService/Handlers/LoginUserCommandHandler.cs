using MediatR;
using moviesStorage.IdentityService.Commands;
using moviesStorage.IdentityService.Responses;

namespace movieStorage.Registration.Handlers;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    public Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}