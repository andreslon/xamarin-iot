using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinIoTApp.Infrastructure.Interfaces.Repositories;
using XamarinIoTApp.Infrastructure.Interfaces.Services;
using XamarinIoTApp.Infrastructure.Models.Responses;

namespace XamarinIoTApp.Infrastructure.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        public IHttpClientService HttpClientService { get; set; }
        public IJsonService JsonService { get; set; }
        public IResourcesService ResourcesService { get; set; }
        public INetworkService NetworkService { get; set; }
        public DriverRepository(IHttpClientService httpClientService, IJsonService jsonService, IResourcesService resourcesService, INetworkService networkService)
        {
            this.HttpClientService = httpClientService;
            this.JsonService = jsonService;
            this.ResourcesService = resourcesService;
            this.NetworkService = networkService;
        }
         
    }
}
