using Duende.IdentityServer.Models;

namespace moviesStorage.IdentityService;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.Profile(),
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(name: "RegistrationApi", "RegistrationService")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client()
            {
                ClientId = "RegistrationApiClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("H3m)y9ah".Sha256())
                },
                AllowedScopes = {"RegistrationApi", "openId", "profile"}
            }
        };
}