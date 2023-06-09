using System;

namespace api.Domain;

public sealed class Rezept
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime Erstelldatum { get; set; }
}
