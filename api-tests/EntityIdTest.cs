using api.Domain;

namespace api_tests.entitiyId_specifications;

public sealed class EntityIdTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void constructor_throws_ArgumentNullException_on_NullOrWhiteSpace_id(string id)
    {
        Assert.Throws<ArgumentNullException>(() => new EntityId(id));
    }

    [Theory]
    [MemberData(nameof(GetDisallowedIds))]
    public void constructor_throws_ArgumentException_on_disallowed_id(string disallowedId)
    {
        Assert.Throws<ArgumentException>(() => new EntityId(disallowedId));
    }

    public static IEnumerable<object[]> GetDisallowedIds()
    {
        foreach (string disallowedId in EntityId.DisallowedIds)
            yield return new [] { disallowedId };
    }

    [Theory]
    [MemberData(nameof(GetInvalidIds))]
    public void constructor_throws_ArgumentException_on_invalid_id(string invalidId)
    {
        Assert.Throws<ArgumentException>(() => new EntityId(invalidId));
    }

    public static IEnumerable<object[]> GetInvalidIds()
    {
        yield return new [] { "invalid" };
        yield return new [] { "00000000-0000-0000-0000-00000000000G" };
        yield return new [] { "ZZZZZZZZ-ZZZZ-ZZZZ-ZZZZ-ZZZZZZZZZZZZ" };
        yield return new [] { "(12345678-1234-1234-1234-123456789abc)" };
        yield return new [] { "{12345678-1234-1234-1234-123456789abc}" };
        yield return new [] { "12345678123412341234123456789abc" };
    }

    [Fact]
    public void constructor_converts_valid_uppercase_id_to_lowercase_silently()
    {
        string validId = "ABCDABCD-1234-ABCD-1234-123456789ABC";

        EntityId entityId = new EntityId(validId);

        Assert.Equal(validId.ToLower(), entityId.Id);
    }

    [Theory]
    [MemberData(nameof(GetAnyIdWithCorrectFormat))]
    public void IsValidGuidFormat_returns_true_for_ids_with_valid_format(string validId)
    {
        bool isValid = EntityId.IsValidGuidFormat(validId);

        Assert.True(isValid);
    }

    public static IEnumerable<object[]> GetAnyIdWithCorrectFormat()
    {
        yield return new []{ "12345678-1234-1234-1234-123456789abc" }; // lowercase (correct and allowed)
        yield return new []{ "ABCDABCD-1234-1234-1234-123456789ABC" }; // uppercase (correct and allowed)

        IEnumerable<object[]> disallowedIds = GetDisallowedIds();
        foreach (object[] disallowedId in disallowedIds)
            yield return disallowedId;
    }

    [Theory]
    [MemberData(nameof(GetInvalidIds))]
    public void IsValidGuidFormat_returns_false_for_ids_with_valid_format_but_invalid_hex_digits(string validId)
    {
        bool isValid = EntityId.IsValidGuidFormat(validId);

        Assert.False(isValid);
    }

    [Theory]
    [MemberData(nameof(GetIdsWithInvalidFormat))]
    public void IsValidGuidFormat_returns_false_for_ids_with_invalid_format(string id)
    {
        bool isValid = EntityId.IsValidGuidFormat(id);

        Assert.False(isValid);
    }

    public static IEnumerable<object[]> GetIdsWithInvalidFormat()
    {
        object? meow = null;
        #pragma warning disable CS8619 // Possible null reference return.
        yield return new [] { meow };
        #pragma warning restore CS8619 // Possible null reference return.
        yield return new [] { "" };
        yield return new [] { " " };
        yield return new [] { "invalid" };
        yield return new [] { "00000000-0000-0000-0000-00000000000G" };
        yield return new [] { "00000000000000000000000000000000" };
        yield return new [] { "000000000000000" };
        yield return new [] { "(00000000-0000-0000-0000-000000000000)" };
        yield return new [] { "{00000000-0000-0000-0000-000000000000}" };
    }

    [Fact]
    public void IsDisallowedId_returns_true_for_disallowed_ids()
    {
        foreach (string disallowedId in EntityId.DisallowedIds)
        {
            bool isDisallowed = EntityId.IsDisallowedId(disallowedId);

            Assert.True(isDisallowed);
        }
    }

    [Fact]
    public void IsDisallowedId_returns_false_for_a_valid_id()
    {
        string validId = "12345678-1234-1234-1234-123456789abc";

        bool isDisallowed = EntityId.IsDisallowedId(validId);

        Assert.False(isDisallowed);
    }

    [Fact]
    public void New_returns_a_valid_id()
    {
        EntityId entityId = EntityId.New();

        Assert.NotNull(entityId.Id);
        Assert.False(String.IsNullOrWhiteSpace(entityId.Id));
        Assert.True(EntityId.IsValidGuidFormat(entityId.Id));
        Assert.False(EntityId.IsDisallowedId(entityId.Id));
    }

    [Fact]
    public void ImplicitConversionToString_returns_id()
    {
        string validId = "12345678-1234-1234-1234-123456789abc";
        EntityId entityId = new EntityId(validId);

        string actual = entityId;

        Assert.Equal(validId, actual);
    }

    [Fact]
    public void ToString_returns_id()
    {
        string validId = "12345678-1234-1234-1234-123456789abc";
        EntityId entityId = new EntityId(validId);

        string actual = entityId.ToString();

        Assert.Equal(validId, actual);
    }
}
