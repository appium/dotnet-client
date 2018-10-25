using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;

namespace Appium.Integration.Tests.Helpers
{
    public class Caps
    {
        public static AppiumOptions getIos102Caps(string app)
        {
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "10.2");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone Simulator");
            capabilities.AddAdditionalCapability(MobileCapabilityType.AppiumVersion, "1.7.1");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, app);
            return capabilities;
        }

        public static AppiumOptions getIos112Caps(string app)
        {
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "11.2");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPad Air 2");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, app);
            return capabilities;
        }

        public static AppiumOptions getIos92Caps(string app)
        {
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "9.2");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "iPhone Simulator");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, app);
            return capabilities;
        }

        public static AppiumOptions getAndroid501Caps(string app)
        {
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(CapabilityType.BrowserName, "");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "5.0.1");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "io.appium.android.apis");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, ".Apidemos");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android Emulator");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, app);
            return capabilities;
        }

        public static AppiumOptions getAndroid19Caps(string app)
        {
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(CapabilityType.BrowserName, "");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "4.4.2");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "Android Emulator");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, app);
            return capabilities;
        }

        public static AppiumOptions getAndroid27Caps(string app)
        {
            AppiumOptions capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(CapabilityType.BrowserName, "");
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