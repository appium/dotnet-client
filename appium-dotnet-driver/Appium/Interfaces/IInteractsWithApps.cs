using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Appium.Interfaces
{
    interface IInteractsWithApps
    {
        /// <summary>
        /// Installs an App.
        /// </summary>
        /// <param name="appPath">a string containing the file path or url of the app.</param>
        void InstallApp(string appPath);

        /// <summary>
        /// Launches the current app.
        /// </summary>
        void LaunchApp();

        /// <summary>
        /// Checks If an App Is Installed.
        /// </summary>
        /// <param name="appPath">a string containing the bundle id.</param>
        /// <return>a bol indicating if the app is installed.</return>
        bool IsAppInstalled(string bundleId);

        /// <summary>
        /// Resets the current app.
        /// </summary>
        void ResetApp();

        /// <summary>
        /// Backgrounds the current app for the given number of seconds.
        /// </summary>
        /// <param name="seconds">a string containing the number of seconds.</param>
        void BackgroundApp(int seconds);


        /// <summary>
        /// Removes an App.
        /// </summary>
        /// <param name="appPath">a string containing the id of the app.</param>
        void RemoveApp(string appId);

        /// <summary>
        /// Closes the current app.
        /// </summary>
        void CloseApp();        
    }
}
