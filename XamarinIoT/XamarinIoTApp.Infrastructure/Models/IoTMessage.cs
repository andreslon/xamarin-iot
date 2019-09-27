using System;
namespace XamarinIoTApp.Infrastructure.Models
{
    public class IoTMessage
    {
        public double Speed { get;  set; }
        public string TripId { get; set; }
        public DateTime RecordedTimeStamp { get; set; }
        public string FuelConsumption { get; set; }
        public double RPM { get; set; }
        public string ElapsedTime { get; set; }
    }
}
