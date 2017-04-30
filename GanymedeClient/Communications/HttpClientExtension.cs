using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace Ganymede.Communications
{
    public static class HttpClientExtension
    {
        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUrl, T model)
        {
            var stream = SerializerWrapper.SerializeToJson<T>(model);

            var streamReader = new StreamReader(stream);
            var json = streamReader.ReadToEnd();

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PostAsync(requestUrl, stringContent);
        }

    }
}
