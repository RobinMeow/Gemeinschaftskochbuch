namespace api.Domain;

public abstract class Entity
{
    public EntityId Id { get; set; } = null!;

    public abstract int ModelVersion { get; init; } // start at zero, so the version is also the amount of times, it was changed :)

    IsoDateTime _createdAt = new IsoDateTime();

    public IsoDateTime CreatedAt
    {
        get => _createdAt;
        set => _createdAt = value;
    }
}
