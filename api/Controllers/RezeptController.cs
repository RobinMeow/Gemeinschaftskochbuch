using System;
using System.Collections.Generic;
using api.Domain;
using api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class RezeptController : GkbController
{
    const string ERROR_MESSAGE_PREFIX = "In " + nameof(RezeptController) + " On ";
    readonly ILogger<RezeptController> _logger;
    readonly RezeptRepository _rezeptRepository;

    public RezeptController(ILogger<RezeptController> logger)
    {
        _logger = logger;
        _rezeptRepository = new RezeptRepository();
    }

    [HttpPost(nameof(Add))]
    public IActionResult Add([FromBody] RezeptDto rezeptDto)
    {
        try
        {
            if (rezeptDto == null) return BadRequest("rezept may not be null.");

            Rezept rezept = Rezept.Create(rezeptDto);
            _rezeptRepository.Add(rezept);

            return Ok(rezept.ToDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, GetErrorMessage(nameof(RezeptController), nameof(Add)), rezeptDto);
            return Status_500_Internal_Server_Error;
        }
    }

    [HttpGet(nameof(GetAll))]
    public IActionResult GetAll()
    {
        try
        {
            IEnumerable<Rezept> rezepte = _rezeptRepository.GetAll();
            return Ok(rezepte.ToDto());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, GetErrorMessage(nameof(RezeptController), nameof(GetAll)));
            return Status_500_Internal_Server_Error;
        }
    }
}
