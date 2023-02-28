using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace moviesStorage.IdentityService;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("MyResource", "My_Resource_DisplayName")
            {
                ApiSecrets = new List<Secret>
                {
                    new Secret("hello".Sha256())
                },
                Scopes = { "RegistrationApi" }
            }
        };
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.Profile(),
            new IdentityResources.OpenId(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(name: "RegistrationApi", displayName: "RegistrationService")
        };
    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client()
            {
                ClientId = "TokenClient",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                RedirectUris = new List<string>(){"https://localhost"},
                AllowOfflineAccess = true,
                AllowedScopes =
                {
                    "RegistrationApi",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess
                }
            }
        };
}