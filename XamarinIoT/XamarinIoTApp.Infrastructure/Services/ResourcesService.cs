using System;
using XamarinIoTApp.Infrastructure.Interfaces;
using XamarinIoTApp.Infrastructure.Interfaces.Services;
using XamarinIoTApp.Infrastructure.Resources;

namespace XamarinIoTApp.Infrastructure.Services
{
    public class ResourcesService: IResourcesService
    {
        public string GetSetting(string key)
        {
            return AppSettings.ResourceManager.GetString(key);
        }
        public string GetResource(string key)
        {
            return AppResources.ResourceManager.GetString(key);
        }
    }
}
