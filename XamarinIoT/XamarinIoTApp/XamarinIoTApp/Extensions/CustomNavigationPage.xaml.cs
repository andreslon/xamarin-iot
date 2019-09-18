using System;
using System.Collections.Generic;

using xam= Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;

namespace XamarinIoTApp.Extensions
{
    public partial class CustomNavigationPage : xam.NavigationPage
    {
        public CustomNavigationPage() : base()
        {
            InitializeComponent();
        }
        public CustomNavigationPage(xam.Page rootPage) : base(rootPage)
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //(this as xam.NavigationPage).On<iOS>().EnableTranslucentNavigationBar();
            //this.BarBackgroundColor = Color.Transparent; 
        }

        public bool IgnoreLayoutChange { get; set; } = false;

        protected override void OnSizeAllocated(double width, double height)
        {
            if (!IgnoreLayoutChange)
                base.OnSizeAllocated(width, height);
        }
    }
}
