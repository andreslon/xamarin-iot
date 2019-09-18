using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using XamarinIoTApp.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinIoTApp.Core.ViewModels
{
    public class TripsPageViewModel : ViewModelBase
    {
        public TripsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}
