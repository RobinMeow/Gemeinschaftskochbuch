using System;
using System.Globalization;

namespace api.Infrastructure;

public static class DateTimeISO8601StringConverter
{
    public static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture; // cache for performance

    ///<summary>Array of allowed ISO 8601 date-time formats ordered in likelihood to occur.</summary>
    public static readonly string[] AllowedIsoFormats = new[]
    {
        "yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.ffffff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.fffff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.ffff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.ff'Z'",
        "yyyy-MM-dd'T'HH:mm:ss.f'Z'",
        "yyyy-MM-dd'T'HH:mm:ss'Z'"
    };

    /// <summary>Converts the specified string in ISO 8601 format to a <see cref="DateTime"/> with <see cref="DateTimeKind.Utc"/>.</summary>
    /// <param name="dateTimeIsoString">The string in ISO 8601 format to convert.</param>
    /// <returns>A <see cref="DateTime"/> value with <see cref="DateTimeKind.Utc"/>.</returns>
    /// <exception cref="FormatException">Thrown if the input string is not in a valid ISO 8601 format.</exception>
    /// <exception cref="ArgumentNullException">Thrown if the input string is null.</exception>
    /// <exception cref="OverflowException">Thrown if the resulting <see cref="DateTime"/> value is outside the range of valid dates.</exception>
    public static DateTime Convert(string dateTimeIsoString)
    {
        // https://stackoverflow.com/questions/12787368/c-sharp-datetime-parse-issue
        // explains that AssumeUniversal returns DateTimeKind Local (the hack? xd) and corrects it if you call ToUniversal()
        // and AdjustToUniversal returns Utc, but "incorrects" the data if you call ToUniversal.
        return DateTime.ParseExact(dateTimeIsoString, AllowedIsoFormats, InvariantCulture, DateTimeStyles.AdjustToUniversal);
    }

    /// <summary>Converts the provided <see cref="DateTime"/> to an ISO 8601 string representation in UTC <see cref="DateTimeKind"/>.</summary>
    /// <param name="dateTime">The <see cref="DateTime"/> to convert.</param>
    /// <returns>The ISO 8601 string representation of the <see cref="DateTime"/> in UTC format.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the <paramref name="dateTime"/> is not of <see cref="DateTimeKind.Utc"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="dateTime"/> is null.</exception>
    public static string Convert(DateTime dateTime)
    {
        if (dateTime.Kind != DateTimeKind.Utc)
            throw new InvalidOperationException("DateTime should always be handled in Utc.");

        return dateTime.ToString("o", InvariantCulture);
    }
}
