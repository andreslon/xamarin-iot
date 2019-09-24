using System;
using Prism.Commands;
using Prism.Navigation;
using XamarinIoTApp.Infrastructure.Interfaces.Repositories;
using XamarinIoTApp.Infrastructure.Interfaces.Services;
using XamarinIoTApp.Infrastructure.Repositories;

namespace XamarinIoTApp.Core.ViewModels
{
    public class ConnectionPageViewModel : ViewModelBase
    {
        public IDriverRepository DriverRepository { get; set; }
        public IObdService ObdService { get; set; }
        public ConnectionPageViewModel(
            INavigationService navigationService,
            IDriverRepository driverRepository,
            IObdService obdService)
            : base(navigationService)
        {
            this.DriverRepository = driverRepository;
            this.ObdService = obdService;
            Title = "Connection Page";

            StartOdbConnection();
        }

        async private void StartOdbConnection()
        {
            var connected=await ObdService.Init();
            if (connected)
            {

            }
        }
    }
}
