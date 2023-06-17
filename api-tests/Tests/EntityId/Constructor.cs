using api.Domain;

namespace api_tests.entitiyId;

public sealed class Constructor : _testData
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void throws_ArgumentNullException_on_NullOrWhiteSpace_id(string id)
    {
        Assert.Throws<ArgumentNullException>(() => new EntityId(id));
    }

    [Theory]
    [MemberData(nameof(GetDisallowedIds))]
    public void throws_ArgumentException_on_disallowed_id(string disallowedId)
    {
        Assert.Throws<ArgumentException>(() => new EntityId(disallowedId));
    }

    [Theory]
    [MemberData(nameof(GetInvalidIds))]
    public void throws_ArgumentException_on_invalid_id(string invalidId)
    {
        Assert.Throws<ArgumentException>(() => new EntityId(invalidId));
    }

    [Fact]
    public void converts_valid_uppercase_id_to_lowercase_silently()
    {
        string validId = "ABCDABCD-1234-ABCD-1234-123456789ABC";

        EntityId entityId = new EntityId(validId);

        Assert.Equal(validId.ToLower(), entityId.Id);
    }
}
