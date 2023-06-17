using api.Domain;

namespace api_tests.entitiyId;

public sealed class implicit_string_conversion
{
    [Fact]
    public void returns_currect_id_string()
    {
        string validId = "12345678-1234-1234-1234-123456789abc";
        EntityId entityId = new EntityId(validId);

        string actual = entityId;

        Assert.Equal(validId, actual);
    }
}
