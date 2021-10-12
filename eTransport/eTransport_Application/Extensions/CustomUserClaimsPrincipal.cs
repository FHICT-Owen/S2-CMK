using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eTransport_Logic;
using eTransport_Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace eTransport_Application.Extensions
{
    public class CustomUserClaimsPrincipal : UserClaimsPrincipalFactory<ETransportUser>
    {
        public CustomUserClaimsPrincipal(
            UserManager<ETransportUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ETransportUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("IsAdmin", user.IsAdmin.ToString()));
            return identity;
        }
    }
}
