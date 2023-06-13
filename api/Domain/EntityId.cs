using System;

namespace api.Domain;

public sealed record EntityId
{
    public const string GuidFormat = "D";
    public static readonly string[] DisallowedIds = {
        "00000000-0000-0000-0000-000000000000",
        "ffffffff-ffff-ffff-ffff-ffffffffffff",
        "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"
    };

    string _id = "00000000-0000-0000-0000-000000000000";

    public string Id { get => _id; }

    public EntityId(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentNullException(nameof(id), $"{nameof(Id)} cannot be null or white space.");

        if (!IsValidGuidFormat(id))
            throw new ArgumentException($"{nameof(Id)} must be in the '{GuidFormat}' formatted GUID in all lowercase.");

        if (IsDisallowedId(id))
            throw new ArgumentException($"{nameof(Id)} cannot be a disallowed GUID.");

        _id = id.ToLower();
    }

    public static bool IsValidGuidFormat(string id)
    {
        return Guid.TryParseExact(id, GuidFormat, out _);
    }

    public static bool IsDisallowedId(string id)
    {
        for (int i = 0; i < DisallowedIds.Length; i++)
            if (DisallowedIds[i] == id)
                return true;
        return false;
    }

    public static EntityId New()
    {
        string newId;

        do
            newId = Guid.NewGuid().ToString(GuidFormat).ToLower();
        while (IsDisallowedId(newId));

        return new EntityId(newId);
    }

    public static implicit operator string(EntityId entityId) => entityId.Id;

    public override string ToString()
    {
        return Id;
    }
}
