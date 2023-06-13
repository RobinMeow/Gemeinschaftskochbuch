using api.Assertions;

namespace api.Domain;

public sealed record EntityId
{
    public EntityId(string id)
    {
        AssertArgument.ArgumentNotNull(id, nameof(id));
        Assert.True(id.Length == System.Guid.Empty.ToString().Length, $"'{id}' has to be a guid.");

        Id = id;
    }

    public string Id { get; init; } = null!;

    public static implicit operator string(EntityId entityId) => entityId.Id;

    public static EntityId New() => new EntityId(System.Guid.NewGuid().ToString().ToLower());
}
