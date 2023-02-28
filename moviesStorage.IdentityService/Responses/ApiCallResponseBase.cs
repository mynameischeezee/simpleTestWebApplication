using System.Net;

namespace moviesStorage.IdentityService.Responses;

public class ApiCallResponseBase
{
    public string Title { get; init; } = default!;

    public string Description { get; init; } = default!;
    
    public HttpStatusCode StatusCode { get; init; } = default!;

}