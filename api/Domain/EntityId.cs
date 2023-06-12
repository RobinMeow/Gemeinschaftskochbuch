namespace api.Domain;

public sealed record EntityId
{
    public EntityId(string id)
    {
        Id = id;
    }

    public string Id { get; set; } = null!;

    public static implicit operator string(EntityId entityId) => entityId.Id;

    public static EntityId New() => new EntityId(System.Guid.NewGuid().ToString().ToLower());
}
