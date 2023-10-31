using System;
using System.Security.Claims;

namespace api.Domain;

public sealed class AuthClaims
{
    readonly ClaimsPrincipal _claimsPrincipal;

    public AuthClaims(ClaimsPrincipal claimsPrincipal)
    {
        foreach (string type in Types.All)
            if (!claimsPrincipal.HasClaim(claim => claim.Type == type))
                throw new ArgumentException($"Claim Type '{type}' is required.", nameof(claimsPrincipal));

        this._claimsPrincipal = claimsPrincipal;
    }

    public string Email => _claimsPrincipal.FindFirst(claim => claim.Type == Types.Email)!.Value;
    public string UserId => _claimsPrincipal.FindFirst(claim => claim.Type == Types.UserId)!.Value;

    public static class Types
    {
        public const string Email = "email";
        public const string UserId = "user_id";
        public const string EmailVerified = "email_verified";
        public static string[] All = new string[] { Types.Email, Types.EmailVerified, Types.UserId };
    }
}
