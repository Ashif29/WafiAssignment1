using System.Text.Json.Serialization;
using System.Text.Json;

namespace WafiArche.Api.Conveerters
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && DateTime.TryParse(reader.GetString(), out DateTime dateTime))
            {
                return DateOnly.FromDateTime(dateTime);
            }
            throw new JsonException("Invalid date format");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }
}
