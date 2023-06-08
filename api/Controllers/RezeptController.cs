using System.Collections.Generic;
using api.Domain;
using api.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers;

[ApiController]
[Route("Rezepte")]
public class RezeptController : ControllerBase
{
    readonly ILogger<RezeptController> _logger;
    readonly RezeptRepository _rezeptRepository;

    public RezeptController(ILogger<RezeptController> logger)
    {
        _logger = logger;
        _rezeptRepository = new RezeptRepository();
    }

    [HttpGet(Name = "GetAll")]
    public IEnumerable<RezeptDto> GetAll()
    {
        IEnumerable<Rezept> rezepte = _rezeptRepository.GetAll();
        return rezepte.ToDto();
    }
}
