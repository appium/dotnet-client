using System;

namespace OpenQA.Selenium.Remote
{
    public class AppiumDriverCommand
    {
        #region Driver Mode Commands

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
