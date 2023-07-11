using System.Threading.Tasks;

namespace api.Domain;

public interface IAuthService
{
    /// <summary>ensures that the token is correctly signed, has not expired</summary>
    Task<VerifiedToken> VerifyIdTokenAsync(string idtoken);
}
