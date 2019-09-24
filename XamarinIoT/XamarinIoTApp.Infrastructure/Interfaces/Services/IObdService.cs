using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinIoTApp.Infrastructure.Interfaces.Services
{
    public interface IObdService
    {
        Task<bool> Init(bool simulatormode = false);
        Dictionary<string, string> Read();
        Task Disconnect();
    }
}
