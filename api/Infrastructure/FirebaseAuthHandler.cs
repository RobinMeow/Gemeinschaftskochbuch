
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
            // https://firebase.google.com/docs/auth/admin/verify-id-tokens
            VerifiedToken verifiedToken = await _firebaseAuthService.VerifyIdTokenAsync(tokenId);

            if (verifiedToken.HasExpired())
                return AuthenticateResult.Fail("Token has expired.");

            List<Claim> claimsRequiredForTheApplication = new List<Claim>(){
                new Claim(AuthClaims.Types.Email, verifiedToken.Claims["email"].ToString()!),
                new Claim(AuthClaims.Types.UserId, verifiedToken.Claims["user_id"].ToString()!),
                new Claim(AuthClaims.Types.EmailVerified, verifiedToken.Claims["email_verified"].ToString()!),
                // ToDo: I think, Authtime should be read out here, once, checked and return auth fail
                // new Claim(AuthTime, verifiedToken.Claims["auth_time"].ToString()!),

            };

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
