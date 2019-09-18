using System;
using System.Collections.Generic;

namespace XamarinIoTApp.Infrastructure.Models.Responses
{
    public class Form
    {
        public string Id { get; set; }
        public string StepId { get; set; }
        public List<Section> Sections { get; set; }
    }
}
