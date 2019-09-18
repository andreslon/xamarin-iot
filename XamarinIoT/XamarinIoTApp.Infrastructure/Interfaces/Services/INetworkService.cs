using System;
namespace XamarinIoTApp.Infrastructure.Interfaces.Services
{
    public interface INetworkService
    {
        bool IsNetworkAvailable { get; }
    }
}
