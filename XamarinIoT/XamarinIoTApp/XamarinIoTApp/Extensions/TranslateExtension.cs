using System;
using System.Collections.Generic;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinIoTApp.Extensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            try
            {
                if (Text == null)
                    return null;
                return Infrastructure.Resources.AppResources
                    .ResourceManager.GetString(Text);
            }
            catch (Exception ex)
            { 
                return Text;
            }
        }
    }
}
