using api.Domain;

namespace api_tests.entitiyId;

public sealed class IsDisallowedId
{
    [Fact]
    public void returns_true_for_disallowed_ids()
    {
        foreach (string disallowedId in EntityId.DisallowedIds)
        {
            bool isDisallowed = EntityId.IsDisallowedId(disallowedId);

            Assert.True(isDisallowed);
        }
    }

    [Fact]
    public void returns_false_for_a_valid_id()
    {
        string validId = "12345678-1234-1234-1234-123456789abc";

        bool isDisallowed = EntityId.IsDisallowedId(validId);

        Assert.False(isDisallowed);
    }
}
