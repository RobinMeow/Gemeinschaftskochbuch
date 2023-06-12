using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class RezeptController : GkbController
{
    readonly ILogger<RezeptController> _logger;
    readonly IRezeptRepository _rezeptRepository;

    public RezeptController(
        ILogger<RezeptController> logger,
        DbContext dbContext
        )
    {
        _logger = logger;
        _rezeptRepository = dbContext.RezeptRepository;
    }

    [HttpPost(nameof(Add))]
    public IActionResult Add([FromBody] RezeptDto rezeptDto)
    {
        try
        {
            if (rezeptDto == null) return BadRequest("rezept may not be null.");

            Rezept rezept = Rezept.Create(rezeptDto);
            _rezeptRepository.AddAsync(rezept);

            return Ok(rezept.ToDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, GetErrorMessage(nameof(RezeptController), nameof(Add)), rezeptDto);
            return Status_500_Internal_Server_Error;
        }
    }

    [HttpGet(nameof(GetAll))]
    public async ValueTask<IActionResult> GetAll()
    {
        try
        {
            IEnumerable<Rezept> rezepte = await _rezeptRepository.GetAllAsync();
            return Ok(rezepte.ToDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, GetErrorMessage(nameof(RezeptController), nameof(GetAll)));
            return Status_500_Internal_Server_Error;
        }
    }
}
