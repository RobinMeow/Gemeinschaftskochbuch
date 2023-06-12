using System;
using MongoDB.Bson.Serialization;

namespace api.Infrastructure;

public class DateTimeSerializer : IBsonSerializer
{
    public Type ValueType => typeof(DateTime);

    public object Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args) // from mongodb to C#
    {
        // MongoDB C# Driver Version (Src: https://github.com/mongodb/mongo-csharp-driver/blob/master/src/MongoDB.Bson/Serialization/Serializers/DateTimeSerializer.cs)
        // string date = MongoDB.Bson.IO.JsonConvert.ToDateTime(context.Reader.ReadString());

        string dateTimeIsoString = context.Reader.ReadString();
        return DateTime.Parse(dateTimeIsoString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.RoundtripKind);
    }

    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value) // from C# to mongodb
    {
        // MongoDB C# Driver Version (Src: https://github.com/mongodb/mongo-csharp-driver/blob/master/src/MongoDB.Bson/Serialization/Serializers/DateTimeSerializer.cs)
        // WriteString(JsonConvert.ToString(value))

        string dateTimeIso = ((DateTime)value).ToISOString();
        context.Writer.WriteString(dateTimeIso);
    }
}