using Google.Apis.Auth.OAuth2;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using System.Threading.Tasks;
using System.Collections.Generic;

public sealed class FirebaseAuthService : IAuthService
{
    readonly FirebaseAuth _firebaseAuth;

    public FirebaseAuthService(string path)
    {
        GoogleCredential firebaseCredential = GoogleCredential.FromFile(path);
        GoogleCredential.GetApplicationDefault();
        var firebaseApp = FirebaseApp.Create(new AppOptions {
            Credential = firebaseCredential
        });

        _firebaseAuth = FirebaseAuth.DefaultInstance;
    }

    public async Task<IReadOnlyDictionary<string, object>?> VerifyIdTokenAsync(string idtoken)
    {
        return (await _firebaseAuth.VerifyIdTokenAsync(idtoken))?.Claims;
    }
}
