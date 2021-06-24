//using System.IO;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace allTdL.Core.Utils
//{
//    public static partial class JsonTextUtil
//    {
//        /// <summary>Deserializes the json to typeof T.</summary>
//        /// <param name="json">The json.</param>
//        /// <returns>A T.</returns>
//        public static T Deserialize<T>(string json)
//        {
//            return JsonSerializer.Deserialize<T>(json);
//        }

//        /// <summary>Deserializes the JSON file stream async.</summary>
//        /// <param name="path">The path.</param>
//        /// <returns>A Task of T</returns>
//        public static async Task<T> DeserializeFilestreamAsync<T>(string path)
//        {
//            await using var openStream = File.OpenRead(path);
//            return await JsonSerializer.DeserializeAsync<T>(openStream);
//        }

//        /// <summary>Serialize an object to a JSON string.</summary>
//        /// <param name="obj">          The obj.</param>
//        /// <param name="writeIndented">If true, write indented.</param>
//        /// <returns>A serialized JSON string.</returns>
//        public static string Serialize(object obj, bool writeIndented = false)
//        {
//            return JsonSerializer.Serialize(obj, JsonSerializerOptions(writeIndented));
//        }

//        /// <summary>Serializes the file async.</summary>
//        /// <param name="path">    The path.</param>
//        /// <param name="contents">The contents.</param>
//        /// <returns>A Task.</returns>
//        public static async Task SerializeFileAsync(string path, object contents)
//        {
//            await using var fileStream = File.Create(path);
//            await JsonSerializer.SerializeAsync(fileStream, contents, JsonSerializerOptions(false));
//        }

//        /// <summary>To a serialized JSON string.</summary>
//        /// <param name="obj">The obj.</param>
//        /// <returns>A JSON string.</returns>
//        public static string ToJson(this object obj) => Serialize(obj);

//        private static JsonSerializerOptions JsonSerializerOptions(bool writeIndented) => new JsonSerializerOptions
//        {
//            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
//            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
//            WriteIndented = writeIndented
//        };
//    }
//}