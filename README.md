#appium-dotnet-driver

This driver is an extension of the [Selenium](http://docs.seleniumhq.org/) C# client. It has 
all the functionalities of the regular driver, but add Appium specific methods on top of this.

## Install

### NuGet

NuGet Package: [](http://www.nuget.org/packages/Appium.WebDriver/)

Dependencies:

- [Selenium.WebDriver](http://www.nuget.org/packages/Selenium.WebDriver/)
- [Newtonsoft.Json](http://www.nuget.org/packages/Newtonsoft.Json/)
 
### Downloads

[appium-dotnet-driver.tar.gz](https://github.com/appium/appium-dotnet-driver/raw/master/downloads/appium-dotnet-driver.tar.gz)

## Usage

- You need to add the following namespace line: `using OpenQA.Selenium.Appium;`.
- Use the `AppiumDriver` class to construct the driver. It works the same as the Selenium Webdriver, except that
 the ports are defaulted to Appium values, and the driver does not know how to start the Appium on its own.
- To use the Appium methods on Element, you need to cast the object returned by the finder method to 
`AppiumWebElement`.


```c#
...
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium;

namespace AppiumDriverDemo
{
	[TestFixture ()]
	public class ProgramTest
	{
		private AppiumDriver driver;

		[TestFixtureSetUp]
		public void beforeAll(){
			DesiredCapabilities capabilities = new DesiredCapabilities();

			capabilities.SetCapability("device", "iPhone Simulator");
			capabilities.SetCapability("deviceName", "iPhone Retina (4-inch 64-bit)");
			capabilities.SetCapability("platform", "ios");
			capabilities.SetCapability("version", "7.1");
			capabilities.SetCapability("app", "<Path to your app>");
			driver = new AppiumDriver(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities);		
		}

		[TestFixtureTearDown]
		public void afterAll(){
			// shutdown
			driver.Quit();
		}
			
		[Test ()]
		public void AppiumDriverMethodsTestCase ()
		{
			// Using appium extension methods
			AppiumWebElement el = (AppiumWebElement) driver.FindElementByIosUIAutomation(".elements()");
			el.SetImmediateValue ("abc");
			Assert.False (driver.IsAppInstalled("RamdomApp"));
		}
	}

}

```

[Full Project Here](https://github.com/appium/appium/tree/1.0-beta/sample-code/examples/dotnet/AppiumDriverDemo)

## API Doc

TODO: generate API Doc

## Dev Build+Test 

- Open with [Xamarin](http://xamarin.com/)
- `Rebuild all`
- `Run Unit Tests`

## Deploy (for maintainers)

Once, if using mono
- Download [Nuget exe](http://nuget.org/nuget.exe)
- Setup the Api Key ([see here](http://docs.nuget.org/docs/creating-packages/creating-and-publishing-a-package#api-key))
- alias NuGet='mono <Nuget Path>/NuGet.exe'

For Nuget:

- `Rebuild All` with `Release` target
- Edit the file: `appium-dotnet-driver.nuspec` (At least bump version and change release notes.)
- `NuGet pack appium-dotnet-driver.nuspec`
- `nuget Push appium-webdriver.<version>.nupkg`
- Commit and push changes

For Downloads:
- Click on Packages/Linux Binaries in the left handside menu
- Commit and push changes
