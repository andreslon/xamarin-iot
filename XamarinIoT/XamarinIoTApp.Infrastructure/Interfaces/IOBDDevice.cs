using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamarinIoTApp.Infrastructure.Interfaces
{
    public interface IOBDDevice
    {
        bool IsSimulated { get; }
        Task<bool> Initialize(bool simulatorMode = false);
        Dictionary<String, String> ReadData();
        Task Disconnect();
    }
}
