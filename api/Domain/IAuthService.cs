using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;

public interface IAuthService
{
    Task<IReadOnlyDictionary<string, object>?> VerifyIdTokenAsync(string idtoken);
}
