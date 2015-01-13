#appium-dotnet-driver

This driver is an extension of the [Selenium](http://docs.seleniumhq.org/) C# client. It has 
all the functionalities of the regular driver, but add Appium specific methods on top of this.

## NuGet

NuGet Package: [](http://www.nuget.org/packages/Appium.WebDriver/)

Dependencies:

- [Selenium.WebDriver](http://www.nuget.org/packages/Selenium.WebDriver/)
- [Newtonsoft.Json](http://www.nuget.org/packages/Newtonsoft.Json/)

Note: we will NOT publish a signed version of this assembly since the dependencies we access through NuGet do not have a signed version - thus breaking the chain and causing us headaches. With that said, you are more than welcome to download the code and build a signed version yourself.
 
## Usage

### basics

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

			capabilities.SetCapability("deviceName", "iPhone Retina (4-inch 64-bit)");
			capabilities.SetCapability("platformName", "iOS");
			capabilities.SetCapability("platformVersion", "7.1");
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

[See samples here](/samples)

### TouchAction
```c#
TouchAction a1 = new TouchAction (driver);
a1
  .Press (element, 100, 100)
  .Wait (1000)
  .Release ();
a1.Perform();
```

### MultiAction

```c#
MultiAction m = new MultiAction(driver);

TouchAction a1 = new TouchAction (driver);
a1
  .Press (element, 100, 100)
  .Wait (1000)
  .Release ();
m.Add(a1);

TouchAction a2 = new TouchAction ();
a2
  .Tap (100, 100)
  .MoveTo (element);
m.Add (a2);

m.Perform();
```

## Dev Build+Test 

- Open with [Xamarin](http://xamarin.com/)
- `Rebuild all`
- `Run tests in test/specs`

## Nuget Deployment (for maintainers)

### To Setup Nuget 
- Download [Nuget exe](http://nuget.org/nuget.exe).
- Setup the Api Key ([see here](http://docs.nuget.org/docs/creating-packages/creating-and-publishing-a-package#api-key)).
- `alias NuGet='mono <Nuget Path>/NuGet.exe'`


### To Release a New Version
- update assemblyInfo.cs, RELEASE_NOTES.md, and appium-dotnet-driver.nuspec with new new version number and release details, then check it in
- pull new code
- `Rebuild All` with `Release` target.
- `NuGet pack appium-dotnet-driver.nuspec`
- `NuGet push Appium.WebDriver.<version>.nupkg`
