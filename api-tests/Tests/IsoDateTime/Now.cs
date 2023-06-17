using api.Domain;

namespace api_tests.IsoDateTime_conversion_tests;

public sealed class Now
{
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
