# appium-dotnet-driver

[![NuGet Badge](https://buildstats.info/nuget/Appium.Webdriver)](https://www.nuget.org/packages/Appium.Webdriver/)
[![Build Status](https://dev.azure.com/AppiumCI/dotnet-client/_apis/build/status/appium.appium-dotnet-driver?branchName=master)](https://dev.azure.com/AppiumCI/dotnet-client/_build/latest?definitionId=13&branchName=master)

[![NuGet Badge](https://buildstats.info/nuget/Appium.Webdriver?includePreReleases=true)](https://www.nuget.org/packages/Appium.WebDriver/5.0.0-beta02)


This driver is an extension of the [Selenium](http://docs.seleniumhq.org/) C# client. It has 
all the functionalities of the regular driver, but add Appium specific methods on top of this.

> **Note**
>
> The last stable version(v4.4.0) supports the legacy Selenium 3.150.0.<br/>	 
> In case you would like to use this client with Selenium 4.0 and above, please use the latest beta version(v5.0.0). <br/>
> We are well aware this project is not actively maintained, therefore any contributors are more than welcomed to assist with this project.

## NuGet

NuGet Package: [](http://www.nuget.org/packages/Appium.WebDriver/)

Dependencies:

- [Selenium.WebDriver](http://www.nuget.org/packages/Selenium.WebDriver/)
- [Newtonsoft.Json](http://www.nuget.org/packages/Newtonsoft.Json/)
- [Selenium.Support](https://www.nuget.org/packages/Selenium.Support/)
- [Castle.Core](https://www.nuget.org/packages/Castle.Core/)

Note: we will NOT publish a signed version of this assembly since the dependencies we access through NuGet do not have a signed version - thus breaking the chain and causing us headaches. With that said, you are more than welcome to download the code and build a signed version yourself.
 
## Usage

### basics

- You need to add the following namespace line: `using OpenQA.Selenium.Appium;`.
- Use the `AppiumDriver` class/subclass to construct the driver. It works the same as the Selenium Webdriver, except that
 the ports are defaulted to Appium values, and the driver does not know how to start the Appium on its own.
- To use the Appium methods on Element, you need to specify the parameter of `AppiumDriver` or its subclasses.

[Read Wiki](https://github.com/appium/appium-dotnet-driver/wiki)

[See samples here](https://github.com/appium/appium-dotnet-driver/tree/master/test/integration)


## Dev Build+Test 

Xamarin/Mono
- Open with [Xamarin](http://xamarin.com/)
- `Rebuild all`
- `Run tests in test/specs`

JetBrains Rider
- Open with [Rider](https://www.jetbrains.com/rider/)
- From the menu `Build -> Rebuild Solution`
- Run tests in Appium.Net.Integration.Tests

Visual studio

- Open with [Visual Studio](https://www.visualstudio.com/)
- build solution

## Nuget Deployment (for maintainers)

### To Setup Nuget 
- Download [Nuget exe](http://nuget.org/nuget.exe).
- Setup the Api Key ([see here](http://docs.nuget.org/docs/creating-packages/creating-and-publishing-a-package#api-key)).
- `alias NuGet='mono <Nuget Path>/NuGet.exe'`


### To Release a New Version

Auto release follow the rule in github/labeler.yml

- update assemblyInfo.cs, RELEASE_NOTES.md, and appium-dotnet-driver.nuspec with new new version number and release details, then check it in
- pull new code
- `Rebuild All` with `Release` target.
- `NuGet pack appium-dotnet-driver.nuspec`
- `NuGet push Appium.WebDriver.<version>.nupkg`
