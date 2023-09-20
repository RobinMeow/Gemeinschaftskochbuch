using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class AuthController : GkbController
{
    readonly IChefRepository _chefRepository;
    readonly ILogger<RecipeController> _logger;

    public AuthController(
        DbContext dbContext,
        ILogger<RecipeController> logger
    )
    {
        _chefRepository = dbContext.ChefRepository;
        _logger = logger;
    }

    [HttpPost(nameof(ChooseChefname))]
    [Authorize]
    public async Task<IActionResult> ChooseChefname(string chefname)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(chefname))
                return BadRequest(new { notifications = new string[] {$"Chefname darf nicht leer sein."} });

            if (chefname.Length < 3)
                return BadRequest(new { notifications = new string[] {$"Chefname muss mind. 3 Zeichen enthalten."} });

            if (chefname.Length > 20)
                return BadRequest(new { notifications = new string[] {$"Chefname darf nicht mehr als 20 Zeichen enthalten."} });

            // validate username, and check for existing ones.
            // read email and userid from claim
            // write into database
            IEnumerable<Chef> chefs = await _chefRepository.GetAllAsync();

            bool nameIsAlreadyTaken = chefs.Any(chef => chef.Name == chefname);

            if (nameIsAlreadyTaken)
                return BadRequest(new { notifications = new string[] {$"Der Name {chefname} ist bereits vergeben."} });


            var authClaims = new AuthClaims(HttpContext.User);

            Chef chef = new Chef(
                chefname,
                new EntityId(authClaims.UserId),
                authClaims.Email
            );

            await _chefRepository.AddAsync(chef);

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, CreateErrorMessage(nameof(AuthController), nameof(ChooseChefname)), chefname);
            return Status_500_Internal_Server_Error;
        }
    }
}
