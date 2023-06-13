using api.Domain;

namespace api_tests.entitiyId_specifications;

// Requirements:
// - id string field has guid format like this 9ec61b44-067a-4703-8039-8f2d85783ca9
// meaning, 8, 4, 4, 4, 12 hex digits seperated by hyphens
// all lower case
// - EntityId instances are equal by value. In EntityId, String and Guid DataType comparison
// - Empty and Full Guids are invalid

public sealed class EntityIdTest
{
    static string EMPTY_GUID = Guid.Empty.ToString(GUID_FORMAT);
    static string FULL_GUID = Guid.Empty.ToString(GUID_FORMAT).Replace("0", "F");
    public const string GUID_FORMAT = "D"; // https://learn.microsoft.com/en-us/dotnet/api/system.guid.tryparseexact?view=net-7.0

    public static string[] _valid_string_guids = {
        "9ec61b44-067a-4703-8039-8f2d85783ca9",
        Guid.NewGuid().ToString(GUID_FORMAT),
        EntityId.New()
    };

    [Test]
    public void is_supported_if_it_is_not_an_empty_guid([ValueSource(nameof(_valid_string_guids))] string id)
    {
        Assert.That(id, Is.Not.EqualTo(EMPTY_GUID));
    }

    [Test]
    public void is_supported_if_it_is_not_a_full_guid([ValueSource(nameof(_valid_string_guids))] string id)
    {
        Assert.That(id, Is.Not.EqualTo(FULL_GUID));
    }

    [Test]
    public void is_supported_if_it_has_guid_format([ValueSource(nameof(_valid_string_guids))] string id)
    {
        Assert.True(Guid.TryParseExact(id, GUID_FORMAT, out _));
    }

    [Test]
    public void is_supported_if_DOT_NET_guid_format_has_a_length_of_36_characters_and_is_seperated_by_hyphens() // maybe should use a regex for this to imrpove readability :)
    {
        string guidString = Guid.Empty.ToString(GUID_FORMAT);

        Assert.That(guidString.Length, Is.EqualTo(36)); // 32 digits + 4 hyphens

        // XXXXXXXX - XXXX -  XXXX -  XXXX -  XXXXXXXXXXXX
        // 01234567 8 9012 13 4567 18 9012 23 456789123456
        Assert.That(guidString[8], Is.EqualTo('-'));
        Assert.That(guidString[13], Is.EqualTo('-'));
        Assert.That(guidString[18], Is.EqualTo('-'));
        Assert.That(guidString[23], Is.EqualTo('-'));

        string hyphenlessGuidString = guidString.Replace("-", "");
        for (int i = 0; i < hyphenlessGuidString.Length; i++)
            Assert.True(Char.IsAsciiHexDigitLower(hyphenlessGuidString[i]), $"'{hyphenlessGuidString[i]}' is not a ascii lower case hex digit.");
    }

    [Test]
    public void is_not_supported_if_it_is_an_empty_guid()
    {
        Assert.Throws<InvalidOperationException>(() => new EntityId(EMPTY_GUID));
    }

    [Test]
    public void is_not_supported_if_it_is_a_full_guid()
    {
        Assert.Throws<InvalidOperationException>(() => new EntityId(FULL_GUID));
    }

    [TestCase("ab0000000-0000-0000-0000-00000000000Z")]
    public void is_not_supported_if_it_has_non_hex_digits(string guidString)
    {
        Assert.Throws<InvalidOperationException>(() => new EntityId(guidString));
    }

    [TestCase("ab000000 0000-0000-0000-000000000000")]
    [TestCase("ab000000000000000000000000000000")]
    [TestCase("ab0000000000-0000-0000-000000000000")]
    [TestCase("ab-0000-0000-0000-000000000000")]
    [TestCase("AB-0000-0000-0000-000000000000")]
    public void is_not_supported_if_it_has_invalid_format(string guidString)
    {
        Assert.Throws<InvalidOperationException>(() => new EntityId(guidString));
    }

    [Test]
    public void is_not_supported_if_empty_string()
    {
        Assert.Throws<ArgumentNullException>(() => new EntityId(""));
    }

    [TestCase("(ab000000-0000-0000-0000-000000000000)")]
    [TestCase("{ab000000-0000-0000-0000-000000000000}")]
    [TestCase("{0x00000011,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}")]
    public void is_not_supported_if_it_has_different_format(string guidString)
    {
        Assert.Throws<InvalidOperationException>(() => new EntityId(guidString));
    }

    [Test]
    public void New_constructs_a_32_hex_digit_lower_case_guid_seperated_by_hyphens()
    {
        EntityId entityId = EntityId.New();
        Assert.That(entityId.ToString(), Is.Not.EqualTo(EMPTY_GUID));
        Assert.True(Guid.TryParseExact(entityId, GUID_FORMAT, out _));
    }

    [TestCase("ABCD0000-0000-0000-0000-000000000000")]
    [TestCase("00000000-0BCD-0000-0000-000000000000")]
    [TestCase("00000000-0000-A0CD-0000-000000000000")]
    [TestCase("00000000-0000-0000-AB0D-000000000000")]
    [TestCase("00000000-0000-0000-0000-00000000ABC0")]
    public void constructor_converts_to_lower_case(string upperCaseId)
    {
        EntityId entityId = new EntityId(upperCaseId);

        char[] nonHyphens = entityId.Id.Where(c => c != '-' && !Char.IsDigit(c)).ToArray();

        for (int i = 0; i < nonHyphens.Length; i++)
            Assert.True(Char.IsLower(nonHyphens[i]), $"'{nonHyphens[i]}' is not a lower case.");
    }

    [Test]
    public void two_New_constructed_IDs_are_not_equal()
    {
        var entityId = EntityId.New();
        Assert.That(entityId, Is.Not.EqualTo(EntityId.New()));
    }


    public static EntityId[] entityIds = {
        EntityId.New(),
        new EntityId(Guid.NewGuid().ToString())
    };

    [Test]
    public void two_instances_with_same_id_are_equal_by_value(
        [ValueSource(nameof(entityIds))] EntityId instance)
    {
        var copy = new EntityId(instance);
        Assert.That(instance, Is.EqualTo(copy));
        Assert.True(instance == copy);
        Assert.True(instance.Equals(copy));
        Assert.True(instance.ToString().Equals(copy.ToString()));
        Assert.True(Guid.Parse(instance.ToString()) == Guid.Parse(copy.ToString()));
        Assert.True(Guid.Parse(instance.ToString()).Equals(Guid.Parse(copy.ToString())));
    }
}
