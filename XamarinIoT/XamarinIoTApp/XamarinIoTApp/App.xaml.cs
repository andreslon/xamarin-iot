using Prism;
using Prism.Ioc;
using XamarinIoTApp.Core;
using XamarinIoTApp.Core.ViewModels;
using XamarinIoTApp.Extensions;
using XamarinIoTApp.Infrastructure.Interfaces;
using XamarinIoTApp.Infrastructure.Interfaces.Repositories;
using XamarinIoTApp.Infrastructure.Interfaces.Services;
using XamarinIoTApp.Infrastructure.Repositories;
using XamarinIoTApp.Infrastructure.Services;
using XamarinIoTApp.Services;
using XamarinIoTApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinIoTApp
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        { 
                InitializeComponent();
            await NavigationService.NavigateAsync(NavigationConstants.Connection);
            
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<CustomNavigationPage>();
            containerRegistry.RegisterForNavigation<MasterPage, MasterPageViewModel>(); 
            containerRegistry.RegisterForNavigation<ConnectionPage, ConnectionPageViewModel>();
            containerRegistry.RegisterForNavigation<UserPage, UserPageViewModel>();

            containerRegistry.Register<IFileService, FileService>();
            containerRegistry.Register<IJsonService, JsonService>();
            containerRegistry.Register<IHttpClientService, HttpClientService>();
            containerRegistry.Register<IResourcesService, ResourcesService>();
            containerRegistry.Register<INetworkService, NetworkService>();
             

            //Repositories
            containerRegistry.Register<IDriverRepository, DriverRepository>();
        }
    }
}
