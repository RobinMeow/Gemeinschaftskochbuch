using api.Domain;

namespace api_tests.entitiyId_specifications;

[TestFixture]
public sealed class EntityIdTest
{
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void constructor_throws_ArgumentNullException_on_NullOrWhiteSpace_id(string id)
    {
        Assert.That(() => new EntityId(id), Throws.ArgumentNullException);
    }

    [Test]
    [TestCaseSource(nameof(GetDisallowedIds))]
    public void constructor_throws_ArgumentException_on_disallowed_id(string disallowedId)
    {
        Assert.That(() => new EntityId(disallowedId), Throws.ArgumentException);
    }

    static IEnumerable<string> GetDisallowedIds()
    {
        return EntityId.DisallowedIds;
    }

    [Test]
    [TestCaseSource(nameof(GetInvalidIds))]
    public void constructor_throws_ArgumentException_on_invalid_id(string invalidId)
    {
        Assert.That(() => new EntityId(invalidId), Throws.ArgumentException);
    }

    static IEnumerable<string> GetInvalidIds()
    {
        yield return "invalid";
        yield return "00000000-0000-0000-0000-00000000000G";
        yield return "ZZZZZZZZ-ZZZZ-ZZZZ-ZZZZ-ZZZZZZZZZZZZ";
        yield return "(12345678-1234-1234-1234-123456789abc)";
        yield return "{12345678-1234-1234-1234-123456789abc}";
        yield return "12345678123412341234123456789abc";
    }

    [Test]
    public void constructor_converts_valid_uppercase_id_to_lowercase_silently()
    {
        string validId = "ABCDABCD-1234-ABCD-1234-123456789ABC";

        EntityId entityId = new EntityId(validId);

        Assert.That(entityId.Id, Is.EqualTo(validId.ToLower()));
    }

    [Test]
    [TestCaseSource(nameof(GetAnyIdWithCorrectFormat))]
    public void IsValidGuidFormat_returns_true_for_ids_with_valid_format(string validId)
    {
        bool isValid = EntityId.IsValidGuidFormat(validId);

        Assert.That(isValid, Is.True);
    }

    static IEnumerable<string> GetAnyIdWithCorrectFormat()
    {
        yield return "12345678-1234-1234-1234-123456789abc"; // lowercase (correct and allowed)
        yield return "ABCDABCD-1234-1234-1234-123456789ABC"; // uppercase (correct and allowed)

        IEnumerable<string> disallowedIds = GetDisallowedIds();
        foreach (string disallowedId in disallowedIds)
            yield return disallowedId;
    }

    [TestCaseSource(nameof(GetInvalidIds))]
    public void IsValidGuidFormat_returns_false_for_ids_with_valid_format_but_invalid_hex_digits(string validId)
    {
        bool isValid = EntityId.IsValidGuidFormat(validId);

        Assert.That(isValid, Is.False);
    }

    [Test]
    [TestCaseSource(nameof(GetIdsWithInvalidFormat))]
    public void IsValidGuidFormat_returns_false_for_ids_with_invalid_format(string id)
    {
        bool isValid = EntityId.IsValidGuidFormat(id);

        Assert.That(isValid, Is.False);
    }

    private static IEnumerable<string> GetIdsWithInvalidFormat()
    {
        #pragma warning disable CS8603 // Possible null reference return.
        yield return null;
        #pragma warning restore CS8603 // Possible null reference return.
        yield return "";
        yield return " ";
        yield return "invalid";
        yield return "00000000-0000-0000-0000-00000000000G";
        yield return "00000000000000000000000000000000";
        yield return "000000000000000";
        yield return "(00000000-0000-0000-0000-000000000000)";
        yield return "{00000000-0000-0000-0000-000000000000}";
    }

    [Test]
    public void IsDisallowedId_returns_true_for_disallowed_ids()
    {
        foreach (string disallowedId in EntityId.DisallowedIds)
        {
            bool isDisallowed = EntityId.IsDisallowedId(disallowedId);

            Assert.That(isDisallowed, Is.True);
        }
    }

    [Test]
    public void IsDisallowedId_returns_false_for_a_valid_id()
    {
        string validId = "12345678-1234-1234-1234-123456789abc";

        bool isDisallowed = EntityId.IsDisallowedId(validId);

        Assert.That(isDisallowed, Is.False);
    }

    [Test]
    [Retry(1)]
    public void New_returns_a_valid_id()
    {
        EntityId entityId = EntityId.New();

        Assert.That(entityId.Id, Is.Not.Null.Or.Empty);
        Assert.That(EntityId.IsValidGuidFormat(entityId.Id), Is.True);
        Assert.That(EntityId.IsDisallowedId(entityId.Id), Is.False);
    }

    [Test]
    public void ImplicitConversionToString_returns_id()
    {
        string validId = "12345678-1234-1234-1234-123456789abc";
        EntityId entityId = new EntityId(validId);

        string idString = entityId;

        Assert.That(idString, Is.EqualTo(validId));
    }

    [Test]
    public void ToString_returns_id()
    {
        string validId = "12345678-1234-1234-1234-123456789abc";
        EntityId entityId = new EntityId(validId);

        string idString = entityId.ToString();

        Assert.That(idString, Is.EqualTo(validId));
    }
}
