using System;
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
            capabilities.DeviceName = Environment.GetEnvironmentVariable("IOS_DEVICE_NAME") ?? "iPhone 17";
            capabilities.PlatformVersion =  Environment.GetEnvironmentVariable("IOS_VERSION") ?? "26.0";
            capabilities.App = app;
            capabilities.AddAdditionalAppiumOption(IOSMobileCapabilityType.LaunchTimeout, Env.InitTimeoutSec.TotalMilliseconds);

            // Allow slow WDA responses on CI while staying below the client command timeout.
            capabilities.AddAdditionalAppiumOption("wdaConnectionTimeout", TimeSpan.FromMinutes(5).TotalMilliseconds);

            // Use the simulator that CI has already booted and waited for.
            var udid = Environment.GetEnvironmentVariable("IOS_UDID");
            if (!string.IsNullOrEmpty(udid))
            {
                capabilities.AddAdditionalAppiumOption(MobileCapabilityType.Udid, udid);
            }

            if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("LOCAL_PREBUILT_WDA")))
            {
                capabilities.AddAdditionalAppiumOption("usePreinstalledWDA", true);
                capabilities.AddAdditionalAppiumOption("prebuiltWDAPath", Environment.GetEnvironmentVariable("LOCAL_PREBUILT_WDA"));
            }

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

        public static AppiumOptions GetAndroidUIAutomatorCaps()
        {
            var capabilities = new AppiumOptions();
            capabilities.AutomationName = AutomationName.AndroidUIAutomator2;
            capabilities.DeviceName = "Android Emulator";
            return capabilities;
        }

        public static AppiumOptions GetAndroidEspressoCaps(string app)
        {
            var capabilities = new AppiumOptions();
            capabilities.AutomationName = AutomationName.AndroidEspresso;
            capabilities.DeviceName = "Android Emulator";
            capabilities.App = app;
            capabilities.AddAdditionalAppiumOption("enforceAppInstall", true);
            return capabilities;
        }
    }
}