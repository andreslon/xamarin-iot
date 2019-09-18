using System.Collections.Generic;

namespace XamarinIoTApp.Infrastructure.Models.Responses
{
    public class Section
    {
        public string Label { get; set; }
        public string Key { get; set; }
        public List<string> Values { get; set; }
        public List<Control> Controls { get; set; }
    }
}