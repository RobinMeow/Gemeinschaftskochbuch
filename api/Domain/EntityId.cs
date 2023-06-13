using System;

namespace api.Domain;

public sealed record EntityId
{
    /// <summary>The format for representing a GUID as a string.</summary>
    public const string GuidFormat = "D";

    public static readonly string[] DisallowedIds = {
        "00000000-0000-0000-0000-000000000000",
        "ffffffff-ffff-ffff-ffff-ffffffffffff",
        "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"
    };

    string _id = "00000000-0000-0000-0000-000000000000";

    public string Id { get => _id; }

    /// <exception cref="ArgumentNullException">Thrown when <paramref name="id"/> is null or white space.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="id"/> is not in the correct format or is a disallowed ID.</exception>
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

    /// <summary>Generates a new valid entity ID by generating a new GUID string until a non-disallowed ID is found.</summary>
    /// <returns>A new <see cref="EntityId"/> instance.</returns>
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
