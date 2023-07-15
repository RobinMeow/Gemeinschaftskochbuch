using api.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class AuthController : GkbController
{
    [HttpPost(nameof(ChooseChefname))]
    [Authorize]
    public IActionResult ChooseChefname(string chefname)
    {
        // validate username, and check for existing ones.
        // read email and userid from claim
        // write into database

        var authClaims = new AuthClaims(HttpContext.User);

        return Ok(new {
            email = authClaims.Email,
            uid = authClaims.UserId
        });
    }
}
