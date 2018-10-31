//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//See the NOTICE file distributed with this work for additional
//information regarding copyright ownership.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

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
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                ["latitude"] = Latitude,
                ["longitude"] = Longitude,
                ["altitude"] = Altitude
            };
            Dictionary<string, object> location = new Dictionary<string, object>()
                {["location"] = parameters};
            return location;
        }
    }
}