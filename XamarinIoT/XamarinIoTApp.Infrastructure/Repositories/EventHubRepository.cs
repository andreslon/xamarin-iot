using System;
using System.Text;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using XamarinIoTApp.Infrastructure.Interfaces.Repositories;
using XamarinIoTApp.Infrastructure.Models;

namespace XamarinIoTApp.Infrastructure.Repositories
{
    public class EventHubRepository: IEventHubRepository
    {
        public DeviceClient DeviceClient { get; set; }
        public string connectionString { get; set; } = "HostName=monkeyfest-iothub.azure-devices.net;DeviceId=Monkeyfest-device;SharedAccessKey=mo66jHCEyzYwh/dZxBNPiUWQfY5nvsiX4X5d2iqEJkk=";
        public EventHubRepository()
        {
            DeviceClient = DeviceClient.CreateFromConnectionString(connectionString, TransportType.Mqtt);
        }
        public async void SendToCloudMessagesAsync(IoTMessage message)
        {
            var messageString = JsonConvert.SerializeObject(message);
            var request = new Message(Encoding.ASCII.GetBytes(messageString));

            request.Properties.Add("speedAlert", (message.Speed > 60) ? "true" : "false");
            await DeviceClient.SendEventAsync(request);
        }
    }
}
