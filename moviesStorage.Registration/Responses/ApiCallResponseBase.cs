using System.Net;

namespace moviesStorage.Registration.Responses;

public class ApiCallResponseBase
{
    public string Title;

    public string Description;
    
    public HttpStatusCode StatusCode;

}