
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace api.Infrastructure;

public sealed class FirebaseAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    readonly IAuthService _firebaseAuthService;

    public FirebaseAuthHandler(IOptionsMonitor<JwtBearerOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, IAuthService firebaseAuthService)
        : base(options, logger, encoder, clock)
    {
        _firebaseAuthService = firebaseAuthService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.NoResult();

        string? authorization = Request.Headers["Authorization"];

        if (string.IsNullOrWhiteSpace(authorization)) // Bearer eyGibberisch...
            return AuthenticateResult.Fail("Invalid token");

        try
        {
            string tokenId = authorization.StartsWith("Bearer ") ? authorization.Substring("Bearer ".Length) : authorization;

            IReadOnlyDictionary<string, object>? claims = await _firebaseAuthService.VerifyIdTokenAsync(tokenId);

            if (claims == null)
                return AuthenticateResult.Fail("Invalid idtoken");

            return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new List<ClaimsIdentity>(){
                new ClaimsIdentity(ToClaims(claims), nameof(FirebaseAuthHandler))
            }), JwtBearerDefaults.AuthenticationScheme));
        }
        catch (System.Exception ex)
        {
            return AuthenticateResult.Fail(ex);
        }
    }

    IEnumerable<Claim>? ToClaims(IReadOnlyDictionary<string, object> claims)
    {
        return new List<Claim>(){
            new Claim("id", claims["user_id"].ToString()!),
            new Claim("email", claims["email"].ToString()!),
        };
    }
}
