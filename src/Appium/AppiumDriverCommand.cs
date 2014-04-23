using System; 

namespace OpenQA.Selenium.Appium
{
    public class AppiumDriverCommand
    {
        #region Appium Specific extensions to JSONWP Commands

        /// <summary>
        /// Represents the Shake Device Mapping command
        /// </summary>
        public const string ShakeDevice = "shakeDevice";

        /// <summary>
        /// Represents the Lock Device Mapping command
        /// </summary>
        public const string LockDevice = "lockDevice";

        /// <summary>
        /// Toggle's the Airplane Mode ("Flight Safe Mode") Command
        /// </summary>
        public const string ToggleAirplaneMode = "toggleAirplaneMode";

        // TODO: needs to be implemented in the future. (currently not implemented in appium but IS in the spec)
        /// <summary>
        /// Represents the Airplane Mode ("Flight Safe Mode") Command
        /// </summary>
        [Obsolete]
        public const string AirplaneMode = "airplaneMode";

        /// <summary>
        /// Press Key Event Command.
        /// </summary>
        public const string KeyEvent = "keyEvent";

        /// <summary>
        /// Rotate Command.
        /// </summary>
        public const string Rotate = "rotate";

        /// <summary>
        /// Get CurrentActivity Command.
        /// </summary>
        public const string GetCurrentActivity = "getCurrentActivity";

        /// <summary>
        /// Install App Command.
        /// </summary>
        public const string InstallApp = "installApp";

        /// <summary>
        /// Remove App Command.
        /// </summary>
        public const string RemoveApp = "removeApp";

        /// <summary>
        /// Is App Installed Command.
        /// </summary>
        public const string IsAppInstalled = "isAppInstalled";

        /// <summary>
        /// Push File Command.
        /// </summary>
        public const string PushFile = "pushFile";

        /// <summary>
        /// Pull File Command.
        /// </summary>
        public const string PullFile = "pullFile";

        /// <summary>
        /// Toggle Wifi Command.
        /// </summary>
        public const string ToggleWiFi = "toggleWiFi";

        /// <summary>
        /// Toggle Location Services Command.
        /// </summary>
        public const string ToggleLocationServices = "toggleLocationServices";

        /// <summary>
        /// Launch App Command.
        /// </summary>
        public const string LaunchApp = "launchApp";

        /// <summary>
        /// Close App Command.
        /// </summary>
        public const string CloseApp = "closeApp";

        /// <summary>
        /// Reset App Command.
        /// </summary>
        public const string ResetApp = "resetApp";

        /// <summary>
        ///  Background App Command.
        /// </summary>
        public const string BackgroundApp = "backgroundApp";

        /// <summary>
        ///  End Test Coverage Command.
        /// </summary>
        public const string EndTestCoverage = "endTestCoverage";

        /// <summary>
        ///  Get App Strings Command.
        /// </summary>
        public const string GetAppStrings = "getAppStrings";

        /// <summary>
        ///  Set Immediate Value Command.
        /// </summary>
        public const string SetImmediateValue = "setImmediateValue";

        /// <summary>
        ///  Find Complex Command.
        /// </summary>
        public const string FindComplex = "findComplex";

        /// <summary>
        /// Represents the Hide Keyboard command
        /// </summary>
        public const string HideKeyboard = "hideKeyboard";

        #endregion Appium Specific extensions to JSONWP Commands

        #region MultiTouchActions
        /// <summary>
        /// Perform multi touch action
        /// </summary>
        public const string TouchMultiPerform = "touchMultiPerform";
        #endregion MultiTouchActions

        #region Context Commands
        /// <summary>
        /// Represents the Contexts command
        /// </summary>
        public const string Contexts = "contexts";

        /// <summary>
        /// Represents the Get Context command
        /// </summary>
        public const string GetContext = "getContext";

        /// <summary>
        /// Represents the Set Context command
        /// </summary>
        public const string SetContext = "setContext";
        #endregion Context Commands
    }
}
