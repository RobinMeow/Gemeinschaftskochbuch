using System;

namespace api.Domain;

public abstract class Entity
{
    public EntityId Id { get; set; } = null!;

    public abstract int ModelVersion { get; set; } // start at zero, so the version is also the amount of times, it was changed :)

    public DateTime CreatedAt { get; set; }
}
