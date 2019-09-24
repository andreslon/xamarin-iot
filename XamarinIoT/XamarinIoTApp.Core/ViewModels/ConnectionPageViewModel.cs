using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using XamarinIoTApp.Core.Events;
using XamarinIoTApp.Core.Models;
using XamarinIoTApp.Infrastructure.Interfaces;
using XamarinIoTApp.Infrastructure.Interfaces.Repositories;
using XamarinIoTApp.Infrastructure.Interfaces.Services;
using XamarinIoTApp.Infrastructure.Repositories;

namespace XamarinIoTApp.Core.ViewModels
{
    public class ConnectionPageViewModel : ViewModelBase
    {
        public IDriverRepository DriverRepository { get; set; }
        public IOBDDevice OBDDevice { get; set; } 
        public IEventAggregator EventAggregator { get; set; }
        public ConnectionPageViewModel(
            IEventAggregator eventAggregator,
            INavigationService navigationService,
            IDriverRepository driverRepository,
            IOBDDevice iOBDDevice )
            : base(navigationService)
        {
            this.DriverRepository = driverRepository; 
            this.OBDDevice = iOBDDevice;
            Title = "Connection Page";
            this.EventAggregator = eventAggregator;
            ConnectCommand = new DelegateCommand(Connect);

            CurrentTrip = new Trip
            {
                Id= Guid.NewGuid().ToString(),
                Points = new ObservableCollection<TripPoint>()
            };
            fuelConsumptionRate = 0;
            //FuelConsumptionUnits = Settings.MetricUnits ? "Liters" : "Gallons";
            FuelConsumptionUnits = "Gallons";
            DistanceUnits = "Kilometers";
            ElapsedTime = "0s";
            Distance = "0.0";
            FuelConsumption = "N/A";
            EngineLoad = "N/A";

            EventAggregator.GetEvent<ObdEvent>().Subscribe(ProcessEvent);
        }

        private void ProcessEvent(Dictionary<string, string> obj)
        {
             
        }

        public DelegateCommand ConnectCommand { get; private set; }

        public bool isConnectedToObd { get; set; }
        public Trip CurrentTrip { get; private set; }

        private double fuelConsumptionRate;

        public string FuelConsumptionUnits { get; }
        public string DistanceUnits { get; }
        public string ElapsedTime { get; }
        public string Distance { get; }
        public string FuelConsumption { get; }
        public string EngineLoad { get; }

        async private void Connect()
        {
            isConnectedToObd = await OBDDevice.Initialize();
            if (isConnectedToObd)
            {

                while (true)
                {
                    var point = new TripPoint
                    {
                        TripId = CurrentTrip.Id,
                        RecordedTimeStamp = DateTime.UtcNow,
                        //Latitude = userLocation.Latitude,
                        //Longitude = userLocation.Longitude,
                        Sequence = CurrentTrip.Points.Count,
                        Speed = -255,
                        RPM = -255,
                        ShortTermFuelBank = -255,
                        LongTermFuelBank = -255,
                        ThrottlePosition = -255,
                        RelativeThrottlePosition = -255,
                        Runtime = -255,
                        DistanceWithMalfunctionLight = -255,
                        EngineLoad = -255,
                        MassFlowRate = -255,
                        EngineFuelRate = -255,
                        VIN = "-255"
                    };
                    AddOBDDataToPoint(point);
                    CurrentTrip.Points.Add(point);

                    await Task.Delay(1000);
                }



            }
        } 

        async void AddOBDDataToPoint(TripPoint point)
        {
            //Read data from the OBD device
            point.HasOBDData = false;
            Dictionary<string, string> obdData = null;

            //if (obdDataProcessor != null)
            obdData =  OBDDevice.ReadData();
             

            if (obdData != null)
            {
                double speed = -255,
                rpm = -255,
                efr = -255,
                el = -255,
                stfb = -255,
                ltfb = -255,
                fr = -255,
                tp = -255,
                rt = -255,
                dis = -255,
                rtp = -255;
                var vin = String.Empty;

                if (obdData.ContainsKey("el") && !string.IsNullOrWhiteSpace(obdData["el"]))
                {
                    if (!double.TryParse(obdData["el"], out el))
                        el = -255;
                }
                if (obdData.ContainsKey("stfb"))
                    double.TryParse(obdData["stfb"], out stfb);
                if (obdData.ContainsKey("ltfb"))
                    double.TryParse(obdData["ltfb"], out ltfb);
                if (obdData.ContainsKey("fr"))
                {
                    double.TryParse(obdData["fr"], out fr);
                    if (fr != -255)
                    {
                        fuelConsumptionRate = fr;
                    }
                }
                if (obdData.ContainsKey("tp"))
                    double.TryParse(obdData["tp"], out tp);
                if (obdData.ContainsKey("rt"))
                    double.TryParse(obdData["rt"], out rt);
                if (obdData.ContainsKey("dis"))
                    double.TryParse(obdData["dis"], out dis);
                if (obdData.ContainsKey("rtp"))
                    double.TryParse(obdData["rtp"], out rtp);
                if (obdData.ContainsKey("spd"))
                    double.TryParse(obdData["spd"], out speed);
                if (obdData.ContainsKey("rpm"))
                    double.TryParse(obdData["rpm"], out rpm);
                if (obdData.ContainsKey("efr") && !string.IsNullOrWhiteSpace(obdData["efr"]))
                {
                    if (!double.TryParse(obdData["efr"], out efr))
                        efr = -255;
                }
                else
                {
                    efr = -255;
                }
                if (obdData.ContainsKey("vin"))
                    vin = obdData["vin"];

                point.EngineLoad = el;
                point.ShortTermFuelBank = stfb;
                point.LongTermFuelBank = ltfb;
                point.MassFlowRate = fr;
                point.ThrottlePosition = tp;
                point.Runtime = rt;
                point.DistanceWithMalfunctionLight = dis;
                point.RelativeThrottlePosition = rtp;
                point.Speed = speed;
                point.RPM = rpm;
                point.EngineFuelRate = efr;
                point.VIN = vin;

#if DEBUG
                foreach (var kvp in obdData)
                {
                    Console.WriteLine($"{kvp.Key} {kvp.Value}");
                }
                    
#endif

                point.HasOBDData = true;
            }
        }

    }
}
