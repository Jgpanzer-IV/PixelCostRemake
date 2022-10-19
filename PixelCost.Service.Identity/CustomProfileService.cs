using Duende.IdentityServer.AspNetIdentity;
using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using PixelCost.Service.Identity.Models;
using System.Security.Claims;

namespace PixelCost.Service.Identity
{
    public class CustomProfileService : ProfileService<ApplicationUser>
    {
        public CustomProfileService(UserManager<ApplicationUser> userManager, IUserClaimsPrincipalFactory<ApplicationUser> claimFactory) : base(userManager,claimFactory)
        {

        }

        protected override async Task GetProfileDataAsync(ProfileDataRequestContext profileDataRequest, ApplicationUser applicationUser) {
            
            // Retrieve requested claim from http context
            var principal = await GetUserClaimsAsync(applicationUser);
            
            // Get claim identity from the context
            var identity = (ClaimsIdentity)principal.Identity;

            // Add claim identity here
            identity.AddClaim(new Claim("role", "client"));

            // Add the claim to requested claim type
            profileDataRequest.AddRequestedClaims(principal.Claims);



        }

    }
}
