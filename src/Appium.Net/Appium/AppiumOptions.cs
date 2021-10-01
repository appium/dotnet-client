using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium
{

    /// <summary>
    /// Abstract class to manage options specific to Appium-based browsers.
    /// </summary>
    public class AppiumOptions : DriverOptions
    {

        // private const string AutomationAppiumOption = "appium:automationName";
        // private const string DeviceAppiumOption = "appium:deviceName";
        // private const string AppAppiumOption = "appium:app";
        private const string PlatformVersionAppiumOption = "appium:platformVersion";

        // private string automationName;
        //  private string deviceName;
        // private string app;
        private string platformVersion;

        private Dictionary<string, object> additionalAppiumOptions = new Dictionary<string, object>();


        /// <summary>
        /// Initializes a new instance of the <see cref="AppiumOptions"/> class.
        /// </summary>
        /// 

        public AppiumOptions() : base()
        {
            // this.AddKnownCapabilityName(this.CapabilityName, "current AppiumOptions class instance");
            // this.AddKnownCapabilityName(AppiumOptions.AutomationAppiumOption, "AutomationName property");
            // this.AddKnownCapabilityName(AppiumOptions.DeviceAppiumOption, "DeviceName property");
            // this.AddKnownCapabilityName(AppiumOptions.AppAppiumOption, "App property");
            this.AddKnownCapabilityName(AppiumOptions.PlatformVersionAppiumOption, "PlatformVersion property");
        }

        /// <summary>
        /// Gets the vendor prefix to apply to Appium-specific capability names.
        /// </summary>
        protected virtual string VendorPrefix { get; }

        /// <summary>
        /// Gets the name of the capability used to store Appium options in
        /// an <see cref="ICapabilities"/> object.
        /// </summary>
        public virtual string CapabilityName { get; }


        /// <summary>
        /// Gets or sets the AutoamtionName of the Appium browser's (e.g. Appium, Selendroid and so on) setting.
        /// </summary>
        //public string AutomationName
        //{
        //    get { return this.automationName; }
        //    set { this.automationName = value; }
        //}

        /// <summary>
        /// Gets or sets the DeviceName of the Appium browser's (e.g. Pixel 3XL, Galaxy S20 and so on) setting.
        /// </summary>
        //public string DeviceName
        //{
        //    get { return this.deviceName; }
        //    set { this.deviceName = value; }
        //}

        /// <summary>
        /// Gets or sets the Capability name used for the apllication setting.
        /// </summary>
        //public string App
        //{
        //    get { return this.app; }
        //    set { this.app = value; }
        //}

        /// <summary>
        /// Gets or sets the PlatformVersion of the Appium browser's (e.g. 10, 11 and so on) setting.
        /// </summary>
        public string PlatformVersion
        {
            get { return this.platformVersion; }
            set { this.platformVersion = value; }
        }

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
            this.ValidateCapabilityName(optionName);
            this.additionalAppiumOptions[optionName] = optionValue;
        }

        /// <summary>
        /// Add new capabilities
        /// </summary>
        /// <param name="capabilityName">Capability name</param>
        /// <param name="capabilityValue">Capabilities value, which cannot be null or empty</param>
        [Obsolete("Use the temporary AddAdditionalOption method or the AddAdditionalChromeOption method for adding additional options")]
        public override void AddAdditionalCapability(string capabilityName, object capabilityValue)
        {

            this.AddAdditionalCapability(capabilityName, capabilityValue, false);
        }

        [Obsolete("Use the temporary AddAdditionalOption method or the AddAdditionalChromeOption method for adding additional options")]
        public void AddAdditionalCapability(string capabilityName, object capabilityValue, bool isGlobalCapability)
        {
            if (isGlobalCapability)
            {
                this.AddAdditionalOption(capabilityName, capabilityValue);
            }
            else
            {
                this.AddAdditionalAppiumOption(capabilityName, capabilityValue);
            }
        }

        /// <summary>
        /// Turn the capabilities into an desired capability
        /// </summary>
        /// <returns>A desired capability</returns>
        public override ICapabilities ToCapabilities()
        {

            Dictionary<string, object> appiumOptions = this.BuildAppiumOptionsDictionary();

            IWritableCapabilities capabilities = this.GenerateDesiredCapabilities(false);
            capabilities.SetCapability(this.CapabilityName, appiumOptions);

            AddVendorSpecificAppiumCapabilities(capabilities);

            return capabilities.AsReadOnly();

            //RemoteSessionSettings remote = new RemoteSessionSettings();

            //foreach (var keyVal in this.ToDictionary())
            //{
            //    remote.AddMetadataSetting(keyVal.Key, keyVal.Value);
            //}

            //return remote;
        }

        /// <summary>
        /// Adds vendor-specific capabilities for Appium-based browsers.
        /// </summary>
        /// <param name="capabilities">The capabilities to add.</param>
        protected virtual void AddVendorSpecificAppiumCapabilities(IWritableCapabilities capabilities)
        {
        }


        protected virtual Dictionary<string, object> BuildAppiumOptionsDictionary()
        {
            Dictionary<string, object> appiumOptions = new Dictionary<string, object>();


            //if (!string.IsNullOrEmpty(this.automationName))
            //{
            //    appiumOptions[AutomationAppiumOption] = this.automationName;
            //}

            //if (!string.IsNullOrEmpty(this.deviceName))
            //{
            //    appiumOptions[DeviceAppiumOption] = this.deviceName;
            //}
            //if (!string.IsNullOrEmpty(this.app))
            //{
            //    appiumOptions[AppAppiumOption] = this.app;
            //}
            if (!string.IsNullOrEmpty(this.platformVersion))
            {
                appiumOptions[PlatformVersionAppiumOption] = this.platformVersion;
            }




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