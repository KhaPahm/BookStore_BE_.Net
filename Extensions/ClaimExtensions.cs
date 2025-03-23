using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Extensions
{
    public static class ClaimExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user) {
            Guid id;

            try {
                id = new Guid(user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")).Value);
            }
            catch {
                id = Guid.Empty;
            }

            return id;
        }

        public static string GetUserRole(this ClaimsPrincipal user) {
            return user.Claims.Single(x => x.Type.Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")).Value;
        }

        public static string GetEmail(this ClaimsPrincipal user) {
            return user.Claims.Single(x => x.Type.Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/emailaddress")).Value;
        }

        public static string GetName(this ClaimsPrincipal user) {
            return user.Claims.Single(x => x.Type.Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/name")).Value;
        }
    }
}