using Duende.IdentityServer.Models;

namespace movieStorage.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(name: "simpleTestWebApplication", displayName: "simpleTestWebApplication.API")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
            { };
}