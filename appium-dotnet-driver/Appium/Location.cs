using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenQA.Selenium.Appium
{
        public class Location
        {
                public Location()
                {
                        Latitude = 0.0;
                        Longitude = 0.0;
                        Altitude = 0.0;
                }

                [JsonProperty(PropertyName = "latitude")]
                public double Latitude { get; set; }

                [JsonProperty(PropertyName = "longitude")]
                public double Longitude { get; set; }

                [JsonProperty(PropertyName = "altitude")]
                public double Altitude { get; set; }

                public Dictionary<string, object> ToDictionary()
                {
                        Dictionary<string, object> parameters = new Dictionary<string, object>();
                        parameters.Add("latitude", Latitude);
                        parameters.Add("longitude", Longitude);
                        parameters.Add("altitude", Altitude);
                        return parameters;
                }
        }
}

