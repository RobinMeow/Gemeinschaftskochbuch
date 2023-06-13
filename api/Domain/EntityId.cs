using System;
using api.Assertions;

namespace api.Domain;

public sealed record EntityId
{
    public EntityId(string id)
    {
        AssertArgument.ArgumentNotNull(id, nameof(id));

        string EMPTY_GUID = Guid.Empty.ToString();
        Assert.False(id == EMPTY_GUID, $"'{id}' may not be an empty guid.");
        Assert.False(id == EMPTY_GUID.Replace("0", "F"), $"'{id}' may not be a full guid.");
        Assert.True(Guid.TryParseExact(id, "D", out _), $"'{id}' has to be a 'D' formatted guid.");

        Id = id.ToLower();
    }

    public string Id { get; init; } = null!;

    public static implicit operator string(EntityId entityId) => entityId.Id;

    public static EntityId New() => new EntityId(Guid.NewGuid().ToString().ToLower());

    public override string ToString()
    {
        return Id;
    }
}
