using System;
using System.Globalization;
using OpenQA.Selenium.Remote;
using static System.String;

namespace OpenQA.Selenium.Appium.Windows
{
    public class WindowsOptions : AppiumOptions
    {
        private const string PlatformNameOption = "platformName";
        private const string AutomationNameOption = "automationName";
        private const string AppOption = "app";
        private const string AppArgumentsOption = "appArguments";
        private const string AppTopLevelWindowOption = "appTopLevelWindow";
        private const string AppWorkingDirOption = "appWorkingDir";
        private const string CreateSessionTimeoutOption = "createSessionTimeout";
        private const string WaitForAppLaunchOption = "ms:waitForAppLaunch";
        private const string ExperimentalWebDriverOption = "ms:experimental-webdriver";
        private const string SystemPortOption = "systemPort";
        private const string PreRunOption = "prerun";
        private const string PostRunOption = "postrun";

        public WindowsOptions()
        {
            PlatformName = "windows";
            AutomationName = "windows";
            AddKnownCapabilityName(AutomationNameOption, "AutomationName property");
            AddKnownCapabilityName(PlatformNameOption, "platformName property");
            AddKnownCapabilityName(AppOption, "App property");
            AddKnownCapabilityName(AppArgumentsOption, "AppArguments property");
            AddKnownCapabilityName(AppArgumentsOption, "AppTopLevelWindow property");
            AddKnownCapabilityName(AppWorkingDirOption, "AppWorkingDir property");
            AddKnownCapabilityName(CreateSessionTimeoutOption, "CreateSessionTimeout property");
            AddKnownCapabilityName(WaitForAppLaunchOption, "WaitForAppLaunchTimeout property");
            AddKnownCapabilityName(ExperimentalWebDriverOption, "IsExperimentalWebDriver property");
            AddKnownCapabilityName(SystemPortOption, "SystemPort property");
            AddKnownCapabilityName(PreRunOption, "PreRunScript property");
            AddKnownCapabilityName(PostRunOption, "PostRunScript property");
        }

        /// <summary>
        /// The name of the UWP application to test or full path to a classic app,
        /// for example Microsoft.WindowsCalculator_8wekyb3d8bbwe!App or C:\Windows\System32\notepad.exe
        /// It is also possible to set app to Root.
        /// In such case the session will be invoked without any explicit target application (actually, it will be Explorer).
        /// Either this option or <see cref="AppTopLevelWindow"/> must be provided on session startup.
        /// </summary>
        public new string App
        {
            get => base.App;
            set => base.App = value;
        }

        /// <summary>
        /// Application arguments string, for example "/?"
        /// </summary>
        public string AppArguments { get; set; }

        /// <summary>
        /// The hexadecimal handle of an existing application top level window to attach to.
        /// <remarks>Either this capability or <see cref="AppName"/> must be provided on session startup.</remarks>
        /// </summary>
        public string AppTopLevelWindow { get; set; }

        /// <summary>
        /// Full path to the folder.
        /// <remarks>This is only applicable for classic apps</remarks>
        /// </summary>
        public string AppWorkingDir { get; set; }

        /// <summary>
        /// Timeout in milliseconds used to retry Appium Windows Driver session startup.
        /// <remarks>Default value is 20000</remarks>
        /// </summary>
        public int CreateSessionTimeout { get; set; } = 20000;

        /// <summary>
        /// Similar to <see cref="CreateSessionTimeout"/>, but in seconds and is applied on the server side.
        /// <remarks>Default value is 50 seconds.</remarks>
        /// <remarks>The limit for this is 50 seconds.</remarks>
        /// </summary>
        public int WaitForAppLaunchTimeout { get; set; } = 50;

        /// <summary>
        /// Enables experimental features and optimizations.
        /// <remarks>This is <see langword="false"/> by default</remarks>
        /// </summary>
        public bool IsExperimentalWebDriver { get; set; }

        /// <summary>
        /// The port number to execute Appium Windows Driver server listener on.
        /// <remarks>The default starting port number for a new Appium Windows Driver session is 4724.
        /// If this port is already busy then the next free port will be automatically selected.</remarks>
        /// </summary>
        public int SystemPort { get; set; } = 4724;

        /// <summary>
        /// An object containing either script or command key.
        /// The value of each key must be a valid PowerShell script or command to be executed prior to the WinAppDriver session startup.
        /// Example: <c>script: 'Get-Process outlook -ErrorAction SilentlyContinue'}</c>
        /// </summary>
        public string PreRunScript { get; set; }

        /// <summary>
        /// An object containing either script or command key.
        /// The value of each key must be a valid PowerShell script or command to be executed after WinAppDriver session is stopped.
        /// </summary>
        public string PostRunScript { get; set; }

        [Obsolete(
            "Use the temporary AddAdditionalOption method or the browser-specific method for adding additional options")]
        public override void AddAdditionalCapability(string capabilityName, object capabilityValue)
        {
            if (IsNullOrEmpty(capabilityName))
            {
                throw new ArgumentException(Format(CultureInfo.InvariantCulture, "Capability name may not be null or an empty string."), nameof(capabilityName));
            }

            AddAdditionalOption(capabilityName, capabilityValue);
        }

        public override ICapabilities ToCapabilities()
        {
            var capabilities = base.ToCapabilities() as DesiredCapabilities;

            if (!IsNullOrEmpty(AppArguments))
            {
                capabilities.SetCapability(AppArgumentsOption, AppArguments);
            }

            if (!IsNullOrEmpty(AppTopLevelWindow))
            {
                capabilities.SetCapability(AppTopLevelWindowOption, AppTopLevelWindow);
            }

            if (!IsNullOrEmpty(AppWorkingDir))
            {
                capabilities.SetCapability(AppWorkingDirOption, AppWorkingDir);
            }

            if (CreateSessionTimeout >= 0)
            {
                capabilities.SetCapability(CreateSessionTimeoutOption, CreateSessionTimeout);
            }

            if (WaitForAppLaunchTimeout >= 0)
            {
                capabilities.SetCapability(WaitForAppLaunchOption, WaitForAppLaunchTimeout);
            }

            if (IsExperimentalWebDriver)
            {
                capabilities.SetCapability(ExperimentalWebDriverOption, true);
            }

            if (SystemPort > 0)
            {
                capabilities.SetCapability(SystemPortOption, SystemPort);
            }

            if (!IsNullOrEmpty(PreRunScript))
            {
                capabilities.SetCapability(PreRunOption, PreRunScript);
            }

            if (!IsNullOrEmpty(PostRunScript))
            {
                capabilities.SetCapability(PostRunScript, PostRunScript);
            }

            return capabilities;
        }
    }
}
