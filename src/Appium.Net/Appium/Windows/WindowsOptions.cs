using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OpenQA.Selenium.Appium.Windows
{
    public class WindowsOptions : AppiumOptions
    {
        private const string AndroidOptionsCapabilityName = "windowsOptions";
        private const string PlatformNameValue = "windows";

        /// <summary>
        /// Initializes a new instance of the <see cref="AndroidOptions"/> class.
        /// </summary>
        public WindowsOptions()
        {
           this.PlatformName = PlatformNameValue;
        }

        /// <summary>
        /// Turn the capabilities into an desired capability
        /// </summary>
        /// <returns>A desired capability</returns>
        public override ICapabilities ToCapabilities()
        {
            RemoteSessionSettings remote = new RemoteSessionSettings();

            foreach (var keyVal in this.ToDictionary())
            {
                remote.AddMetadataSetting(keyVal.Key, keyVal.Value);
            }

            return remote;
        }

        /// <summary>
        /// Gets the vendor prefix to apply to Chromium-specific capability names.
        /// </summary>
        protected override string VendorPrefix
        {
            get { return "appium"; }
        }

        /// <summary>
        /// Gets the name of the capability used to store Appium options in
        /// an <see cref="ICapabilities"/> object.
        /// </summary>
        public override string CapabilityName
        {
            get { return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", this.VendorPrefix, AndroidOptionsCapabilityName); }
        }
    }
}
