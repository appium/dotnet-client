using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.ObjectModel;

namespace Appium.Integration.Tests.iOS
{
	public class IOSSearchingClassChainTest
	{
		private IOSDriver<AppiumWebElement> driver;

		[TestFixtureSetUp]
		public void beforeAll()
		{
            DesiredCapabilities capabilities = Caps.getIos102Caps(Apps.get("iosUICatalogApp"));
			if (Env.isSauce())
			{
				capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
				capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
				capabilities.SetCapability("name", "ios - complex");
				capabilities.SetCapability("tags", new string[] { "sample" });

			}
			Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIForIOS;
			driver = new IOSDriver<AppiumWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
			driver.Manage().Timeouts().ImplicitlyWait(Env.IMPLICIT_TIMEOUT_SEC);
		}

		[TestFixtureTearDown]
		public void AfterEach()
		{
			if (driver != null)
			{
				driver.Quit();
			}
			if (!Env.isSauce())
			{
				AppiumServers.StopLocalService();
			}
		}

		[Test()]
		public void FindByClassChainTest()
		{
            ReadOnlyCollection<AppiumWebElement> sliderCellStaticTextElements_1 = driver
                .FindElements(new ByIosClassChain("**/XCUIElementTypeCell/XCUIElementTypeStaticText[`name == 'Sliders'`]"));
            Assert.AreEqual(sliderCellStaticTextElements_1.Count, 1);
            ReadOnlyCollection<AppiumWebElement> sliderCellStaticTextElements_2 = driver
                .FindElementsByIosClassChain("**/XCUIElementTypeCell");
            Assert.AreEqual(sliderCellStaticTextElements_2.Count, 18);

            AppiumWebElement actionSheetsCell = driver.FindElement(new ByIosClassChain("**/XCUIElementTypeCell/XCUIElementTypeStaticText[`name == 'Action Sheets'`]"));
            actionSheetsCell.Tap(1, 250);
            driver.FindElementByIosClassChain("**/XCUIElementTypeNavigationBar/XCUIElementTypeButton").Click();
		}
	}
}
