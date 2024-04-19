using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OpenQA.Selenium.Appium
{
    /// <summary>
    /// Generic driver options
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
            AddKnownCapabilityName(AutomationNameOption, "AutomationName property");
            AddKnownCapabilityName(DeviceNameOption, "DeviceName property");
            AddKnownCapabilityName(AppOption, "Application property");
            AddKnownCapabilityName(PlatformVersionOption, "PlatformVersion property");
            AddKnownCapabilityName("app", "Application property");
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
        /// Gets or sets the Capability name used for the application setting.
        /// </summary>
        public string App { get; set; }

        /// <summary>
        /// Gets or sets the PlatformVersion of the Appium browser's (e.g. 10, 11 and so on) setting.
        /// </summary>
        public string PlatformVersion { get; set; }

        /// <summary>
        /// Gets or sets the Browser name of the Appium browser's (e.g. Chrome, Safari and so on) setting.
        /// </summary>
        public new string BrowserName
        {
            get { return base.BrowserName; }
            set { base.BrowserName = value; }
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
            string name = optionName.Contains(':') ? optionName : $"{VendorPrefix}:{optionName}";
            ValidateCapabilityName(name);
            additionalAppiumOptions[name] = optionValue;
        }

        /// <summary>
        /// This method is overridden to provide a clear exception indicating that 
        /// the <see cref="AddAdditionalAppiumOption"/> method should be used for adding additional options.
        /// </summary>
        /// <param name="optionName">The name of the additional option.</param>
        /// <param name="optionValue">The value of the additional option.</param>
        /// <exception cref="NotImplementedException">
        /// Thrown to indicate that <see cref="AddAdditionalAppiumOption"/> should be used for adding additional options.
        /// </exception>
        public override void AddAdditionalOption(string optionName, object optionValue)
        {
            throw new NotImplementedException("Use the AddAdditionalAppiumOption method for adding additional options");
        }

        /// <summary>
        /// Turn the capabilities into a desired capability
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

        /// <summary>
        /// Builds a dictionary containing known Appium options with their corresponding values.
        /// </summary>
        /// <returns>A dictionary representing known Appium options and their values.</returns>
        protected virtual Dictionary<string, object> BuildAppiumKnownOptionsDictionary()
        {
            Dictionary<string, object> knownOptions = new Dictionary<string, object>();

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

            if (!string.IsNullOrEmpty(PlatformVersion))
            {
                knownOptions[PlatformVersionOption] = PlatformVersion;
            }

            return knownOptions;
        }

        private Dictionary<string, object> BuildAppiumOptionsDictionary()
        {
            var appiumOptions = BuildAppiumKnownOptionsDictionary();

            foreach (KeyValuePair<string, object> pair in additionalAppiumOptions)
            {
                appiumOptions.Add(pair.Key, pair.Value);
            }

            return appiumOptions;
        }

        /// <summary>
        /// Converts the current instance of <see cref="AppiumOptions"/> to a ReadOnlyDictionary.
        /// </summary>
        /// <returns>A ReadOnlyDictionary representation of the <see cref="AppiumOptions"/>.</returns>
        public IDictionary<string, object> ToDictionary()
        {
            IWritableCapabilities writeable = GenerateDesiredCapabilities(true);
            var baseDict = (writeable.AsReadOnly() as ReadOnlyDesiredCapabilities).ToDictionary();
            return MergeOptionsDictionary(baseDict);
        }

        /// <summary>
        /// Merges the provided <see cref="IWritableCapabilities"/> dictionary with the <see cref="AppiumOptions"/> dictionary.
        /// </summary>
        /// <param name="baseDict">The base <see cref="IWritableCapabilities"/> dictionary.</param>
        /// <returns>A ReadOnlyDictionary representing the merged <see cref="AppiumOptions"/> dictionary.</returns>
        private IDictionary<string, object> MergeOptionsDictionary(IDictionary<string, object> baseDict)
        {
            Dictionary<string, object> appiumOptionsDict = BuildAppiumOptionsDictionary();
            var mergedDict = appiumOptionsDict.Concat(baseDict).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return new ReadOnlyDictionary<string, object>(mergedDict);
        }

    }
}
