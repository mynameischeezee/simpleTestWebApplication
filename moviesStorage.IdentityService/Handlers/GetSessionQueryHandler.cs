using AutoMapper;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using MediatR;
using moviesStorage.IdentityService.Queries;
using moviesStorage.IdentityService.Responses;

namespace moviesStorage.IdentityService.Handlers;
 
public class GetSessionQueryHandler : IRequestHandler<GetSessionQuery, UserSessionResponse>
{
    private readonly ISessionManagementService _sessionManagementService;
    private readonly IMapper _mapper;

    public GetSessionQueryHandler(ISessionManagementService sessionManagementService, IMapper mapper)
    {
        _sessionManagementService = sessionManagementService;
        _mapper = mapper;
    }

    public async Task<UserSessionResponse> Handle(GetSessionQuery request, CancellationToken cancellationToken)
    {
        var sessions = await _sessionManagementService.QuerySessionsAsync(cancellationToken: cancellationToken);
        var result = sessions.Results.IsNullOrEmpty()
            ? new UserSessionResponse()
            : _mapper.Map<UserSessionResponse>(sessions.Results.LastOrDefault());
        return result;
    }
}