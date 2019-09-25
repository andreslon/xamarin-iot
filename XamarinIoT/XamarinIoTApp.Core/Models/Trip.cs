using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Prism.Mvvm;

namespace XamarinIoTApp.Core.Models
{
    public class Trip: BindableBase
    {
        public Trip()
        { 
        }

        public string Name { get; set; }

        public string UserId { get; set; }

        public ObservableCollection<TripPoint> Points { get; set; }


        public DateTime RecordedTimeStamp { get; set; }
        public DateTime EndTimeStamp { get; set; }

        /// <summary>
        ///     Gets or sets the rating. 0 - 100
        /// </summary>
        /// <value>The rating.</value>
        public int Rating { get; set; }

        public bool IsComplete { get; set; }

        public bool HasSimulatedOBDData { get; set; }

        /// <summary>
        ///     Gets or sets the average speed.
        /// </summary>
        /// <value>The average speed.</value>
        public double AverageSpeed { get; set; }

        /// <summary>
        ///     Gets or sets the fuel used. Stored in Gallons
        /// </summary>
        /// <value>The fuel used.</value>
        public double FuelUsed { get; set; }

        public long HardStops { get; set; }

        public long HardAccelerations { get; set; }

        public string MainPhotoUrl { get; set; }

#if BACKEND
        public double Distance { get; set; }
#else
        double distance;

        /// <summary>
        ///     Gets or sets the total distance in miles.
        /// </summary>
        /// <value>The total distance.</value> 
        public double Distance { get; set; }
        //Do not sync with backend, used localy only
        [JsonIgnore]
        //public string TimeAgo => EndTimeStamp.ToLocalTime().Humanize(false);
        public string TimeAgo => EndTimeStamp.ToLocalTime().ToString();

        [JsonIgnore]
        public double DistanceConverted => (Distance * 1.60934);

        [JsonIgnore]
        public string Units => "km";

        [JsonIgnore]
        public string TotalDistance => DistanceConverted.ToString("f") + " " + Units;

        [JsonIgnore]
        public string TotalDistanceNoUnits => DistanceConverted.ToString("f");

        [JsonIgnore]
        public string StartTimeDisplay => RecordedTimeStamp.ToLocalTime().ToString("t");

        [JsonIgnore]
        public string EndTimeDisplay
            =>
                (Points?.Count).GetValueOrDefault() > 0
                    ? Points[Points.Count - 1].RecordedTimeStamp.ToLocalTime().ToString("t")
                    : string.Empty;

        public string Id { get; internal set; }

        //[JsonIgnore]
        //public IList<Photo> Photos { get; set; }

#endif
    }
}
