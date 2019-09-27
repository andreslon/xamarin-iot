using System;
using XamarinIoTApp.Infrastructure.Models;

namespace XamarinIoTApp.Infrastructure.Interfaces.Repositories
{
    public interface IEventHubRepository
    {
        void SendToCloudMessagesAsync(IoTMessage message);
    }
}
