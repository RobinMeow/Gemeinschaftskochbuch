using System;

namespace api.Domain;

public abstract class Entity
{
    public EntityId Id { get; set; } = null!;

    public abstract int ModelVersion { get; set; } // start at zero, so the version is also the amount of times, it was changed :)

    DateTime _createdAt = DateTime.MinValue.ToUniversalTime();

    public DateTime CreatedAt
    {
        get => _createdAt;
        set => _createdAt = DateTime.SpecifyKind(value.ToUniversalTime(), DateTimeKind.Utc);
    }
}
