using System.Collections.Generic;
using api.Domain;
using api.Infrastructure;
using Microsoft.AspNetCore.Cors;
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
        IEnumerable<Rezept> rezepte = _rezeptRepository.GetAll();
        return rezepte.ToDto();
    }

    [HttpPost(nameof(Add))]
    public ActionResult<RezeptDto> Add([FromBody] RezeptDto rezeptDto)
    {
        if (rezeptDto == null) return BadRequest("rezept may not be null.");

        Rezept rezept = Rezept.Create(rezeptDto);
        _rezeptRepository.Add(rezept);

        return Ok(rezept.ToDto());
    }
}
