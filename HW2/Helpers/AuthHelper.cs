using System.Security.Claims;
using System.Text.RegularExpressions;

namespace GaiaShare.Helpers
{
    public static class AuthHelper
    {
        public static string getUserId(ClaimsPrincipal user)
        {
            if( user == null ) 
                return string.Empty; // or null? JVP-May-2022
            return getClaimType(user, ClaimTypes.NameIdentifier);
        }

        public static string getEmailAddress(ClaimsPrincipal user)
        {
            if (user == null)
                return string.Empty; // or null? JVP-May-2022
            return getClaimType(user, ClaimTypes.Email);
        }

        //General function for any claim type
        public static string getClaimType(ClaimsPrincipal user, string claimType)
        {
            if (user.HasClaim(c => c.Type == claimType)) // example: ClaimTypes.NameIdentifier
            {
                var claimsIdentity = user.Identity as ClaimsIdentity;
                if (claimsIdentity != null)
                {
                    // the principal identity is a claims identity.
                    // now we need to find the NameIdentifier claim
                    var userIdClaim = claimsIdentity.Claims
                        .FirstOrDefault(x => x.Type == claimType);
                    //ClaimTypes.NameIdentifier
                    //ClaimTypes.Name
                    //ClaimTypes.Emailaddress

                    if (userIdClaim != null)
                    {
                        var userIdValue = userIdClaim.Value;
                        return userIdClaim.Value;
                    }
                }
            }
            return "";
        }


    }
}
