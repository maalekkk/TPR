using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TPR_Task2.Serialization
{
    public class JsonFormatter<T>
    {
        private static JsonSerializerSettings _settings;

        public JsonFormatter()
        {
            _settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                NullValueHandling = NullValueHandling.Include
            };
        }

        public void Serialize(Stream stream ,object obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented, _settings);
            byte[] buffer = Encoding.UTF8.GetBytes(json);
            stream.Write(buffer, 0, buffer.Length);
        }

        public T Deserialize(Stream stream)
        {
            using (StreamReader streamReader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd(), _settings);
            }
        }
    }
}