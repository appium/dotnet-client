using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium
{
    public class NetworkConnectionSetting
    {
        private const int AirplaneMode = 1;
        private const int WiFi = 2;
        private const int Data = 4;

        public int Value { get; private set; }

        public NetworkConnectionSetting(bool airplaneMode, bool wifi, bool data)
        {
            int a = airplaneMode ? NetworkConnectionSetting.AirplaneMode : 0;
            int b = wifi ? NetworkConnectionSetting.WiFi : 0;
            int c = data ? NetworkConnectionSetting.Data : 0;

            Value = a | b | c;
        }

        public NetworkConnectionSetting(int bitmask)
        {
            Value = bitmask;
        }

        public bool AirplaneModeEnabled { get { return (Value & AirplaneMode) != 0; } }

        public bool WifiEnabled { get { return (Value & WiFi) != 0; } }

        public bool DataEnabled { get { return (Value & Data) != 0; } }

        public void setAirplaneMode(bool enable)
        {
            if (enable)
            {
                Value = Value | AirplaneMode;
            }
            else
            {
                Value = Value & ~AirplaneMode;
            }
        }

        public void setWifi(bool enable)
        {
            if (enable)
            {
                Value = Value | WiFi;
            }
            else
            {
                Value = Value & ~WiFi;
            }
        }

        public void setData(bool enable)
        {
            if (enable)
            {
                Value = Value | Data;
            }
            else
            {
                Value = Value & ~Data;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Int32) 
            {
                return Value == (Int32) obj;
            }

            if (obj is NetworkConnectionSetting) {
                return Value == ((NetworkConnectionSetting)obj).Value;
            }
            else 
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public override string ToString()
        {
            return String.Format("{ AirplaneMode: {0}, Wifi: {1}, Data: {2}}", AirplaneModeEnabled, WifiEnabled, DataEnabled);
        }
    }
}
