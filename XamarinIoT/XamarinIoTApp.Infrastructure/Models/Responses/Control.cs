using System.Collections.Generic;

namespace XamarinIoTApp.Infrastructure.Models.Responses
{
    public class Control
    {
        public string Label { get; set; }
        public string Type { get; set; }
        public string Placeholder { get; set; }
        public bool Required { get; set; }
        public List<Data> Data { get; set; }
        public string Key { get; set; }
        public bool Single { get; set; }
        public string Url { get; set; }
        public string CurrentValue { get; set; }
    }
}