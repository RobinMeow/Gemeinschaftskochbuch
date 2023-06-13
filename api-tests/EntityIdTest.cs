using api.Domain;

namespace api_tests;

public sealed class EntityIdTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void new_entityId_has_guid_format()
    {
        EntityId entityId = EntityId.New();

        bool has_guid_format = Guid.TryParse(entityId.Id, out Guid guid);

        Assert.True(has_guid_format);
        Assert.That(entityId.Id, Is.EqualTo(guid.ToString()));
    }

    [Test]
    public void new_entityId_string_is_all_lowercase()
    {
        EntityId entityId = EntityId.New();

        bool has_guid_format = Guid.TryParse(entityId.Id, out Guid guid);

        for (int i = 0; i < entityId.Id.Length; i++)
        {
            char c = entityId.Id[i];
            Assert.True(Char.IsUpper(c));
        }
    }

    [Test]
    public void new_entityId_string_equals_parsed_guid_in_string_comparison()
    {
        EntityId entityId = EntityId.New();

        bool has_guid_format = Guid.TryParse(entityId.Id, out Guid guid);

        Assert.That(entityId.Id, Is.EqualTo(guid.ToString()));
    }
}
