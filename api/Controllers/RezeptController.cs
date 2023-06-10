using System;
using System.Collections.Generic;
using api.Domain;
using api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class RezeptController : ControllerBase
{
    readonly ILogger<RezeptController> _logger;
    readonly RezeptRepository _rezeptRepository;

    public RezeptController(ILogger<RezeptController> logger)
    {
        _logger = logger;
        _rezeptRepository = new RezeptRepository();
    }

    [HttpGet(nameof(GetAll))]
    public IEnumerable<RezeptDto> GetAll()
    {
        try
        {
            IEnumerable<Rezept> rezepte = _rezeptRepository.GetAll();
            return rezepte.ToDto();
        }
        catch (Exception ex)
        {
            throw;
        }
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
            throw; // ToDo: Read About Asp.NET Core Logging. I know there is alot of stuff prebuilt.
        }
    }
}
