using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleTest
{
    /// <summary>
    /// 时间格式化扩展
    /// </summary>
    public class DatetimeJsonConverter : JsonConverter<DateTime>
    {
        /// <summary>
        /// 读取
        /// </summary>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTime.TryParse(reader.GetString(), out DateTime date))
                    return date;
            }
            return reader.GetDateTime();
        }

        /// <summary>
        /// 写入
        /// </summary>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {

            if (value.Hour == 0 && value.Minute == 0 && value.Second == 0)
            {
                writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
            }
            else
            {
                writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
            }

        }
    }

    /// <summary>
    /// 时间格式化扩展
    /// </summary>
    public class DateTimeNullableConverter : JsonConverter<DateTime?>
    {
        /// <summary>
        /// 读取
        /// </summary>
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var val = reader.GetString();
                if (!string.IsNullOrEmpty(val) && DateTime.TryParse(val, out DateTime date))
                {
                    return date;
                }
                return null;
            }
            return reader.GetDateTime();
        }

        /// <summary>
        /// 写入
        /// </summary>
        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            {
                var dt = (DateTime)value;
                if (dt.Hour == 0 && dt.Minute == 0 && dt.Second == 0)
                {
                    writer.WriteStringValue(dt.ToString("yyyy-MM-dd"));
                }
                else
                {
                    writer.WriteStringValue(dt.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
        }
    }

    /// <summary>
    /// 时间格式化扩展
    /// </summary>
    public class DateTimeNullableConverterLongTime : JsonConverter<DateTime?>
    {
        /// <summary>
        /// 读取
        /// </summary>
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && DateTime.TryParse(reader.GetString(), out DateTime date))
            {
                return date;
            }
            return reader.GetDateTime();
        }

        /// <summary>
        /// 写入
        /// </summary>
        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    /// <summary>
    /// Boolean格式化扩展, 默认字符串,数字均不能转bool,所以加此扩展
    /// </summary>
    public class BoolJsonConverter : JsonConverter<bool>
    {
        /// <summary>
        /// 读取
        /// </summary>
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var str = (reader.GetString() ?? "").ToLower();
                return str == "yes" || str == "true" || str == "1";
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetInt32() != 0;
            }

            return reader.GetBoolean();
        }

        /// <summary>
        /// 写入
        /// </summary>
        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteBooleanValue(value);
        }
    }

    /// <summary>
    /// Boolean格式化扩展, 
    /// </summary>
    public class BoolNullableConverter : JsonConverter<bool?>
    {
        /// <summary>
        /// 读取
        /// </summary>
        public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }
            if (reader.TokenType == JsonTokenType.String)
            {
                var str = (reader.GetString() ?? "").ToLower();
                if (string.IsNullOrEmpty(str))
                {
                    return null;
                }
                return str == "yes" || str == "true" || str == "1";
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetInt32() != 0;
            }

            return reader.GetBoolean();
        }

        /// <summary>
        /// 写入
        /// </summary>
        public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteBooleanValue(value.Value);
            }
        }
    }
    /// <summary>
    /// int?格式化扩展, 
    /// </summary>
    public class IntNullableConverter : JsonConverter<int?>
    {
        /// <summary>
        /// 读取
        /// </summary>
        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var str = reader.GetString();
                if (!string.IsNullOrEmpty(str) && int.TryParse(reader.GetString(), out int result))
                {
                    return result;
                }
                return null;
            }
            else if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }
            return reader.GetInt32();
        }
        /// <summary>
        /// 写入
        /// </summary>
        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            writer.WriteNumberValue(value.Value);
        }
    }

    public static class JsonExtension
    {

        /// <summary>
        /// 对象转json字符串
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="ignoreNullVal">是否忽略空属性</param>
        /// <param name="indented">是否缩进</param>
        /// <param name="camelCase">是否启用驼峰</param>
        /// <returns></returns>
        public static string ToJsonString<TSource>(this TSource source, bool ignoreNullVal = false, bool indented = true, bool camelCase = true)
        {
            var options = new JsonSerializerOptions
            {
                Converters =
                                {
                                    new DatetimeJsonConverter(),
                                    new DateTimeNullableConverter(),
                                    new BoolJsonConverter(),
                                    new BoolNullableConverter(),
                                    new IntNullableConverter()
                                },
                //启用缩进设置
                WriteIndented = indented,
                //设置支持中文的unicode编码
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            //启用驼峰格式
            if (camelCase)
            {
                options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            }
            if (ignoreNullVal)
            {
                //是否忽略null属性
                options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            }
            try
            {
                return (typeof(TSource) == typeof(DataTable))
                ? JsonSerializer.Serialize(value: ((DataTable)(object)source).ToDictionaryList(), options: options)
                : JsonSerializer.Serialize(value: source, options: options);
            }
            catch (JsonException)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// DataTable转Dictionary List
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> ToDictionaryList(this DataTable dt)
        {
            var columns = dt.Columns.Cast<DataColumn>().ToList();
            return dt.Rows.Cast<DataRow>().Select(r => columns.ToDictionary(k => k.ColumnName, c => r[c] == DBNull.Value ? null : r[c])).ToList();
        }
    }
}
