using System;

namespace OpenQA.Selenium.Appium
{
    /// <summary>
    /// Extension methods to convert to/from ScreenOrientation enum
    /// </summary>
    public static class ScreenOrientationExtensions
    {
        /// <summary>
        /// Converts the Screen Orientation to the string needed by the JSON Wire Protocol
        /// </summary>
        /// <param name="orientation"></param>
        /// <returns></returns>
        public static string JSONWireProtocolString(this ScreenOrientation orientation)
        {
            return orientation.ToString().ToUpper();
        }

        /// <summary>
        /// Converts the string to a screen orientation if possible, else throws ArgumentOutOfRangeException 
        /// </summary>
        /// <param name="orientation"></param>
        /// <returns>Screen Orientation</returns>
        public static ScreenOrientation ConvertToScreenOrientation(this string orientation)
        {
            ScreenOrientation retVal = ScreenOrientation.Landscape;
            switch (orientation)
            {
                case "PORTRAIT":
                    retVal = ScreenOrientation.Portrait;
                    break;
                case "LANDSCAPE":
                    retVal = ScreenOrientation.Landscape;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unknown Orientation Type Passed in");
            }
            return retVal;
        }

    }
}
