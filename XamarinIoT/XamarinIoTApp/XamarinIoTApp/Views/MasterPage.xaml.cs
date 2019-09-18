using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinIoTApp.Extensions;
using Xamarin.Forms;

namespace XamarinIoTApp.Views
{
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //this.Detail = new CustomNavigationPage( new UserPage());
        }
    }
}
