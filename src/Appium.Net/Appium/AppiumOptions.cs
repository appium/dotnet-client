using System;
using System.Collections.Generic;
using System.Globalization;

namespace OpenQA.Selenium.Appium
{
    /// <summary>
    /// Generic browser options
    /// </summary>
    public class AppiumOptions : DriverOptions
    {
        private const string DeviceNameOption = "appium:deviceName";
        private const string PlatformVersionOption = "appium:platformVersion";
        protected string VendorPrefix = "appium";
        protected const string AutomationNameOption = "appium:automationName";
        protected internal const string AppOption = "appium:app";

        protected readonly Dictionary<string, object> additionalAppiumOptions = new Dictionary<string, object>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AppiumOptions"/> class.
        /// </summary>
        public AppiumOptions()
        {
            AddKnownCapabilityName(AutomationNameOption, "AutomationName property");
            AddKnownCapabilityName(DeviceNameOption, "DeviceName property");
            AddKnownCapabilityName(AppOption, "App property");
            AddKnownCapabilityName(PlatformVersionOption, "PlatformVersion property");
        }

        /// <summary>
        /// Gets or sets the AutoamtionName of the Appium browser's (e.g. Appium, Selendroid and so on) setting.
        /// </summary>
        public string AutomationName { get; set; }


        /// <summary>
        /// Gets or sets the DeviceName of the Appium browser's (e.g. Pixel 3XL, Galaxy S20 and so on) setting.
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// Gets or sets the Capability name used for the apllication setting.
        /// </summary>
        public string App { get; set; }

        /// <summary>
        /// Gets or sets the PlatformVersion of the Appium browser's (e.g. 10, 11 and so on) setting.
        /// </summary>
        public string PlatformVersion { get; set; }

        /// <summary>
        /// Provides a means to add additional capabilities not yet added as type safe options
        /// for the specific browser driver.
        /// </summary>
        /// <param name="optionName">The name of the capability to add.</param>
        /// <param name="optionValue">The value of the capability to add.</param>
        /// <exception cref="ArgumentException">
        /// thrown when attempting to add a capability for which there is already a type safe option, or
        /// when <paramref name="optionName"/> is <see langword="null"/> or the empty string.
        /// </exception>
        /// <remarks>Calling <see cref="AddAdditionalOption(string, object)"/>
        /// where <paramref name="optionName"/> has already been added will overwrite the
        /// existing value with the new value in <paramref name="optionValue"/>.
        /// </remarks>
        public void AddAdditionalOption(string optionName, object optionValue)
        {
            string name = optionName.Contains(":") ? optionName : $"{VendorPrefix}:{optionName}";
            this.additionalAppiumOptions[optionName] = optionValue;
        }

        /// <summary>
        /// Add new capabilities
        /// </summary>
        /// <param name="capabilityName">Capability name</param>
        /// <param name="capabilityValue">Capabilities value, which cannot be null or empty</param>
        [Obsolete(
            "Use the temporary AddAdditionalOption method or the browser-specific method for adding additional options")]
        public override void AddAdditionalCapability(string capabilityName, object capabilityValue)
        {
            if (string.IsNullOrEmpty(capabilityName))
            {
                throw new ArgumentException("Capability name may not be null an empty string.", "capabilityName");
            }

            this.AddAdditionalOption(capabilityName, capabilityValue);
        }

        /// <summary>
        /// Turn the capabilities into an desired capability
        /// </summary>
        /// <returns>A desired capability</returns>
        public override ICapabilities ToCapabilities()
        {
            var capabilities = GenerateDesiredCapabilities(true);

            foreach (var option in BuildAppiumOptionsDictionary())
            {
                capabilities.SetCapability(option.Key, option.Value);
            }

            return capabilities;
        }

        protected virtual Dictionary<string, object> BuildAppiumKnownOptionsDictionary()
        {
            var knownOptions = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(App))
            {
                knownOptions[AppOption] = App;
            }

            if (!string.IsNullOrEmpty(AutomationName))
            {
                knownOptions[AutomationNameOption] = AutomationName;
            }

            if (!string.IsNullOrEmpty(DeviceName))
            {
                knownOptions[DeviceNameOption] = DeviceName;
            }

            if (!string.IsNullOrEmpty(PlatformName))
            {
                knownOptions[PlatformVersionOption] = PlatformName;
            }

            return knownOptions;
        }

        protected Dictionary<string, object> BuildAppiumOptionsDictionary()
        {
            var appiumOptions = BuildAppiumKnownOptionsDictionary();

            foreach (var pair in additionalAppiumOptions)
            {
                appiumOptions.Add(pair.Key, pair.Value);
            }

            return appiumOptions;
        }

        public Dictionary<string, object> ToDictionary()
        {
            var writeable = GenerateDesiredCapabilities(false);

            return ((AppiumCapabilities)writeable).ToDictionary();
        }
    }
}