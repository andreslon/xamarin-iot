using System;
using System.Collections.Generic;
using Prism.Events;

namespace XamarinIoTApp.Core.Events
{
    public class ObdEvent : PubSubEvent<Dictionary<String, String>>
    {
    }
}
