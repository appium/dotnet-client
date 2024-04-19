using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Net.Integration.Tests.helpers
{
    public class Caps
    {
        public static AppiumOptions GetIosCaps(string app)
        {
            var capabilities = new AppiumOptions();
            capabilities.AutomationName = AutomationName.iOSXcuiTest;
            capabilities.DeviceName = "iPhone 15";
            capabilities.PlatformVersion = "17.0";
            capabilities.App = app;
            capabilities.AddAdditionalAppiumOption(IOSMobileCapabilityType.LaunchTimeout, Env.InitTimeoutSec.TotalMilliseconds);

            return capabilities;
        }

        public static AppiumOptions GetAndroidUIAutomatorCaps(string app)
        {
            var capabilities = new AppiumOptions();
            capabilities.AutomationName = AutomationName.AndroidUIAutomator2;
            capabilities.DeviceName = "Android Emulator";
            capabilities.App = app;
            return capabilities;
        }

        public static AppiumOptions GetAndroidEspressoCaps(string app)
        {
            var capabilities = new AppiumOptions();
            capabilities.AutomationName = AutomationName.AndroidEspresso;
            capabilities.DeviceName = "Android Emulator";
            capabilities.App = app;
            return capabilities;
        }
    }
}