using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation; 
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
        public IEventHubRepository EventHubRepository { get; set; }
        public ConnectionPageViewModel( 
            INavigationService navigationService,
            IDriverRepository driverRepository,
            IEventHubRepository eventHubRepository,
            IOBDDevice iOBDDevice )
            : base(navigationService)
        {
            this.DriverRepository = driverRepository; 
            this.OBDDevice = iOBDDevice;
            this.EventHubRepository = eventHubRepository;
            Title = "Connection Page"; 
            ConnectCommand = new DelegateCommand(Connect);
            SimulateConnectionCommand = new DelegateCommand(SimulateConnection);
            ClearCommand = new DelegateCommand(Clear);

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
        }

        private void Clear()
        {
            CurrentTrip.Points = new ObservableCollection<TripPoint>();
        } 
        public DelegateCommand ClearCommand { get; private set; }
        public DelegateCommand ConnectCommand { get; private set; }
        public DelegateCommand SimulateConnectionCommand { get; private set; }
        
        public bool isConnectedToObd { get; set; }
        public Trip CurrentTrip { get; private set; }
         
        public double fuelConsumptionRate { get; set; }
        public string FuelConsumptionUnits { get; set; }
        public string DistanceUnits { get; set; }
        public string ElapsedTime { get; set; }
        public string Distance { get; set; }
        public string FuelConsumption { get; set; }
        public string EngineLoad { get; set; }
        public double RPM { get; set; }
        public double Speed { get; set; }


        async private void SimulateConnection()
        {
            isConnectedToObd = await OBDDevice.Initialize(true);
            if (isConnectedToObd)
            {
                await StartStreaming();
            }
        }
        async private void Connect()
        {
            isConnectedToObd = await OBDDevice.Initialize();
            if (isConnectedToObd)
            {
               await StartStreaming(); 
            }
        }

       async private Task StartStreaming()
        {
            while (true)
            {

                TripPoint previous = null;
                double newDistance = 0;
                if (CurrentTrip.Points.Count > 1)
                {
                    previous = CurrentTrip.Points[CurrentTrip.Points.Count - 1];
                    //newDistance = DistanceUtils.CalculateDistance(userLocation.Latitude,
                    //    userLocation.Longitude, previous.Latitude, previous.Longitude);

                    //if (newDistance > 4) // if more than 4 miles then gps is off don't use
                    //    return;
                }

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

                if (point.RPM>0)
                {
                    CurrentTrip.Points.Add(point);
                    EventHubRepository.SendToCloudMessagesAsync(new Infrastructure.Models.IoTMessage
                    {
                        Speed= point.Speed,
                        RPM = point.RPM,
                        TripId = point.TripId,
                        RecordedTimeStamp = point.RecordedTimeStamp,
                        FuelConsumption = FuelConsumption,
                        ElapsedTime = ElapsedTime 
                    });

                    if (CurrentTrip.Points.Count > 1 && previous != null)
                    {
                        RPM = point.RPM;
                        Speed = point.Speed;
                        CurrentTrip.Distance += newDistance;
                        Distance = CurrentTrip.TotalDistanceNoUnits;

                        //calculate gas usage
                        var timeDif1 = point.RecordedTimeStamp - previous.RecordedTimeStamp;
                        CurrentTrip.FuelUsed += fuelConsumptionRate * 0.00002236413 * timeDif1.TotalSeconds;
                        if (CurrentTrip.FuelUsed == 0)
                            FuelConsumption = "N/A";
                        else
                            FuelConsumption = (CurrentTrip.FuelUsed * 3.7854).ToString("N2");
                    }
                    else
                    {
                        CurrentTrip.FuelUsed = 0;
                        FuelConsumption = "N/A";
                    }

                    var timeDif = point.RecordedTimeStamp - CurrentTrip.RecordedTimeStamp;

                    //track seconds, minutes, then hours
                    if (timeDif.TotalMinutes < 1)
                        ElapsedTime = $"{timeDif.Seconds}s";
                    else if (timeDif.TotalHours < 1)
                        ElapsedTime = $"{timeDif.Minutes}m {timeDif.Seconds}s";
                    else
                        ElapsedTime = $"{(int)timeDif.TotalHours}h {timeDif.Minutes}m {timeDif.Seconds}s";

                    if (point.EngineLoad != -255)
                        EngineLoad = $"{(int)point.EngineLoad}%";

                    FuelConsumptionUnits = "Gallons";
                    DistanceUnits = "Kilometers";
                } 
                await Task.Delay(1000);
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
