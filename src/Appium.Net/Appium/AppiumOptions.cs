using Newtonsoft.Json;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace OpenQA.Selenium.Appium
{
    /// <summary>
    /// Generic browser options
    /// </summary>
    public class AppiumOptions : DriverOptions
    {
        /// <summary>
        /// The dictionary of capabilities
        /// </summary>
        private readonly AppiumCapabilities capabilities = new AppiumCapabilities();

        /// <summary>
        /// Add new capabilities
        /// </summary>
        /// <param name="capabilityName">Capability name</param>
        /// <param name="capabilityValue">Capabilities value, which cannot be null or empty</param>
        public override void AddAdditionalCapability(string capabilityName, object capabilityValue)
        {
            if (string.IsNullOrEmpty(capabilityName))
            {
                throw new ArgumentException("Capability name may not be null an empty string.", "capabilityName");
            }

            var writeable = this.GenerateDesiredCapabilities(true);
            writeable.SetCapability(capabilityName, capabilityValue);
        }

        /// <summary>
        /// Turn the capabilities into an desired capability
        /// </summary>
        /// <returns>A desired capability</returns>
        public override ICapabilities ToCapabilities()
        {
            RemoteSessionSettings remote = new RemoteSessionSettings();

            foreach(var keyVal in this.ToDictionary())
            {
                remote.AddMetadataSetting(keyVal.Key, keyVal.Value);
            }

            return remote;
        }

        public IDictionary<string, object> ToDictionary()
        {
            var writeable = this.GenerateDesiredCapabilities(true);
            return (writeable.AsReadOnly() as ReadOnlyDesiredCapabilities).ToDictionary();
        }
    }
}