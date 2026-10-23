//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//See the NOTICE file distributed with this work for additional
//information regarding copyright ownership.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using OpenQA.Selenium.Appium.Enums;
using System;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.iOS
{
    /// <summary>
    /// Options for the Appium XCUITest driver.
    /// Sets <c>platformName</c> to iOS and <c>appium:automationName</c> to XCUITest,
    /// and exposes the most commonly used driver capabilities as typed properties.
    /// Read: https://appium.github.io/appium-xcuitest-driver/latest/reference/capabilities/
    /// </summary>
    /// <remarks>
    /// Properties left unset are not sent to the server. Capabilities without a typed property
    /// can still be passed with <see cref="AppiumOptions.AddAdditionalAppiumOption(string, object)"/>.
    /// </remarks>
    public class XCUITestOptions : AppiumOptions
    {
        private const string UdidOption = "appium:udid";
        private const string NoResetOption = "appium:noReset";
        private const string FullResetOption = "appium:fullReset";
        private const string NewCommandTimeoutOption = "appium:newCommandTimeout";
        private const string LanguageOption = "appium:language";
        private const string LocaleOption = "appium:locale";
        private const string BundleIdOption = "appium:bundleId";
        private const string AutoAcceptAlertsOption = "appium:autoAcceptAlerts";
        private const string AutoDismissAlertsOption = "appium:autoDismissAlerts";
        private const string WdaLocalPortOption = "appium:wdaLocalPort";
        private const string WdaLaunchTimeoutOption = "appium:wdaLaunchTimeout";
        private const string UseNewWdaOption = "appium:useNewWDA";
        private const string UsePrebuiltWdaOption = "appium:usePrebuiltWDA";
        private const string UpdatedWdaBundleIdOption = "appium:updatedWDABundleId";
        private const string XcodeOrgIdOption = "appium:xcodeOrgId";
        private const string XcodeSigningIdOption = "appium:xcodeSigningId";
        private const string ShowXcodeLogOption = "appium:showXcodeLog";
        private const string SimulatorStartupTimeoutOption = "appium:simulatorStartupTimeout";

        /// <summary>
        /// Initializes a new instance of the <see cref="XCUITestOptions"/> class.
        /// </summary>
        public XCUITestOptions() : base()
        {
            PlatformName = MobilePlatform.IOS;
            AutomationName = OpenQA.Selenium.Appium.Enums.AutomationName.iOSXcuiTest;

            AddKnownCapabilityName(UdidOption, "Udid property");
            AddKnownCapabilityName(NoResetOption, "NoReset property");
            AddKnownCapabilityName(FullResetOption, "FullReset property");
            AddKnownCapabilityName(NewCommandTimeoutOption, "NewCommandTimeout property");
            AddKnownCapabilityName(LanguageOption, "Language property");
            AddKnownCapabilityName(LocaleOption, "Locale property");
            AddKnownCapabilityName(BundleIdOption, "BundleId property");
            AddKnownCapabilityName(AutoAcceptAlertsOption, "AutoAcceptAlerts property");
            AddKnownCapabilityName(AutoDismissAlertsOption, "AutoDismissAlerts property");
            AddKnownCapabilityName(WdaLocalPortOption, "WdaLocalPort property");
            AddKnownCapabilityName(WdaLaunchTimeoutOption, "WdaLaunchTimeout property");
            AddKnownCapabilityName(UseNewWdaOption, "UseNewWda property");
            AddKnownCapabilityName(UsePrebuiltWdaOption, "UsePrebuiltWda property");
            AddKnownCapabilityName(UpdatedWdaBundleIdOption, "UpdatedWdaBundleId property");
            AddKnownCapabilityName(XcodeOrgIdOption, "XcodeOrgId property");
            AddKnownCapabilityName(XcodeSigningIdOption, "XcodeSigningId property");
            AddKnownCapabilityName(ShowXcodeLogOption, "ShowXcodeLog property");
            AddKnownCapabilityName(SimulatorStartupTimeoutOption, "SimulatorStartupTimeout property");
        }

        /// <summary>
        /// Gets or sets the unique identifier of the real device or simulator to run the session on.
        /// </summary>
        public string Udid { get; set; }

        /// <summary>
        /// Gets or sets whether to keep the application state between sessions.
        /// </summary>
        public bool? NoReset { get; set; }

        /// <summary>
        /// Gets or sets whether to uninstall the application and reset the simulator before and after the session.
        /// </summary>
        public bool? FullReset { get; set; }

        /// <summary>
        /// Gets or sets how long the server waits for a new command before ending the session.
        /// Sent to the server in whole seconds.
        /// </summary>
        public TimeSpan? NewCommandTimeout { get; set; }

        /// <summary>
        /// Gets or sets the language to set on the simulator (e.g. fr).
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the locale to set on the simulator (e.g. fr_CA).
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Gets or sets the bundle identifier of the application under test (e.g. io.appium.TestApp).
        /// </summary>
        public string BundleId { get; set; }

        /// <summary>
        /// Gets or sets whether to accept all system alerts automatically.
        /// </summary>
        public bool? AutoAcceptAlerts { get; set; }

        /// <summary>
        /// Gets or sets whether to dismiss all system alerts automatically.
        /// </summary>
        public bool? AutoDismissAlerts { get; set; }

        /// <summary>
        /// Gets or sets the local port used to communicate with WebDriverAgent.
        /// Set a distinct value per session when running sessions in parallel.
        /// </summary>
        public int? WdaLocalPort { get; set; }

        /// <summary>
        /// Gets or sets how long to wait for WebDriverAgent to start.
        /// Sent to the server in milliseconds.
        /// </summary>
        public TimeSpan? WdaLaunchTimeout { get; set; }

        /// <summary>
        /// Gets or sets whether to uninstall and rebuild WebDriverAgent before the session.
        /// </summary>
        public bool? UseNewWda { get; set; }

        /// <summary>
        /// Gets or sets whether to skip building WebDriverAgent and use an already built one.
        /// </summary>
        public bool? UsePrebuiltWda { get; set; }

        /// <summary>
        /// Gets or sets the bundle identifier to sign WebDriverAgent with on a real device.
        /// </summary>
        public string UpdatedWdaBundleId { get; set; }

        /// <summary>
        /// Gets or sets the Apple developer team identifier used to sign WebDriverAgent on a real device.
        /// </summary>
        public string XcodeOrgId { get; set; }

        /// <summary>
        /// Gets or sets the signing identity used to sign WebDriverAgent on a real device (e.g. Apple Development).
        /// </summary>
        public string XcodeSigningId { get; set; }

        /// <summary>
        /// Gets or sets whether to include the Xcode build output of WebDriverAgent in the server log.
        /// </summary>
        public bool? ShowXcodeLog { get; set; }

        /// <summary>
        /// Gets or sets how long to wait for the simulator to boot.
        /// Sent to the server in milliseconds.
        /// </summary>
        public TimeSpan? SimulatorStartupTimeout { get; set; }

        /// <inheritdoc/>
        protected override Dictionary<string, object> BuildAppiumKnownOptionsDictionary()
        {
            var knownOptions = base.BuildAppiumKnownOptionsDictionary();

            OptionValues.AddIfSet(knownOptions, UdidOption, Udid);
            OptionValues.AddIfSet(knownOptions, NoResetOption, NoReset);
            OptionValues.AddIfSet(knownOptions, FullResetOption, FullReset);
            OptionValues.AddIfSet(knownOptions, NewCommandTimeoutOption, OptionValues.ToSeconds(NewCommandTimeout));
            OptionValues.AddIfSet(knownOptions, LanguageOption, Language);
            OptionValues.AddIfSet(knownOptions, LocaleOption, Locale);
            OptionValues.AddIfSet(knownOptions, BundleIdOption, BundleId);
            OptionValues.AddIfSet(knownOptions, AutoAcceptAlertsOption, AutoAcceptAlerts);
            OptionValues.AddIfSet(knownOptions, AutoDismissAlertsOption, AutoDismissAlerts);
            OptionValues.AddIfSet(knownOptions, WdaLocalPortOption, WdaLocalPort);
            OptionValues.AddIfSet(knownOptions, WdaLaunchTimeoutOption, OptionValues.ToMilliseconds(WdaLaunchTimeout));
            OptionValues.AddIfSet(knownOptions, UseNewWdaOption, UseNewWda);
            OptionValues.AddIfSet(knownOptions, UsePrebuiltWdaOption, UsePrebuiltWda);
            OptionValues.AddIfSet(knownOptions, UpdatedWdaBundleIdOption, UpdatedWdaBundleId);
            OptionValues.AddIfSet(knownOptions, XcodeOrgIdOption, XcodeOrgId);
            OptionValues.AddIfSet(knownOptions, XcodeSigningIdOption, XcodeSigningId);
            OptionValues.AddIfSet(knownOptions, ShowXcodeLogOption, ShowXcodeLog);
            OptionValues.AddIfSet(knownOptions, SimulatorStartupTimeoutOption, OptionValues.ToMilliseconds(SimulatorStartupTimeout));

            return knownOptions;
        }
    }
}
