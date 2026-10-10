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

namespace OpenQA.Selenium.Appium.Android
{
    /// <summary>
    /// Options for the Appium UiAutomator2 driver.
    /// Sets <c>platformName</c> to Android and <c>appium:automationName</c> to UiAutomator2,
    /// and exposes the most commonly used driver capabilities as typed properties.
    /// Read: https://github.com/appium/appium-uiautomator2-driver#capabilities
    /// </summary>
    /// <remarks>
    /// Properties left unset are not sent to the server. Capabilities without a typed property
    /// can still be passed with <see cref="AppiumOptions.AddAdditionalAppiumOption(string, object)"/>.
    /// </remarks>
    public class UiAutomator2Options : AppiumOptions
    {
        private const string UdidOption = "appium:udid";
        private const string NoResetOption = "appium:noReset";
        private const string FullResetOption = "appium:fullReset";
        private const string NewCommandTimeoutOption = "appium:newCommandTimeout";
        private const string LanguageOption = "appium:language";
        private const string LocaleOption = "appium:locale";
        private const string AppPackageOption = "appium:appPackage";
        private const string AppActivityOption = "appium:appActivity";
        private const string AppWaitPackageOption = "appium:appWaitPackage";
        private const string AppWaitActivityOption = "appium:appWaitActivity";
        private const string AppWaitDurationOption = "appium:appWaitDuration";
        private const string AvdOption = "appium:avd";
        private const string AvdLaunchTimeoutOption = "appium:avdLaunchTimeout";
        private const string AutoGrantPermissionsOption = "appium:autoGrantPermissions";
        private const string SystemPortOption = "appium:systemPort";
        private const string DisableWindowAnimationOption = "appium:disableWindowAnimation";
        private const string ChromedriverExecutableOption = "appium:chromedriverExecutable";

        /// <summary>
        /// Initializes a new instance of the <see cref="UiAutomator2Options"/> class.
        /// </summary>
        public UiAutomator2Options() : base()
        {
            PlatformName = MobilePlatform.Android;
            AutomationName = OpenQA.Selenium.Appium.Enums.AutomationName.AndroidUIAutomator2;

            AddKnownCapabilityName(UdidOption, "Udid property");
            AddKnownCapabilityName(NoResetOption, "NoReset property");
            AddKnownCapabilityName(FullResetOption, "FullReset property");
            AddKnownCapabilityName(NewCommandTimeoutOption, "NewCommandTimeout property");
            AddKnownCapabilityName(LanguageOption, "Language property");
            AddKnownCapabilityName(LocaleOption, "Locale property");
            AddKnownCapabilityName(AppPackageOption, "AppPackage property");
            AddKnownCapabilityName(AppActivityOption, "AppActivity property");
            AddKnownCapabilityName(AppWaitPackageOption, "AppWaitPackage property");
            AddKnownCapabilityName(AppWaitActivityOption, "AppWaitActivity property");
            AddKnownCapabilityName(AppWaitDurationOption, "AppWaitDuration property");
            AddKnownCapabilityName(AvdOption, "Avd property");
            AddKnownCapabilityName(AvdLaunchTimeoutOption, "AvdLaunchTimeout property");
            AddKnownCapabilityName(AutoGrantPermissionsOption, "AutoGrantPermissions property");
            AddKnownCapabilityName(SystemPortOption, "SystemPort property");
            AddKnownCapabilityName(DisableWindowAnimationOption, "DisableWindowAnimation property");
            AddKnownCapabilityName(ChromedriverExecutableOption, "ChromedriverExecutable property");
        }

        /// <summary>
        /// Gets or sets the serial number of the device to run the session on (e.g. emulator-5554).
        /// </summary>
        public string Udid { get; set; }

        /// <summary>
        /// Gets or sets whether to keep the application state (data, permissions) between sessions.
        /// </summary>
        public bool? NoReset { get; set; }

        /// <summary>
        /// Gets or sets whether to uninstall the application and clear its data before and after the session.
        /// </summary>
        public bool? FullReset { get; set; }

        /// <summary>
        /// Gets or sets how long the server waits for a new command before ending the session.
        /// Sent to the server in whole seconds.
        /// </summary>
        public TimeSpan? NewCommandTimeout { get; set; }

        /// <summary>
        /// Gets or sets the language to set on the device (e.g. en).
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the locale to set on the device (e.g. US).
        /// </summary>
        public string Locale { get; set; }

        /// <summary>
        /// Gets or sets the package identifier of the application under test (e.g. io.appium.android.apis).
        /// </summary>
        public string AppPackage { get; set; }

        /// <summary>
        /// Gets or sets the activity to launch (e.g. .ApiDemos).
        /// </summary>
        public string AppActivity { get; set; }

        /// <summary>
        /// Gets or sets the package to wait for after launching the application.
        /// </summary>
        public string AppWaitPackage { get; set; }

        /// <summary>
        /// Gets or sets the activity, or comma-separated list of activities, to wait for after launching the application.
        /// </summary>
        public string AppWaitActivity { get; set; }

        /// <summary>
        /// Gets or sets how long to wait for <see cref="AppWaitActivity"/> to appear.
        /// Sent to the server in milliseconds.
        /// </summary>
        public TimeSpan? AppWaitDuration { get; set; }

        /// <summary>
        /// Gets or sets the name of the Android virtual device to launch (e.g. Pixel_8_API_35).
        /// </summary>
        public string Avd { get; set; }

        /// <summary>
        /// Gets or sets how long to wait for the virtual device to start.
        /// Sent to the server in milliseconds.
        /// </summary>
        public TimeSpan? AvdLaunchTimeout { get; set; }

        /// <summary>
        /// Gets or sets whether to grant all permissions requested by the application on install.
        /// </summary>
        public bool? AutoGrantPermissions { get; set; }

        /// <summary>
        /// Gets or sets the local port used to communicate with the UiAutomator2 server.
        /// Set a distinct value per session when running sessions in parallel.
        /// </summary>
        public int? SystemPort { get; set; }

        /// <summary>
        /// Gets or sets whether to disable window animations on the device during the session.
        /// </summary>
        public bool? DisableWindowAnimation { get; set; }

        /// <summary>
        /// Gets or sets the full path to the Chromedriver executable used for web contexts.
        /// </summary>
        public string ChromedriverExecutable { get; set; }

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
            OptionValues.AddIfSet(knownOptions, AppPackageOption, AppPackage);
            OptionValues.AddIfSet(knownOptions, AppActivityOption, AppActivity);
            OptionValues.AddIfSet(knownOptions, AppWaitPackageOption, AppWaitPackage);
            OptionValues.AddIfSet(knownOptions, AppWaitActivityOption, AppWaitActivity);
            OptionValues.AddIfSet(knownOptions, AppWaitDurationOption, OptionValues.ToMilliseconds(AppWaitDuration));
            OptionValues.AddIfSet(knownOptions, AvdOption, Avd);
            OptionValues.AddIfSet(knownOptions, AvdLaunchTimeoutOption, OptionValues.ToMilliseconds(AvdLaunchTimeout));
            OptionValues.AddIfSet(knownOptions, AutoGrantPermissionsOption, AutoGrantPermissions);
            OptionValues.AddIfSet(knownOptions, SystemPortOption, SystemPort);
            OptionValues.AddIfSet(knownOptions, DisableWindowAnimationOption, DisableWindowAnimation);
            OptionValues.AddIfSet(knownOptions, ChromedriverExecutableOption, ChromedriverExecutable);

            return knownOptions;
        }
    }
}
