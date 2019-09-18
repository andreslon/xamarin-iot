using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks; 

namespace XamarinIoTApp.Infrastructure.Interfaces.Services
{
    public interface IHttpClientService
    {
        Task<HttpResponseMessage> GetAsync(string url, string authorizationToken = null);
        Task<HttpResponseMessage> PostAsync(string url, Dictionary<string,string> request, string authorizationToken = null);
        Task<HttpResponseMessage> PutAsync<T>(string url, T request, string authorizationToken = null);
        Task<HttpResponseMessage> DeleteAsync(string url, string authorizationToken = null);
    }
}
