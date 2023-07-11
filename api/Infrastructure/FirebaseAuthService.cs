using Google.Apis.Auth.OAuth2;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using System.Threading.Tasks;
using api.Domain;

namespace api.Infrastructure;

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

    public async Task<VerifiedToken> VerifyIdTokenAsync(string idtoken)
    {
        // What the VerifyIdTokenAsync ensures: https://firebase.google.com/docs/auth/admin/verify-id-tokens#c
        FirebaseToken firebaseToken = await _firebaseAuth.VerifyIdTokenAsync(idtoken);
        var verifiedToken = new VerifiedToken(firebaseToken.Claims);
        return verifiedToken;
    }
}
