// https://docs.nunit.org/articles/nunit/writing-tests/attributes/culture.html

using System.Globalization;
using api.Infrastructure;

// I wonder if it would have been better, to roll my own DateTime, like I did with EntityId
// ToDo [Semi-Issue]: I actually get in trouble when I need the default DateTime Serialization implementation in MongoDb for something different. Whatever that may be.

namespace api_tests.DateTimeISO8601StringConverter_conversions;

[TestFixture]
public sealed class from___ISO_8601_string___to___UTC_DateTime
{
    [Test]
    [TestCaseSource(nameof(_valid_ISO_8601_strings))]
    public void convert_in_all_cultures(string isoString, DateTime expected, int expectedOffset)
    {
        CultureInfo originalCulture = CultureInfo.CurrentCulture;

        IEnumerable<CultureInfo> allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

        foreach (CultureInfo culture in allCultures)
        {
            CultureInfo.CurrentCulture = culture;

            DateTime actual = DateTimeISO8601StringConverter.Convert(isoString);

            Assert.That(actual,      Is.EqualTo(expected).Within(TimeSpan.FromTicks(expectedOffset)));
            Assert.That(actual.Kind, Is.EqualTo(DateTimeKind.Utc));
        }

        CultureInfo.CurrentCulture = originalCulture;
    }

    /// <summary>Includes a offset (only!) because the DateTime constructor does not support smaller milliseconds than 999. Using magic numbers (ticks) in the ctor instead seemed not readable enough for me to keep the precision.</summary>
    static readonly IEnumerable<TestCaseData> _valid_ISO_8601_strings = new TestCaseData[] {
        new TestCaseData("2023-06-14T12:34:56.1234567Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4567),
        new TestCaseData("2023-06-14T12:34:56.123456Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4560),
        new TestCaseData("2023-06-14T12:34:56.12345Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4500),
        new TestCaseData("2023-06-14T12:34:56.1234Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4000),
        new TestCaseData("2023-06-14T12:34:56.123Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 0),
        new TestCaseData("2023-06-14T12:34:56.12Z", new DateTime(2023, 06, 14, 12, 34, 56, 120, DateTimeKind.Utc), 0),
        new TestCaseData("2023-06-14T12:34:56.1Z", new DateTime(2023, 06, 14, 12, 34, 56, 100, DateTimeKind.Utc), 0),
        new TestCaseData("2023-06-14T12:34:56Z", new DateTime(2023, 06, 14, 12, 34, 56, DateTimeKind.Utc), 0)
    };

    [Test]
    [TestCase("2023-06-14")] // Missing time component
    [TestCase("2023-06-14T12:34:56")] // Missing 'Z' offset
    [TestCase("2023-06-14T12:34:56.123")] // Missing 'Z' offset
    [TestCase("2023/06/14T12:34:56Z")] // Invalid date separator
    [TestCase("2023-06-14T12:34:56Z+05:00")] // Offset not allowed in ISO 8601
    [TestCase("2023-06-14T12:34:56.123456789Z")] // Excessive fractional seconds
    [TestCase("2023-06-14T12:34:56Z+00:00")] // Offset not allowed in ISO 8601
    [TestCase("2023-06-14T12:34:56.123Z-05:00")] // Offset not allowed in ISO 8601
    public void invalid_ISO_string_throws_FormatException(string invalidIsoString)
    {
        Assert.Throws<FormatException>(() => DateTimeISO8601StringConverter.Convert(invalidIsoString));
    }
}

[TestFixture]
public sealed class from___UTC_DateTime___to___ISO_8601_string
{
    [Test]
    [SetCulture("en-US")]
    [TestCaseSource(nameof(_valid_UtcDateTimes))]
    public void Test(DateTime validUtcDate, string expectedIsoString)
    {
        string result = DateTimeISO8601StringConverter.Convert(validUtcDate);
        Assert.That(result, Is.EqualTo(expectedIsoString));
    }

    static readonly IEnumerable<TestCaseData> _valid_UtcDateTimes = new TestCaseData[] {
        new TestCaseData(new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), "2023-06-14T12:34:56.1230000Z"), // Milliseconds: 123
        new TestCaseData(new DateTime(2023, 06, 14, 12, 34, 56, 120, DateTimeKind.Utc), "2023-06-14T12:34:56.1200000Z"), // Milliseconds: 120
        new TestCaseData(new DateTime(2023, 06, 14, 12, 34, 56, 100, DateTimeKind.Utc), "2023-06-14T12:34:56.1000000Z"), // Milliseconds: 100
        new TestCaseData(new DateTime(2023, 06, 14, 12, 34, 56, 002, DateTimeKind.Utc), "2023-06-14T12:34:56.0020000Z"), // Milliseconds: 2
        new TestCaseData(new DateTime(2023, 06, 14, 12, 34, 56, 010, DateTimeKind.Utc), "2023-06-14T12:34:56.0100000Z"), // Milliseconds: 10
        new TestCaseData(new DateTime(2023, 06, 14, 12, 34, 56, 000, DateTimeKind.Utc), "2023-06-14T12:34:56.0000000Z")  // Milliseconds: 0
    };

    [Test]
    [TestCaseSource(nameof(_valid_UtcDateTimes))]
    [SetCulture("zu-ZA")] // Japan
    public void Test_In_Japan(DateTime validUtcDate, string expectedIsoString)
    {
        string result = DateTimeISO8601StringConverter.Convert(validUtcDate);
        Assert.That(result, Is.EqualTo(expectedIsoString));
    }

    [Test]
    [TestCase(DateTimeKind.Local)]
    [TestCase(DateTimeKind.Unspecified)]
    public void invalid_DateTimeKind_throws_InvalidOperationException(DateTimeKind kind)
    {
        DateTime dateTime = new DateTime(2023, 06, 14, 12, 34, 56, kind);
        Assert.That(() => DateTimeISO8601StringConverter.Convert(dateTime), Throws.InvalidOperationException);
    }
}
