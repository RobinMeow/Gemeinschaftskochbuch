using System;

namespace api.Domain;

public sealed record EntityId
{
    static readonly IdentifierSpecification[] _identifierSpecifications = new IdentifierSpecification[] {
        new GuidEntityIdSpecification(),
        new ChefIdSpecification()
    };

    readonly string _id = "00000000-0000-0000-0000-000000000000";

    public string Id { get => _id; }

    /// <exception cref="ArgumentNullException">Thrown when <paramref name="id"/> is null or white space.</exception>
    /// <exception cref="ArgumentException">Thrown when <paramref name="id"/> is not in the correct format or is a disallowed ID.</exception>
    public EntityId(string id)
    {
        bool validId = false;
        for (int i = 0; i < _identifierSpecifications.Length; i++)
        {
            if (_identifierSpecifications[i].IsSatisfiedBy(id))
            {
                validId = true;
                break;
            }
        }

        if (!validId)
        {
            throw new ArgumentException($"Id '{id}' does not satisfy any IdentifierSpecification.");
        }

        _id = id.ToLower();
    }

    /// <summary>Generates a new valid entity ID by generating a new GUID string until a non-disallowed ID is found.</summary>
    /// <returns>A new <see cref="EntityId"/> instance.</returns>
    public static EntityId New()
    {
        string newId;

        do
            newId = Guid.NewGuid().ToString(GuidEntityIdSpecification.GuidFormat).ToLower();
        while (GuidEntityIdSpecification.IsDisallowedId(newId));

        return new EntityId(newId);
    }

    public static implicit operator string(EntityId entityId) => entityId.Id;

    public override string ToString()
    {
        return Id;
    }
}

interface IdentifierSpecification {
    public bool IsSatisfiedBy(string identifier);
}
public sealed class GuidEntityIdSpecification : IdentifierSpecification
{
    /// <summary>The format for representing a GUID as a string.</summary>
    public const string GuidFormat = "D";

    public static readonly string[] DisallowedIds = {
        "00000000-0000-0000-0000-000000000000",
        "ffffffff-ffff-ffff-ffff-ffffffffffff",
        "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"
    };

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

    public bool IsSatisfiedBy(string guid)
    {
        if (string.IsNullOrWhiteSpace(guid))
            return false;
            // throw new ArgumentNullException(nameof(guid), $"{nameof(guid)} cannot be null or white space.");

        if (!IsValidGuidFormat(guid))
            return false;
            // throw new ArgumentException($"{nameof(guid)} must be in the '{GuidFormat}' formatted GUID in all lowercase.");

        if (IsDisallowedId(guid))
            return false;
            // throw new ArgumentException($"{nameof(guid)} cannot be a disallowed GUID.");

        return true;
    }
}
