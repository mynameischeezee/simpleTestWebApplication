using MediatR;
using moviesStorage.IdentityService.Queries;
using moviesStorage.IdentityService.Responses;

namespace movieStorage.Registration.Handlers;

public class LogoutUserQueryHandler : IRequestHandler<LogoutUserQuery, LogoutUserResponse>
{
    public Task<LogoutUserResponse> Handle(LogoutUserQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}