using Appium.Net.Integration.Tests.Properties;
using OpenQA.Selenium;
using System;
using System.Text;

namespace Appium.Net.Integration.Tests.Helpers
{
    internal class Paths
    {
        private bool _isWindows;
        private bool _isMacOs;
        private bool _isLinux;
        private string _pathToCustomizedAppiumJs;

        public Paths()
        {
            // Detect the platform during object creation.
            _isWindows = Platform.CurrentPlatform.IsPlatformType(PlatformType.Windows);
            _isMacOs = Platform.CurrentPlatform.IsPlatformType(PlatformType.Mac);
            _isLinux = Platform.CurrentPlatform.IsPlatformType(PlatformType.Linux);
        }

        public string PathToCustomizedAppiumJs
        {
            get
            {
                if (_pathToCustomizedAppiumJs == null)
                {
                    GetAppiumJsPath();
                }
                return _pathToCustomizedAppiumJs;
            }
        }

        private void GetAppiumJsPath()
        {
            byte[] bytes;
            string appiumJsPath = null;

            if (_isWindows)
            {
                bytes = Resources.PathToWindowsNode;
                appiumJsPath = Encoding.UTF8.GetString(bytes);
            }
            else if (_isMacOs)
            {
                bytes = Resources.PathToMacOSNode;
                appiumJsPath = Encoding.UTF8.GetString(bytes);
            }
            else if (_isLinux)
            {
                bytes = Resources.PathToLinuxNode;
                appiumJsPath = Encoding.UTF8.GetString(bytes);
            }
            else
            {
                // Handle unsupported platform
                throw new PlatformNotSupportedException("Unsupported platform");
            }

            if (appiumJsPath.Contains("%USERPROFILE%"))
            {
                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                userProfile = userProfile.Replace("\\", "/");
                _pathToCustomizedAppiumJs = appiumJsPath.Replace("%USERPROFILE%", userProfile);
            }
            else
            {
                _pathToCustomizedAppiumJs = appiumJsPath;
            }
        }
    }
}
