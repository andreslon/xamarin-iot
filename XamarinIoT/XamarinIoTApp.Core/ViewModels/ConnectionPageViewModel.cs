using System;
using Prism.Commands;
using Prism.Navigation;
using XamarinIoTApp.Infrastructure.Interfaces.Repositories;
using XamarinIoTApp.Infrastructure.Repositories;

namespace XamarinIoTApp.Core.ViewModels
{
    public class ConnectionPageViewModel : ViewModelBase
    {
        public IDriverRepository DriverRepository { get; set; } 

        public ConnectionPageViewModel(INavigationService navigationService,IDriverRepository driverRepository)
            : base(navigationService)
        {
            this.DriverRepository = driverRepository;
            Title = "Connection Page";
        }
    }
}
