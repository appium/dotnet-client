using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Net.Integration.Tests.helpers
{
    public class Caps
    {
        public static AppiumOptions GetIosCaps(string app)
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, AutomationName.iOSXcuiTest);
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone X");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "12.0");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, app);
            capabilities.AddAdditionalCapability(IOSMobileCapabilityType.LaunchTimeout, Env.InitTimeoutSec.TotalMilliseconds);

            return capabilities;
        }

        public static AppiumOptions GetAndroidCaps(string app)
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, AutomationName.AndroidUIAutomator2);
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android Emulator");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, app);
            return capabilities;
        }
    }
}