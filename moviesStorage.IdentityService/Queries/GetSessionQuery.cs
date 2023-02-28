using Duende.IdentityServer.Models;
using MediatR;
using moviesStorage.IdentityService.Responses;

namespace moviesStorage.IdentityService.Queries;

public class GetSessionQuery : IRequest<UserSessionResponse>
{
    
}