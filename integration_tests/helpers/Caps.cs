using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;

namespace Appium.Integration.Tests.Helpers
{
    public class Caps
    {
        public static AppiumOptions GetIOSCaps(string app)
        {
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, AutomationName.iOSXcuiTest);
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone X");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "12.0");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, app);
            capabilities.AddAdditionalCapability(IOSMobileCapabilityType.LaunchTimeout, Env.InitTimeoutSec.TotalMilliseconds);

            return capabilities;
        }

        public static AppiumOptions GetAndroidCaps(string app)
        {
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, AutomationName.AndroidUIAutomator2);
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android Emulator");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, app);
            return capabilities;
        }

        public static AppiumOptions getAndroid27Caps(string app)
        {
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "8.1.0");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android Emulator");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, app);
            return capabilities;
        }

        public static AppiumOptions getSelendroid19Caps(string app)
        {
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(CapabilityType.BrowserName, "");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "4.4.2");
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, "selendroid");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android Emulator");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, app);
            return capabilities;
        }
    }
}