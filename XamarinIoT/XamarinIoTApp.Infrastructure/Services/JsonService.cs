using Newtonsoft.Json;
using XamarinIoTApp.Infrastructure.Interfaces;
using XamarinIoTApp.Infrastructure.Interfaces.Services;
using System.Net.Http;
using System.Threading.Tasks;

namespace XamarinIoTApp.Infrastructure.Services
{
    public class JsonService : IJsonService
    {
        public async Task<T> GetSerializedResponse<T>(HttpResponseMessage result)
        {
            string response = await result.Content.ReadAsStringAsync();
            T serializedResponse = JsonConvert.DeserializeObject<T >(response);
            return serializedResponse;
        }


        public T Deserialize<T>(string text)
        {
            T deserializedObject = JsonConvert.DeserializeObject<T>(text);
            return deserializedObject;
        }
        public string Serialize(object obj)
        {
            string serializedObject = JsonConvert.SerializeObject(obj);
            return serializedObject;
        }
    }
}
