// https://docs.nunit.org/articles/nunit/writing-tests/attributes/culture.html

using api.Infrastructure;

namespace api_tests.entityIdSerializer_specifications;

[TestFixture]
public sealed class DateTimeISO8601StringConverterTests
{
    [Test]
    [TestCaseSource(nameof(GetValidIsoStrings))]
    [SetCulture("en-US")]
    public void Convert_returns_correctly_converted_UTcDateTime_from_valid_IsoString(string isoString, DateTime expectedDateTime, int expectedOffset)
    {
        DateTime correctlyConvertedUtcDateTime = DateTimeISO8601StringConverter.Convert(isoString);
        Assert.That(correctlyConvertedUtcDateTime, Is.EqualTo(expectedDateTime).Within(TimeSpan.FromTicks(expectedOffset)));
        Assert.That(correctlyConvertedUtcDateTime.Kind, Is.EqualTo(expectedDateTime.Kind));
    }

    [Test]
    [TestCaseSource(nameof(GetValidIsoStrings))]
    [SetCulture("zu-ZA")] // South Africa
    public void Convert_ValidIsoString_ReturnsDateTime_zu_ZA(string isoString, DateTime expectedDateTime, int expectedOffset)
        => Convert_returns_correctly_converted_UTcDateTime_from_valid_IsoString(isoString, expectedDateTime, expectedOffset);

    [Test]
    [TestCaseSource(nameof(GetValidIsoStrings))]
    [SetCulture("ja-JP")] // Japan
    public void Convert_ValidIsoString_ReturnsDateTime_ja_JP(string isoString, DateTime expectedDateTime, int expectedOffset)
        => Convert_returns_correctly_converted_UTcDateTime_from_valid_IsoString(isoString, expectedDateTime, expectedOffset);

    /// <summary>Includes a offset, only, because the DateTime constructor does not support smaller milliseconds than 999. Using magic numbers (ticks) in de ctor instead seemed not readable enough for me to keep the precision.</summary>
    static IEnumerable<TestCaseData> GetValidIsoStrings()
    {
        yield return new TestCaseData("2023-06-14T12:34:56.1234567Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4567);
        yield return new TestCaseData("2023-06-14T12:34:56.123456Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4560);
        yield return new TestCaseData("2023-06-14T12:34:56.12345Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4500);
        yield return new TestCaseData("2023-06-14T12:34:56.1234Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4000);
        yield return new TestCaseData("2023-06-14T12:34:56.123Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 0);
        yield return new TestCaseData("2023-06-14T12:34:56.12Z", new DateTime(2023, 06, 14, 12, 34, 56, 120, DateTimeKind.Utc), 0);
        yield return new TestCaseData("2023-06-14T12:34:56.1Z", new DateTime(2023, 06, 14, 12, 34, 56, 100, DateTimeKind.Utc), 0);
        yield return new TestCaseData("2023-06-14T12:34:56Z", new DateTime(2023, 06, 14, 12, 34, 56, DateTimeKind.Utc), 0);
    }

    [Test]
    [TestCaseSource(nameof(GetValidDateTimeValues))]
    public void Convert_ValidDateTime_ReturnsIsoString(DateTime dateTime, string expectedIsoString)
    {
        string result = DateTimeISO8601StringConverter.Convert(dateTime);
        Assert.That(result, Is.EqualTo(expectedIsoString));
    }

    [Test]
    [TestCaseSource(nameof(GetValidDateTimeValues))]
    [SetCulture("zu-ZA")] // I have no idea where this is. Seems like Japan?
    public void Convert_ValidDateTime_ReturnsIsoString_zu_ZA(DateTime dateTime, string expectedIsoString)
    {
        string result = DateTimeISO8601StringConverter.Convert(dateTime);
        Assert.That(result, Is.EqualTo(expectedIsoString));
    }

    static IEnumerable<TestCaseData> GetValidDateTimeValues()
    {
        yield return new TestCaseData(new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), "2023-06-14T12:34:56.1230000Z");
        yield return new TestCaseData(new DateTime(2023, 06, 14, 12, 34, 56, 120, DateTimeKind.Utc), "2023-06-14T12:34:56.1200000Z");
        yield return new TestCaseData(new DateTime(2023, 06, 14, 12, 34, 56, 100, DateTimeKind.Utc), "2023-06-14T12:34:56.1000000Z");
        yield return new TestCaseData(new DateTime(2023, 06, 14, 12, 34, 56, 002, DateTimeKind.Utc), "2023-06-14T12:34:56.0020000Z");
        yield return new TestCaseData(new DateTime(2023, 06, 14, 12, 34, 56, 010, DateTimeKind.Utc), "2023-06-14T12:34:56.0100000Z");
        yield return new TestCaseData(new DateTime(2023, 06, 14, 12, 34, 56, 000, DateTimeKind.Utc), "2023-06-14T12:34:56.0000000Z");
    }

    [Test]
    [TestCaseSource(nameof(GetInvalidIsoStrings))]
    public void Convert_InvalidIsoString_ThrowsFormatException(string invalidIsoString)
    {
        Assert.Throws<FormatException>(() => DateTimeISO8601StringConverter.Convert(invalidIsoString));
    }

    static IEnumerable<string> GetInvalidIsoStrings()
    {
        yield return "2023-06-14"; // Missing time component
        yield return "2023-06-14T12:34:56"; // Missing 'Z' offset
        yield return "2023-06-14T12:34:56.123"; // Missing 'Z' offset
        yield return "2023/06/14T12:34:56Z"; // Invalid date separator
        yield return "2023-06-14T12:34:56Z+05:00"; // Offset not allowed in ISO 8601
        yield return "2023-06-14T12:34:56.123456789Z"; // Excessive fractional seconds
        yield return "2023-06-14T12:34:56Z+00:00"; // Offset not allowed in ISO 8601
        yield return "2023-06-14T12:34:56.123Z-05:00"; // Offset not allowed in ISO 8601
    }

    [Test]
    public void Convert_InvalidDateTimeKind_ThrowsInvalidOperationException(
        [Values(DateTimeKind.Local, DateTimeKind.Unspecified)] DateTimeKind kind)
    {
        DateTime dateTime = new DateTime(2023, 06, 14, 12, 34, 56, kind);
        Assert.That(() => DateTimeISO8601StringConverter.Convert(dateTime),
            Throws.InvalidOperationException);
    }
}
