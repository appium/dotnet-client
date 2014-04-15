using System;

namespace OpenQA.Selenium.Remote
{
    public class AppiumDriverCommand
    {
        #region Driver Mode Commands

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

		#endregion Driver Mode Commands

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
