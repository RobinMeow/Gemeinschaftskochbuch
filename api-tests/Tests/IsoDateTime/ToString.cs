using api.Domain;

namespace api_tests.IsoDateTime_conversion_tests;

public sealed class ToString
{
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
}
