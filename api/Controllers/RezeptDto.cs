using System;

namespace api.Controllers;

public sealed class RezeptDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Erstelldatum { get; set; }
}
