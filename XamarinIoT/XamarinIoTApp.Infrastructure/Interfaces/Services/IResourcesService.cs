using System;
namespace XamarinIoTApp.Infrastructure.Interfaces.Services
{
    public interface IResourcesService
    {
        string GetSetting(string key);
        string GetResource(string key);
    }
}
