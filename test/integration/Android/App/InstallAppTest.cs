using System;
using Appium.Net.Integration.Tests.helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using NUnit.Framework;

namespace Appium.Net.Integration.Tests.Android.App
{
    public class InstallAppTest : IDisposable
    {
        private readonly AndroidDriver _driver;
        private readonly string _apkPath;
        private readonly string _packageName;

        public InstallAppTest()
        {
            _apkPath = Apps.Get(Apps.androidApiDemos);
            _packageName = Apps.GetId(Apps.androidApiDemos);
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            AppiumOptions opts = Caps.GetAndroidUIAutomatorCaps();
            // Do not preinstall the app via capabilities; we test explicit installation.
            _driver = new AndroidDriver(serverUri, opts, TimeSpan.FromMinutes(2));
        }

        [Test]
        public void MobileInstallApp_InstallsPackage()
        {
            _driver.InstallApp(_apkPath);
            Assert.That(_driver.IsAppInstalled(_packageName), Is.True);
        }

        [Test]
        public void MobileInstallApp_IsIdempotent()
        {
            _driver.InstallApp(_apkPath);
            _driver.InstallApp(_apkPath); // second time should not fail
            Assert.That(_driver.IsAppInstalled(_packageName), Is.True);
        }


        [Test]
        public void MobileInstallApp_InvalidPath_Throws()
        {
            var badPath = "/nonexistent/path/app.apk";
            var ex = Assert.Throws<UnknownErrorException>(() => _driver.InstallApp(badPath));
            Assert.That(ex.Message, Does.Contain("does not exist").IgnoreCase);
        }

        [Test]
        public void MobileInstallApp_WithTimeout_InstallsPackage()
        {
            _driver.InstallApp(_apkPath, timeout: 60000);
            Assert.That(_driver.IsAppInstalled(_packageName), Is.True);
        }

        [Test]
        public void MobileInstallApp_WithCheckVersion_InstallsPackage()
        {
            _driver.InstallApp(_apkPath, checkVersion: true);
            Assert.That(_driver.IsAppInstalled(_packageName), Is.True);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            try
            {
                if (!string.IsNullOrWhiteSpace(_packageName) && _driver.IsAppInstalled(_packageName))
                {
                    _driver.RemoveApp(_packageName);
                }
            }
            catch (Exception ex) 
            { 
                Console.Error.WriteLine($"Exception during Dispose: {ex}");
            }
            _driver?.Quit();
        }
    }
}