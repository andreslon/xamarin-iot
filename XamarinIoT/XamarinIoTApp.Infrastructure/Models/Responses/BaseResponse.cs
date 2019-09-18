using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace XamarinIoTApp.Infrastructure.Models.Responses
{
     
    public class BaseResponse<T>
    {
        [JsonIgnore()]
        public HttpResponseMessage HttpResponse { get; set; }
        public T Data { get; set; } 
    }

    public class BaseResponse
    {
        [JsonIgnore()]
        public HttpResponseMessage HttpResponse { get; set; }
    }
}
