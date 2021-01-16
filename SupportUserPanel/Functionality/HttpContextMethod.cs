using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SupportUserPanel.Functionality
{
    public static class HttpContextMethod
    {
        public static int GetCurrentUserId(ClaimsPrincipal user)
        {
            var currentUserId = 0;
            var claimsIdentity = user.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                var userIdClaim = claimsIdentity.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                    currentUserId = Convert.ToInt32(userIdClaim.Value);
                return currentUserId;
            }
            return currentUserId;
        }
    }
}
