using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace PixelCost.Service.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("role",new [] {"role"})
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope(){ 
                Name = "create",
                DisplayName = "CreateAPI",
                Description = "Authority that will able to create entity in API services."
            },
            new ApiScope(){ 
                Name = "retrieve",
                DisplayName = "RetrieveAPI",
                Description = "Authority that will able to retrieve entity from API services."
            },
            new ApiScope(){
                Name = "update",
                DisplayName = "UpdateAPI",
                Description = "Authority that will able to update entity in API services."
            },
            new ApiScope(){
                Name = "delete",
                DisplayName = "DeleteAPI",
                Description = "Authority that will able to delete entity in API services."
            },
            new ApiScope(){
                Name = "admin",
                DisplayName = "AdminAPI",
                Description = "Authority that will able to perform all operation to API services."
            }
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "admin" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive.client",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:7296/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:7296/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7296/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "create", "retrieve", "update", "delete"
                }
            },


            new Client{

                ClientId = "interactive.admin",
                ClientSecrets = { new Secret("EOKFSS-304-2344?FLREODAWOF".Sha256())},

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = {""},
                FrontChannelLogoutUri = "",
                PostLogoutRedirectUris = {""},

                AllowOfflineAccess = true,
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "admin"
                }
            }

        };
}
