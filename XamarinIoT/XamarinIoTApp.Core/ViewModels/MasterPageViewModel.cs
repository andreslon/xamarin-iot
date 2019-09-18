using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms; 
namespace XamarinIoTApp.Core.ViewModels
{
    public class MasterPageViewModel : BindableBase
    {
        private INavigationService _navigationService;

        public ObservableCollection<MenuItem> MenuItems { get; set; }

        private MenuItem selectedMenuItem;
        public MenuItem SelectedMenuItem
        {
            get => selectedMenuItem;
            set => SetProperty(ref selectedMenuItem, value);
        }

        public DelegateCommand NavigateCommand { get; private set; }

        public MasterPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            MenuItems = new ObservableCollection<MenuItem>();
            MenuItems.Add(new MenuItem()
            {
                Icon = "Menu",
                PageName = "StepsPage",
                Title = "Pre-Advise"
            });
            MenuItems.Add(new MenuItem()
            {
                Icon = "Menu",
                PageName = "UserPage",
                Title = "Profile"
            });
            NavigateCommand = new DelegateCommand(Navigate);
        }

        async void Navigate()
        {
            await _navigationService.NavigateAsync($"{NavigationConstants.Menu}/{SelectedMenuItem.PageName}");
        }
    }
    public class MenuItem
    {

        public string Title { get; set; }
        public string Icon { get; set; }
        public string PageName { get; set; }
    }
}
