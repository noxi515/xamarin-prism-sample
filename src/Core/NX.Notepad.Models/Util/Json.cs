using System.Globalization;
using System.IO;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NX.Notepad.Util
{
    public static class Json
    {
        private static readonly JsonSerializer Serializer;

        static Json()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            Serializer = JsonSerializer.CreateDefault(settings);
        }

        [ContractAnnotation("obj:null => null; obj:notnull => notnull")]
        public static string Stringify([CanBeNull] object obj, bool pretty = false)
        {
            if (obj == null)
            {
                return null;
            }

            using (var textWriter = new StringWriter(new StringBuilder(128), CultureInfo.InvariantCulture))
            using (var jsonWriter = new JsonTextWriter(textWriter))
            {
                jsonWriter.Formatting = pretty ? Formatting.None : Formatting.Indented;
                Serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
        }

        [ContractAnnotation("json:null => null; json:notnull => notnull")]
        public static T Parse<T>([CanBeNull] string json)
        {
            if (json == null)
            {
                return default(T);
            }

            using (var reader = new JsonTextReader(new StringReader(json)))
            {
                return Serializer.Deserialize<T>(reader);
            }
        }

        [NotNull]
        public static T Parse<T>([NotNull] Stream stream)
        {
            using (var reader = new JsonTextReader(new StreamReader(stream, Encoding.UTF8)))
            {
                return Serializer.Deserialize<T>(reader);
            }
        }

        [NotNull]
        public static string ToJson([NotNull] this object obj)
        {
            return Stringify(obj);
        }
    }
}