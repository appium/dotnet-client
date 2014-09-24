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
        /// Represents the Is Device Locked Mapping command
        /// </summary>
        public const string IsLocked = "isLocked";

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
        /// Pull Folder Command.
        /// </summary>
        public const string PullFolder = "pullFolder";

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

        /// <summary>
        ///  Open Notifications Command
        /// </summary>
        public const string OpenNotifications = "openNotifications";

        /// <summary>
        /// The Start Activity command
        /// </summary>
        public const string StartActivity = "startActivity";

        #endregion Appium Specific extensions to JSONWP Commands

        #region TouchActions
        /// <summary>
        /// Perform touch action
        /// </summary>
        public const string TouchActionV2Perform = "touchActionV2Perform";

        /// <summary>
        /// Perform multi touch action
        /// </summary>
        public const string MultiActionV2Perform = "multiActionV2Perform";
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

        #region JSON Wire Protocol
        /// <summary>
        /// Represents the Get Orientation Command
        /// </summary>
        public const string GetOrientation = "getOrientation";

        /// <summary>
        /// Represents the Set Orientation Command
        /// </summary>
        public const string SetOrientation = "setOrientation";

        /// <summary>
        /// Represents the Get Network Connection Command
        /// </summary>
        public const string GetConnectionType = "getConnectionType";

        /// <summary>
        /// Represents the Set Network Connection Command
        /// </summary>
        public const string SetConnectionType = "setConnectionType";

        /// <summary>
        /// Represents the Get Available Engines Command
        /// </summary>
        public const string GetAvailableEngines = "getAvailableEngines";

        /// <summary>
        /// Represents the Get Active Engine Command
        /// </summary>
        public const string GetActiveEngine = "getActiveEngine";

        /// <summary>
        /// Represents the is IME active Command
        /// </summary>
        public const string IsIMEActive = "isIMEActive";

        /// <summary>
        /// Represents the Activate Engine Command
        /// </summary>
        public const string ActivateEngine = "activateEngine";

        /// <summary>
        /// Represents the Deactivate Engine Command
        /// </summary>
        public const string DeactivateEngine = "deactivateEngine";

        /// <summary>
        /// Represents the Screenshot Command
        /// </summary>
        public const string GetScreenshot = "screenshot";

		/// <summary>
		/// Represents the GetSettings Command
		/// </summary>
		public const string GetSettings = "getSettings";

		/// <summary>
		/// Represents the UpdateSettings Command
		/// </summary>
		public const string UpdateSettings = "updateSettings";

        #endregion JSON Wire Protocol
    }
}
