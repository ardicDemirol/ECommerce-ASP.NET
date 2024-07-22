using System.Security.Claims;

namespace ECommerce.Extensions;
public static class ClaimExtensions
{
    public static string GetUserName(this ClaimsPrincipal user)
    {
        return user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
    }
}
