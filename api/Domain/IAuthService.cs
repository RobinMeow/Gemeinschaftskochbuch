using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAuthService
{
    Task<IReadOnlyDictionary<string, object>?> VerifyIdTokenAsync(string idtoken);
}
