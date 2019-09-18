using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamarinIoTApp.Infrastructure.Interfaces;
using XamarinIoTApp.Infrastructure.Interfaces.Services;

namespace XamarinIoTApp.Infrastructure.Services
{
    public class HttpClientService : IHttpClientService
    {
        public HttpClient Client { get; set; }
        public HttpClientService()
        {
            Client = new HttpClient();
        }
        async public Task<HttpResponseMessage> GetAsync(string url, string authorizationToken = null)
        { 
             return await Client.GetAsync(new Uri(url)); 
        }
        async public Task<HttpResponseMessage> PostAsync(string url, Dictionary<string,string> request, string authorizationToken = null)
        {
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await Client.PostAsync(url, new FormUrlEncodedContent(request));
            //string bodyRequest = JsonConvert.SerializeObject(request);
            //return await Client.PostAsync(url, new StringContent(bodyRequest, Encoding.UTF8, "application/x-www-form-urlencoded"));
        }
        async public Task<HttpResponseMessage> PutAsync<T>(string url, T request, string authorizationToken = null)
        {
            string bodyRequest = JsonConvert.SerializeObject(request);
            return await Client.PostAsync(url, new StringContent(bodyRequest, Encoding.UTF8, "application/json"));
        }
        async public Task<HttpResponseMessage> DeleteAsync(string url, string authorizationToken = null)
        {
            return await Client.DeleteAsync(new Uri(url));
        }
    }
}
