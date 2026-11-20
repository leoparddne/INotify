using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.Notify.WebAPI.Util
{
    /// <summary>
    /// 自定义 DateTime 转换器（yyyy-MM-dd HH:mm:ss）
    /// </summary>
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString()!);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
