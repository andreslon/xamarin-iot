using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinIoTApp.Infrastructure.Interfaces;
using XamarinIoTApp.Infrastructure.Interfaces.Services;

namespace XamarinIoTApp.Services
{
    public class OBDDevice: IOBDDevice
	{
        public IObdService ObdService { get; set; }
        public OBDDevice(IObdService obdService)
        {
            ObdService = obdService;
        } 
        public bool IsSimulated { get; private set; }

        public async Task Disconnect()
        {
            await ObdService.Disconnect();
        }

        public async Task<bool> Initialize(bool simulatorMode = false)
        {
            IsSimulated = simulatorMode;
            return await ObdService.Init(simulatorMode);
        }

        public Dictionary<String, String> ReadData()
        {
            return ObdService.Read();
        }
    }
}
