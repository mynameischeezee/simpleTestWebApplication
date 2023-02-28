namespace moviesStorage.IdentityService.Responses;

public class UserSessionResponse : ApiCallResponseBase
{
    public string SubjectId { get; init; } = default!;
    public string SessionId { get; init; } = default!;
    public string DisplayName { get; init; } = default!;
    public DateTime Created { get; init; }
    public DateTime Renewed { get; init; }
    public DateTime? Expires { get; init; }
    public string Issuer { get; init; } = default!;
}