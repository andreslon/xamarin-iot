using System;
using Xamarin.Forms;

namespace XamarinIoTApp.Services
{
    public class ConfigurationService
    {
        public void SetConfiguration(string primaryColor)
        {

            //Set application settings XAML
            //App.Current.Resources["PrimaryColor"] = primaryColor;
            Prism.PrismApplicationBase.Current.Resources["PrimaryColor"] = primaryColor;

            //

            if (Device.OS == TargetPlatform.Android)
            {

            }
        }
    }
}
