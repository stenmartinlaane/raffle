namespace WebApp.Helpers;

using System;
using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal == null)
        {
            throw new ArgumentNullException(nameof(claimsPrincipal));
        }

        var idClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
        if (idClaim == null)
        {
            throw new InvalidOperationException("User ID claim not found.");
        }

        return Guid.Parse(idClaim.Value);
    }
}
