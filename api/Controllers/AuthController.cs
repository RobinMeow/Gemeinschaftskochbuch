using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class AuthController : GkbController
{
    [HttpPost(nameof(SignUp))]
    [Authorize]
    public IActionResult SignUp([FromBody] string chefname)
    {
        // validate username, and check for existing ones.
        // read email and userid from claim
        // write into database
        return Ok();
    }
}
