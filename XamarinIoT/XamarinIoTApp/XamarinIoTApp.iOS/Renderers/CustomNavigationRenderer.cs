using System;
using XamarinIoTApp.Extensions;
using XamarinIoTApp.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
 

[assembly: ExportRenderer(typeof(CustomNavigationPage), typeof(CustomNavigationRenderer))]

namespace XamarinIoTApp.iOS.Renderers
{
    public class CustomNavigationRenderer : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationBar.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            NavigationBar.ShadowImage = new UIImage();
            NavigationBar.BackgroundColor = UIColor.Clear;
            NavigationBar.BarTintColor = UIColor.Clear;
            NavigationBar.Translucent = true;

            UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            UINavigationBar.Appearance.ShadowImage = new UIImage();
            UINavigationBar.Appearance.BackgroundColor = UIColor.Clear;
            //UINavigationBar.Appearance.TintColor = UIColor.Red;
            UINavigationBar.Appearance.BarTintColor = UIColor.Clear;
            UINavigationBar.Appearance.Translucent = true;
        }

        protected override void Dispose(bool disposing)
        {  
            base.Dispose(disposing);
        }
    }
}
