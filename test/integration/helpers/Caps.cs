using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Net.Integration.Tests.helpers
{
    public class Caps
    {
        public static AppiumOptions GetIosCaps(string app)
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalOption(MobileCapabilityType.AutomationName, AutomationName.iOSXcuiTest);
            capabilities.AddAdditionalOption(MobileCapabilityType.DeviceName, "iPhone X");
            capabilities.AddAdditionalOption(MobileCapabilityType.PlatformVersion, "12.0");
            capabilities.AddAdditionalOption(MobileCapabilityType.App, app);
            capabilities.AddAdditionalOption(IOSMobileCapabilityType.LaunchTimeout, Env.InitTimeoutSec.TotalMilliseconds);

            return capabilities;
        }

        public static AppiumOptions GetAndroidUIAutomatorCaps(string app)
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalOption(MobileCapabilityType.AutomationName, AutomationName.AndroidUIAutomator2);
            capabilities.AddAdditionalOption(MobileCapabilityType.DeviceName, "Android Emulator");
            capabilities.AddAdditionalOption(MobileCapabilityType.App, app);
            return capabilities;
        }

        public static AppiumOptions GetAndroidEspressoCaps(string app)
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalOption(MobileCapabilityType.AutomationName, AutomationName.AndroidEspresso);
            capabilities.AddAdditionalOption(MobileCapabilityType.DeviceName, "Android Emulator");
            capabilities.AddAdditionalOption(MobileCapabilityType.App, app);
            return capabilities;
        }
    }
}