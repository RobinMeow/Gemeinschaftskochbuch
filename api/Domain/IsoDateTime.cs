using System;
using System.Globalization;

namespace api.Domain;

public struct IsoDateTime
{
    static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture; // cache for performance

    ///<summary>Array of allowed ISO 8601 date-time formats ordered in likelihood to occur.</summary>
    static readonly string[] AllowedIsoFormats = new[]
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

    DateTime _dateTime = DateTime.MinValue;

    public IsoDateTime(string isoString)
    {
        DateTime dateTimeUnspecified = DateTime.ParseExact(isoString, AllowedIsoFormats, InvariantCulture, DateTimeStyles.AdjustToUniversal);
        _dateTime = DateTime.SpecifyKind(dateTimeUnspecified, DateTimeKind.Utc);
    }

    public static explicit operator DateTime(IsoDateTime isoDateTime)
    {
        return isoDateTime._dateTime;
    }

    public override string ToString()
    {
        return _dateTime.ToString("o", InvariantCulture);
    }
}