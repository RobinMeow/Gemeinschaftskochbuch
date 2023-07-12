using api.Domain;

namespace api_tests.entitiyId;

public sealed class IsDisallowedId
{
    [Fact]
    public void returns_true_for_disallowed_ids()
    {
        Parallel.ForEach(EntityId.DisallowedIds, (disallowedId) => {
            Assert.True(EntityId.IsDisallowedId(disallowedId));
        });
    }

    [Fact]
    public void returns_false_for_a_valid_id()
    {
        string validId = "12345678-1234-1234-1234-123456789abc";

        bool isDisallowed = EntityId.IsDisallowedId(validId);

        Assert.False(isDisallowed);
    }
}
