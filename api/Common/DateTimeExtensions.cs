using System;

namespace api;

public static class DateTimeExtensions
{
    public static string ToISOString(this DateTime date)
    {
        if (date.Kind != DateTimeKind.Utc)
            date = date.ToUniversalTime();

        return date.ToString("o", System.Globalization.CultureInfo.InvariantCulture); // Round-trip Format Specifier (“o”);
    }
}
