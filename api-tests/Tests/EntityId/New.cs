using api.Domain;

namespace api_tests.entitiyId;

public sealed class New
{
    [Fact]
    public void returns_a_valid_id()
    {
        EntityId entityId = EntityId.New();

        Assert.NotNull(entityId.Id);
        Assert.False(String.IsNullOrWhiteSpace(entityId.Id));
        Assert.True(GuidEntityIdSpecification.IsValidGuidFormat(entityId.Id));
        Assert.False(GuidEntityIdSpecification.IsDisallowedId(entityId.Id));
    }
}
