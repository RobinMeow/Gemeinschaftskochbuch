using System.Globalization;
using api.Domain;

namespace api_tests.IsoDateTime_conversion_tests;

// ISO 8601 is the international standard for formatting dates and times. It uses the 24 hour timekeeping system and defaults to UTC (Coordinated Universal Time)
// this means, it should not contain offsets in strings (not even zero!) and should also be in Utc

public sealed class Constructor
{
    [Theory]
    [MemberData(nameof(Get_valid_ISO_8601_strings))]
    public void Constructor_converts_ISO_string_to_DateTime(string isoString, DateTime expected, int expectedFaultTolerance)
    {
        IsoDateTime isoDateTime = new IsoDateTime(isoString);
        DateTime actual = (DateTime)isoDateTime;

        Assert.Equal(expected, actual, TimeSpan.FromTicks(expectedFaultTolerance));
    }

    /// <summary>Includes a offset (only!) because the DateTime constructor does not support smaller milliseconds than 999. Using magic numbers (ticks) in the ctor instead seemed not readable enough for me to keep the precision.</summary>
    public static IEnumerable<object[]> Get_valid_ISO_8601_strings()
    {
        yield return new object[] { "9999-12-31T23:59:59.9999999Z", new DateTime(9999, 12, 31, 23, 59, 59, 999, DateTimeKind.Utc), 9999 };
        yield return new object[] { "2023-06-14T12:34:56.1234567Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4567 };
        yield return new object[] { "2023-06-14T12:34:56.123456Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4560 };
        yield return new object[] { "2023-06-14T12:34:56.12345Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4500 };
        yield return new object[] { "2023-06-14T12:34:56.1234Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 4000 };
        yield return new object[] { "2023-06-14T12:34:56.123Z", new DateTime(2023, 06, 14, 12, 34, 56, 123, DateTimeKind.Utc), 0 };
        yield return new object[] { "2023-06-14T12:34:56.12Z", new DateTime(2023, 06, 14, 12, 34, 56, 120, DateTimeKind.Utc), 0 };
        yield return new object[] { "2023-06-14T12:34:56.1Z", new DateTime(2023, 06, 14, 12, 34, 56, 100, DateTimeKind.Utc), 0 };
        yield return new object[] { "2023-06-14T12:34:56Z", new DateTime(2023, 06, 14, 12, 34, 56, DateTimeKind.Utc), 0 };
        yield return new object[] { "0001-01-01T00:00:00Z", new DateTime(1, 01, 01, 00, 00, 00, 0, DateTimeKind.Utc), 0 };
    }

    [Theory]
    [InlineData("2023-06-14T12:34:56.1234567Z")]
    [InlineData("2023-06-14T12:34:56Z")]
    public void Constructor_converts_ISO_string_to_DateTime_UTC(string isoString)
    {
        IsoDateTime isoDateTime = new IsoDateTime(isoString);
        DateTime actual = (DateTime)isoDateTime;

        Assert.Equal(DateTimeKind.Utc, actual.Kind);
    }

    /// <summary>https://docs.nunit.org/articles/nunit/writing-tests/attributes/culture.html</summary>
    [Theory]
    [MemberData(nameof(Get_valid_ISO_8601_strings))]
    public void Constructor_converts_ISO_string_to_DateTime_UTC_CultureIndependend(string isoString, DateTime expected, int expectedFaultTolerance)
    {
        CultureInfo originalCulture = CultureInfo.CurrentCulture;

        foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
        {
            CultureInfo.CurrentCulture = culture;

            IsoDateTime isoDateTime = new IsoDateTime(isoString);
            DateTime actual = (DateTime)isoDateTime;

            Assert.Equal(expected, actual, TimeSpan.FromTicks(expectedFaultTolerance));
            Assert.Equal(DateTimeKind.Utc, actual.Kind);
        }

        CultureInfo.CurrentCulture = originalCulture;
    }

    [Theory]
    [InlineData(null)]
    public void Constructor_throws_ArgumentNullException_on_null(string invalidIso)
    {
        Assert.Throws<ArgumentNullException>(() => new IsoDateTime(invalidIso));
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("Robin was here")]
    [InlineData("2023-06-14")] // Missing time component
    [InlineData("2023-06-14T12:34:56")] // Missing 'Z' offset
    [InlineData("2023-06-14T12:34:56.123")] // Missing 'Z' offset
    [InlineData("2023/06/14T12:34:56Z")] // Invalid date separator
    [InlineData("2023-06-14T12:34:56Z+05:00")] // Offset not allowed in ISO 8601
    [InlineData("2023-06-14T12:34:56.123456789Z")] // Excessive fractional seconds
    [InlineData("2023-06-14T12:34:56Z+00:00")] // Offset not allowed in ISO 8601
    [InlineData("2023-06-14T12:34:56.123Z-05:00")] // Offset not allowed in ISO 8601
    public void Constructor_throws_FormatException_on_invalid_iso_string(string invalidIso)
    {
        Assert.Throws<FormatException>(() => new IsoDateTime(invalidIso));
    }

    [Theory]
    [MemberData(nameof(Get_invalid_DateTimeKinds))]
    public void Constructor_throws_ArgumentException_on_invalid_DateTimeKind(DateTime invalidDate)
    {
        Assert.Throws<ArgumentException>(() => new IsoDateTime(invalidDate));
    }

    public static IEnumerable<object[]> Get_invalid_DateTimeKinds()
    {
        yield return new object[] { new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Local) };
        yield return new object[] { new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Unspecified) };
    }

    [Theory]
    [MemberData(nameof(Get_valid_DateTimes))]
    public void Constructor_returns_same_DateTime_as_passed_in(DateTime validDate)
    {
        DateTime actual = new IsoDateTime(validDate);
        Assert.Equal(validDate, actual);
    }

    public static IEnumerable<object[]> Get_valid_DateTimes()
    {
        yield return new object[] { new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc) };
        yield return new object[] { DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc) };
        yield return new object[] { DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc) };
        yield return new object[] { DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc) };
    }

    [Theory]
    [InlineData("2023-06-14T12:34:56.1234567Z")]
    [InlineData("2023-06-14T12:34:56.1234560Z")]
    [InlineData("2023-06-14T12:34:56.1234500Z")]
    [InlineData("2023-06-14T12:34:56.1234000Z")]
    [InlineData("2023-06-14T12:34:56.1230000Z")]
    [InlineData("2023-06-14T12:34:56.1200000Z")]
    [InlineData("2023-06-14T12:34:56.1000000Z")]
    [InlineData("2023-06-14T12:34:56.0000000Z")]
    public void ToString_returns_valid_iso_string(string iso)
    {
        string actual = new IsoDateTime(iso).ToString();
        Assert.Equal(iso, actual);
    }

    [Fact]
    public void Now_returns_current_DateTime()
    {
        DateTime actual = IsoDateTime.Now;
        DateTime utcNow = DateTime.UtcNow;
        Assert.Equal(utcNow, actual, TimeSpan.FromMilliseconds(1)); // 1ms is very generous (for my machine). Test succeeded often on 0.01ms
    }

    [Fact]
    public void Now_returns_Utc()
    {
        DateTime actual = IsoDateTime.Now;
        Assert.Equal(DateTimeKind.Utc, actual.Kind);
    }
}
