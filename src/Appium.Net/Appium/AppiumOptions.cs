using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium
{
    /// <summary>
    /// Generic browser options
    /// </summary>
    public class AppiumOptions : DriverOptions
    {
        private const string VendorPrefix = "appium";
        private const string AutomationNameOption = "appium:automationName";
        private const string DeviceNameOption = "appium:deviceName";
        private const string AppOption = "appium:app";
        private const string PlatformVersionOption = "appium:platformVersion";
        
        private readonly Dictionary<string, object> additionalAppiumOptions = new Dictionary<string, object>();


        /// <summary>
        /// Initializes a new instance of the <see cref="AppiumOptions"/> class.
        /// </summary>
        public AppiumOptions() : base()
        {
            this.AddKnownCapabilityName(AppiumOptions.AutomationNameOption, "AutomationName property");
            this.AddKnownCapabilityName(AppiumOptions.DeviceNameOption, "DeviceName property");
            this.AddKnownCapabilityName(AppiumOptions.AppOption, "Application property");
            this.AddKnownCapabilityName(AppiumOptions.PlatformVersionOption, "PlatformVersion property");
            this.AddKnownCapabilityName("app", "Application property");
        }

        /// <summary>
        /// Gets or sets the AutoamtionName of the Appium browser's (e.g. Appium, Selendroid and so on) setting.
        /// </summary>
        public string AutomationName  {get; set;}


        /// <summary>
        /// Gets or sets the DeviceName of the Appium browser's (e.g. Pixel 3XL, Galaxy S20 and so on) setting.
        /// </summary>
        public string DeviceName  {get; set;}

        /// <summary>
        /// Gets or sets the Capability name used for the apllication setting.
        /// </summary>
        public string App {get; set;}

        /// <summary>
        /// Gets or sets the PlatformVersion of the Appium browser's (e.g. 10, 11 and so on) setting.
        /// </summary>
        public string PlatformVersion { get; set; }

        /// <summary>
        /// Provides a means to add additional capabilities not yet added as type safe options
        /// for the Appium driver.
        /// </summary>
        /// <param name="optionName">The name of the capability to add.</param>
        /// <param name="optionValue">The value of the capability to add.</param>
        /// <exception cref="ArgumentException">
        /// thrown when attempting to add a capability for which there is already a type safe option, or
        /// when <paramref name="optionName"/> is <see langword="null"/> or the empty string.
        /// </exception>
        /// <remarks>Calling <see cref="AddAdditionalAppiumOption(string, object)"/>
        /// where <paramref name="optionName"/> has already been added will overwrite the
        /// existing value with the new value in <paramref name="optionValue"/>.
        /// Calling this method adds capabilities to the Appium-specific options object passed to
        /// webdriver executable.</remarks>
        public void AddAdditionalAppiumOption(string optionName, object optionValue)
        {
            string name;

            if (optionName.Contains(":"))
            {
                name = optionName;
            }
            else
            {
                name = $"{VendorPrefix}:{optionName}";
            }

            this.ValidateCapabilityName(name);
            this.additionalAppiumOptions[name] = optionValue;
        }

        /// <summary>
        /// Add new capabilities
        /// </summary>
        /// <param name="capabilityName">Capability name</param>
        /// <param name="capabilityValue">Capabilities value, which cannot be null or empty</param>
        [Obsolete("Use the temporary AddAdditionalOption method or the AddAdditionalAppiumOption method for adding additional options")]
        public override void AddAdditionalCapability(string capabilityName, object capabilityValue)
        {
            this.AddAdditionalAppiumOption(capabilityName, capabilityValue);
        }

        /// <summary
        /// Turn the capabilities into an desired capability
        /// </summary>
        /// <returns>A desired capability</returns>
        public override ICapabilities ToCapabilities()
        {
            var capabilities = this.GenerateDesiredCapabilities(true);

            foreach(var option in this.BuildAppiumOptionsDictionary())
            {
                capabilities.SetCapability(option.Key, option.Value);
            }

            return capabilities;
        }

        protected virtual Dictionary<string, object> BuildAppiumKnownOptionsDictionary()
        {
            Dictionary<string, object> knownOptions = new Dictionary<string, object>();

            if (!string.IsNullOrEmpty(this.App))
            {
                knownOptions[AppOption] = this.App;
            }

            if (!string.IsNullOrEmpty(this.AutomationName))
            {
                knownOptions[AutomationNameOption] = this.AutomationName;
            }

            if (!string.IsNullOrEmpty(this.DeviceName))
            {
                knownOptions[DeviceNameOption] = this.DeviceName;
            }

            if (!string.IsNullOrEmpty(this.PlatformVersion))
            {
                knownOptions[PlatformVersionOption] = this.PlatformVersion;
            }

            return knownOptions;

        }

        private Dictionary<string, object> BuildAppiumOptionsDictionary()
        {
            var appiumOptions = BuildAppiumKnownOptionsDictionary();

            foreach (KeyValuePair<string, object> pair in this.additionalAppiumOptions)
            {
                appiumOptions.Add(pair.Key, pair.Value);
            }

            return appiumOptions;
        }

        public IDictionary<string, object> ToDictionary()
        {
            var writeable = this.GenerateDesiredCapabilities(true);
            return (writeable.AsReadOnly() as ReadOnlyDesiredCapabilities).ToDictionary();
        }
    }
}