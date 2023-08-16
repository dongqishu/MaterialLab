using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class GlobalUtil
    {

        /// <summary>
        /// 字符串转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonstr"></param>
        /// <param name="defaultVal"></param>
        /// <returns></returns>
        public static T DeserializeJson<T>(string jsonstr, T defaultVal = default)
        {
            return (T)DeserializeJsonByType(jsonstr, typeof(T), defaultVal);
        }

        public static object DeserializeJsonByType(string jsonstr, Type type, object defaultVal = null)
        {
            //JsonObject To Object:  JsonSerializer.Deserialize<MyClass>(jsonObj);

            //从安全性角度说应Deserialize特定类型, 如:Student
            if (!string.IsNullOrEmpty(jsonstr))
            {
                try
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
                        }
                    };
                    //设置支持中文的unicode编码
                    options.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                    options.ReferenceHandler = ReferenceHandler.IgnoreCycles;

                    //充许字符串形数字读取
                    options.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                    //启用缩进设置, 反序列化不影响
                    // options.JsonSerializerOptions.WriteIndented = true;
                    //允许多余逗号
                    options.AllowTrailingCommas = true;
                    //跳过注释
                    options.ReadCommentHandling = JsonCommentHandling.Skip;
                    options.UnknownTypeHandling = JsonUnknownTypeHandling.JsonNode;

                    //启用驼峰格式
                    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    //设为True则与Newtonjson一致, name, Name都可反序列化到对象. 否则: camelCase模式则:只能name=>Name, 非camelCase: 只能Name=>Name
                    options.PropertyNameCaseInsensitive = true;
                    return JsonSerializer.Deserialize(jsonstr, type, options);
                }
                catch
                {

                }
            }
            return defaultVal;
        }


        /// <summary>
        /// 对象转字符串 - 默认不启用缩进设置
        /// </summary>
        /// <param name="obj">obj对象</param>
        /// <param name="ignoreNullVal">是否忽略空属性</param>
        /// <param name="indented">是否缩进</param>
        /// <param name="camelCase">是否启用驼峰</param>
        /// <returns></returns>
        public static string SeriliazeToJson(object obj, bool ignoreNullVal = false, bool indented = false, bool camelCase = true)
        {
            return obj.ToJsonString(ignoreNullVal, indented, camelCase);
        }
    }
}
