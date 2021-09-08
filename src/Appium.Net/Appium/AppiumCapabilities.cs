using System.Collections.Generic;
using System.Reflection;
using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium
{
    /// <summary>
    /// Appium capabilities
    /// </summary>
    public class AppiumCapabilities : DriverOptions
    {
        public override void AddAdditionalCapability(string capabilityName, object capabilityValue)
        {
            this.AddAdditionalOption(capabilityName, capabilityValue);
        }

        public override ICapabilities ToCapabilities()
        {
            return GenerateDesiredCapabilities(true).AsReadOnly();
        }
    }
}