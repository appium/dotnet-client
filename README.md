# appium-dotnet-driver

![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Appium.WebDriver)
[![Build Status](https://dev.azure.com/AppiumCI/dotnet-client/_apis/build/status/appium.dotnet-client?branchName=release%2F5.0.0)](https://dev.azure.com/AppiumCI/dotnet-client/_build/latest?definitionId=68&branchName=release%2F5.0.0)

[![NuGet Downloads](https://img.shields.io/nuget/dt/Appium.Webdriver.svg)](https://www.nuget.org/packages/Appium.Webdriver)

[![Build and deploy NuGet package](https://github.com/appium/dotnet-client/actions/workflows/release-nuget.yml/badge.svg)](https://github.com/appium/dotnet-client/actions/workflows/release-nuget.yml)

This driver is an extension of the [Selenium](http://docs.seleniumhq.org/) C# client. It has 
all the functionalities of the regular driver, but add Appium specific methods on top of this.

## Appium server compatibility for v5.x 

In case you are using the latest beta client v5.x please be aware you will either have to upgrade your appium server to 2.x or add the base-path argument:
`appium --base-path=/wd/hub`, due to a breaking change on the default server base path. <br/>
Regardless, it's highly recommended you move to appium 2.x since appium 1.x is no longer maintained. <br/>
For more details about how to migrate to 2.x, see the following link : [appium 2.x migrating](https://appium.github.io/appium/docs/en/2.0/guides/migrating-1-to-2/)

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

[See samples here](https://github.com/appium/sample-code/tree/master/sample-code/examples/dotnet/AppiumDotNetSample)


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
- update assemblyInfo.cs, RELEASE_NOTES.md, and appium-dotnet-driver.nuspec with new new version number and release details, then check it in
- pull new code
- `Rebuild All` with `Release` target.
- `NuGet pack appium-dotnet-driver.nuspec`
- `NuGet push Appium.WebDriver.<version>.nupkg`
