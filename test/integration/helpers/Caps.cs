using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Net.Integration.Tests.helpers
{
    public class Caps
    {
        public static AppiumOptions GetIosCaps(string app)
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.AutomationName, AutomationName.iOSXcuiTest);
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.DeviceName, "iPhone X");
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.PlatformVersion, "12.0");
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.App, app);
            capabilities.AddAdditionalAppiumOption(IOSMobileCapabilityType.LaunchTimeout, Env.InitTimeoutSec.TotalMilliseconds);

            return capabilities;
        }

        public static AppiumOptions GetAndroidUIAutomatorCaps(string app)
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.AutomationName, AutomationName.AndroidUIAutomator2);
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.DeviceName, "Android Emulator");
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.App, app);
            return capabilities;
        }

        public static AppiumOptions GetAndroidEspressoCaps(string app)
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.AutomationName, AutomationName.AndroidEspresso);
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.DeviceName, "Android Emulator");
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.App, app);
            return capabilities;
        }
    }
}