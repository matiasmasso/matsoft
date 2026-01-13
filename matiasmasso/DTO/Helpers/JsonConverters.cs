using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DTO.Helpers
{
    public static class JsonConverters
    {
        // Simple DateOnly converter for System.Text.Json (.NET 6+)
        public class DateOnlyJsonConverter : JsonConverter<DateOnly>
        {
            private const string Format = "yyyy-MM-dd";

            public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var s = reader.GetString();
                if (string.IsNullOrEmpty(s)) return default;
                return DateOnly.ParseExact(s, Format);
            }

            public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString(Format));
            }
        }
    }
}
