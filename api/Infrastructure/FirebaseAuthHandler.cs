
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using api.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace api.Infrastructure;

public sealed class FirebaseAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    readonly IAuthService _firebaseAuthService;

    public FirebaseAuthHandler(
        IOptionsMonitor<JwtBearerOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IAuthService firebaseAuthService
        ) : base(options, logger, encoder, clock)
    {
        _firebaseAuthService = firebaseAuthService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.NoResult();

        string? authorization = Request.Headers["Authorization"];

        if (string.IsNullOrWhiteSpace(authorization))
            return AuthenticateResult.Fail("Invalid token");

        try
        {
            string tokenId = authorization.StartsWith("Bearer ")
                ? authorization.Substring("Bearer ".Length)
                : authorization;

            VerifiedToken verifiedToken = await _firebaseAuthService.VerifyIdTokenAsync(tokenId);
            List<Claim> claimsRequiredForTheApplication = new List<Claim>();

            return AuthenticateResult.Success(new AuthenticationTicket(new ClaimsPrincipal(new List<ClaimsIdentity>(){
                new ClaimsIdentity(claimsRequiredForTheApplication, nameof(FirebaseAuthHandler))
            }), JwtBearerDefaults.AuthenticationScheme));
        }
        catch (Exception ex)
        {
            Logger.LogError($"{ex.Message}\n{ex.StackTrace}");
            return AuthenticateResult.Fail(ex);
        }
    }
}
