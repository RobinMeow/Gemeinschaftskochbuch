using System;
using MongoDB.Bson.Serialization;

namespace api.Infrastructure;

public class DateTimeSerializer : IBsonSerializer
{
    public Type ValueType => typeof(DateTime);

    public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) // from mongodb to C#
    {
        string dateTimeIsoString = context.Reader.ReadString();
        return DateTime.Parse(dateTimeIsoString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.RoundtripKind);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value) // from C# to mongodb
    {
        string dateTimeIso = ((DateTime)value).ToISOString();
        context.Writer.WriteString(dateTimeIso);
    }
}