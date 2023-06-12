using System;

namespace api.Controllers;

public sealed class RezeptDto : EntityDto
{
    public override int ModelVersion { get; set; } = Domain.Rezept.MODEL_VERSION;
    public required string Name { get; set; }
}
