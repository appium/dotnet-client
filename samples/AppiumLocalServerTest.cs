using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Service.Options;
using System;
using System.IO;

namespace Appium.Samples
{
	[TestFixture ()]
	public class AppiumLocalServerTest
	{
        private string PathToCustomizedAppiumJS; 

		[TestFixtureSetUp]
		public void BeforeAll(){
            byte[] bytes = null;

            bool isWindows = Platform.CurrentPlatform.IsPlatformType(PlatformType.Windows);
            bool isMacOS   = Platform.CurrentPlatform.IsPlatformType(PlatformType.Mac);
            bool isLinux   = Platform.CurrentPlatform.IsPlatformType(PlatformType.Linux);

            if (isWindows)
            {
                bytes = Properties.Resources.PathToWindowsNode;
            }
            if (isMacOS)
            {
                bytes = Properties.Resources.PathToMacOSNode;
            }
            if (isLinux)
            {
                bytes = Properties.Resources.PathToLinuxNode;
            }

            PathToCustomizedAppiumJS = System.Text.Encoding.UTF8.GetString(bytes);
        }

        [Test]
        public void CheckAbilityToBuildDefaultService()
        {
            AppiumLocalService.BuildDefaultService();
        }

        [Test]
        public void CheckAbilityToBuildServiceWithDefinedParametersAndNodeSetInProperties()
        {
            try
            {
                string definedNode = PathToCustomizedAppiumJS;
                Environment.SetEnvironmentVariable(AppiumServiceBuilder.AppiumNodeProperty, definedNode);

                OptionCollector args = new OptionCollector().AddArguments(GeneralOptionList.OverrideSession());
                new AppiumServiceBuilder().WithIPAddress("127.0.0.1").UsingPort(4000).WithArguments(args).Build();
            }
            finally
            {

                Environment.SetEnvironmentVariable(AppiumServiceBuilder.AppiumNodeProperty, string.Empty);
            }
        }

        [Test]
        public void CheckAbilityToBuildServiceWithDefinedParametersAndExternallyDefinedNode()
        {
            OptionCollector args = new OptionCollector().AddArguments(GeneralOptionList.OverrideSession());
            new AppiumServiceBuilder().WithAppiumJS(new FileInfo(PathToCustomizedAppiumJS)).WithIPAddress("127.0.0.1").
                    UsingPort(4000).WithArguments(args).Build();
        }
    }
}

