using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace XamarinIoTApp.Infrastructure.Interfaces.Services
{
    public interface IJsonService
    {
        Task<T> GetSerializedResponse<T>(HttpResponseMessage result);
        T Deserialize<T>(string text);
        string Serialize(object obj);
    }
}
