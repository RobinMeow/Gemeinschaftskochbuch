using System.Globalization;
using api.Domain;

namespace api_tests.IsoDateTime_conversion_tests;

// ISO 8601 is the international standard for formatting dates and times. It uses the 24 hour timekeeping system and defaults to UTC (Coordinated Universal Time)
// this means, it should not contain offsets in strings (not even zero!) and should also be in Utc

[TestFixture]
public sealed class constrcutor
{
    [Test]
    [TestCaseSource(nameof(_valid_ISO_8601_strings))]
    public void Constructor_converts_ISO_string_to_DateTime(string isoString, DateTime expected, int expectedOffset)
    {
        IsoDateTime isoDateTime = new IsoDateTime(isoString);
        DateTime actual = (DateTime)isoDateTime;

        Assert.That(actual, Is.EqualTo(expected).Within(TimeSpan.FromTicks(expectedOffset)));
    }

    /// <summary>Includes a offset (only!) because the DateTime constructor does not support smaller milliseconds than 999. Using magic numbers (ticks) in the ctor instead seemed not readable enough for me to keep the precision.</summary>
    static readonly IEnumerable<TestCaseData> _valid_ISO_8601_strings = new TestCaseData[] {
        new TestCaseData("9999-12-31T23:59:59.9999999Z", new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Utc), 9999),
        new TestCaseData("2023-06-14T12:34:56.1234567Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4567),
        new TestCaseData("2023-06-14T12:34:56.123456Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4560),
        new TestCaseData("2023-06-14T12:34:56.12345Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4500),
        new TestCaseData("2023-06-14T12:34:56.1234Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4000),
        new TestCaseData("2023-06-14T12:34:56.123Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 0),
        new TestCaseData("2023-06-14T12:34:56.12Z", new DateTime(2023, 06, 14, 12, 34, 56, 120, DateTimeKind.Utc), 0),
        new TestCaseData("2023-06-14T12:34:56.1Z", new DateTime(2023, 06, 14, 12, 34, 56, 100, DateTimeKind.Utc), 0),
        new TestCaseData("2023-06-14T12:34:56Z", new DateTime(2023, 06, 14, 12, 34, 56, DateTimeKind.Utc), 0),
        new TestCaseData("0001-01-01T00:00:00Z", new DateTime(1, 01, 01, 00, 00, 00, 0, DateTimeKind.Utc), 0)
    };

    [Test]
    [TestCase("2023-06-14T12:34:56.1234567Z")]
    [TestCase("2023-06-14T12:34:56Z")]
    public void Constructor_converts_ISO_string_to_DateTime_UTC(string isoString)
    {
        IsoDateTime isoDateTime = new IsoDateTime(isoString);
        DateTime actual = (DateTime)isoDateTime;

        Assert.That(actual.Kind, Is.EqualTo(DateTimeKind.Utc));
    }

    /// <summary>https://docs.nunit.org/articles/nunit/writing-tests/attributes/culture.html</summary>
    [Test]
    [TestCaseSource(nameof(_valid_ISO_8601_strings))]
    public void Constructor_converts_ISO_string_to_DateTime_UTC_CultureIndependend(string isoString, DateTime expected, int expectedOffset)
    {
        CultureInfo originalCulture = CultureInfo.CurrentCulture;

        foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
        {
            CultureInfo.CurrentCulture = culture;

            IsoDateTime isoDateTime = new IsoDateTime(isoString);
            DateTime actual = (DateTime)isoDateTime;

            Assert.That(actual, Is.EqualTo(expected).Within(TimeSpan.FromTicks(expectedOffset)));
            Assert.That(actual.Kind, Is.EqualTo(DateTimeKind.Utc));
        }

        CultureInfo.CurrentCulture = originalCulture;
    }

    [Test]
    [TestCase("2023-06-14T12:34:56.1234567Z")]
    [TestCase("2023-06-14T12:34:56.1234560Z")]
    [TestCase("2023-06-14T12:34:56.1234500Z")]
    [TestCase("2023-06-14T12:34:56.1234000Z")]
    [TestCase("2023-06-14T12:34:56.1230000Z")]
    [TestCase("2023-06-14T12:34:56.1200000Z")]
    [TestCase("2023-06-14T12:34:56.1000000Z")]
    [TestCase("2023-06-14T12:34:56.0000000Z")]
    public void ToString_returns_valid_iso_string(string iso)
    {
        string actual = new IsoDateTime(iso).ToString();
        Assert.That(actual, Is.EqualTo(iso));
    }

    [Test]
    [TestCase(null)]
    public void Constructor_throws_ArgumentNullException_on_null(string invalidIso)
    {
        Assert.That(() => new IsoDateTime(invalidIso), Throws.ArgumentNullException);
    }

    [Test]
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("Robin was here")]
    [TestCase("2023-06-14")] // Missing time component
    [TestCase("2023-06-14T12:34:56")] // Missing 'Z' offset
    [TestCase("2023-06-14T12:34:56.123")] // Missing 'Z' offset
    [TestCase("2023/06/14T12:34:56Z")] // Invalid date separator
    [TestCase("2023-06-14T12:34:56Z+05:00")] // Offset not allowed in ISO 8601
    [TestCase("2023-06-14T12:34:56.123456789Z")] // Excessive fractional seconds
    [TestCase("2023-06-14T12:34:56Z+00:00")] // Offset not allowed in ISO 8601
    [TestCase("2023-06-14T12:34:56.123Z-05:00")] // Offset not allowed in ISO 8601
    public void Constructor_throws_FormatException_on_invalid_iso_string(string invalidIso)
    {
        Assert.Throws<FormatException>(() => new IsoDateTime(invalidIso));
    }
}