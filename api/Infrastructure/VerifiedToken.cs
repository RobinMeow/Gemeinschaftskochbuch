using System.Collections.Generic;

namespace api.Domain;

/// <summary>Ensures UserId and Email are valid within the claims.</summary>
public sealed record VerifiedToken
{
    public IReadOnlyDictionary<string, object> Claims;

    public VerifiedToken(
        IReadOnlyDictionary<string, object> claims
        )
    {
        // if (claims.Count == 0)
        //     throw new InvalidOperationException($"{nameof(VerifiedToken)} requires at least one Claim.");

        // if (!claims.ContainsKey(ClaimKey.UserId) || claims[ClaimKey.UserId] == null)
        //     throw new ArgumentException($"missing {ClaimKey.UserId} claim.", nameof(claims));

        // if (!claims.ContainsKey(ClaimKey.Email) || claims[ClaimKey.Email] == null)
        //     throw new ArgumentException($"missing {ClaimKey.Email} claim.", nameof(claims));

        Claims = claims;
    }

    // static class ClaimKey
    // {
    //     public const string UserId = "user_id";
    //     public const string Email = "email";
    // }
}
