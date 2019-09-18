using System;
using XamarinIoTApp.Infrastructure.Interfaces.Services;
using Xamarin.Essentials;

namespace XamarinIoTApp.Services
{
    public class NetworkService: INetworkService
    {
        public bool IsNetworkAvailable
        {
            get
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    // Connection to internet is available
                    return true;
                }
                return false;
            }
        }
    }
}
