# appium-dotnet-client

[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Appium.WebDriver)](https://www.nuget.org/packages/Appium.WebDriver/absoluteLatest)
[![Build and deploy NuGet package](https://github.com/appium/dotnet-client/actions/workflows/release-nuget.yml/badge.svg)](https://github.com/appium/dotnet-client/actions/workflows/release-nuget.yml)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Appium.Webdriver.svg)](https://www.nuget.org/packages/Appium.Webdriver)

[![Build Status](https://dev.azure.com/AppiumCI/dotnet-client/_apis/build/status/appium.dotnet-client?branchName=main)](https://dev.azure.com/AppiumCI/dotnet-client/_build/latest?definitionId=68&branchName=main)

[![Help Wanted](https://img.shields.io/github/issues-raw/appium/dotnet-client/HelpWanted?style=plastic&logo=github&logoColor=blue&label=Help%20Wanted%20issues&color=red
)](https://github.com/appium/dotnet-client/issues?q=is%3Aopen+is%3Aissue+label%3AHelpWanted)

[![License](https://img.shields.io/badge/License-Apache_2.0-lightblue.svg)](https://opensource.org/licenses/Apache-2.0)

This driver is an extension of the [Selenium](http://docs.seleniumhq.org/) C# client. It has 
all the functionalities of the regular driver, but add Appium-specific methods on top of this.

## Compatibility Matrix

The Appium .NET Client depends on [Selenium .NET binding](https://www.nuget.org/packages/Selenium.WebDriver), thus the Selenium .NET binding update might affect the Appium .NET Client behavior. 
For example, some changes in the Selenium binding could break the Appium client.

|Appium .NET Client| Selenium Binding	| .NET Version |
|----|----|----|
|`7.0.0` |`4.27.0` |.NET Standard 2.0, .NET 6.0|
|`6.0.0` |`4.25.0` |.NET Standard 2.0, .NET 6.0|
|`5.1.0` |`4.23.0` |.NET 6.0, .NET Framework 4.8 |
|`5.0.0` |`4.0.0` - `4.22.0` | .NET 6.0, .NET Framework 4.8 |
|`4.4.5` |`3.141.0` |.NET Standard 2.0, .NET Framework 4.8 |

## v5
 
### Appium server compatibility for v5.x

> [!IMPORTANT]
> In case you are using the latest beta client v5.x please be aware you will either have to upgrade your appium server to 2.x or add the base-path argument:
> `appium --base-path=/wd/hub`, due to a breaking change on the default server base path. <br/>
> Regardless, moving to appium 2.x is highly recommended since appium 1.x is no longer maintained. <br/>
> For more details about how to migrate to 2.x, see the following link : [appium 2.x migrating](https://appium.github.io/appium/docs/en/2.0/guides/migrating-1-to-2/)

#### Additional Information
W3C Actions: https://www.selenium.dev/documentation/webdriver/actions_api  <br/>
App management: Please read [issue #15807](https://github.com/appium/appium/issues/15807) for more details

### Migration Guide to W3C actions
```csharp
  using OpenQA.Selenium.Interactions;
  
  var touch = new PointerInputDevice(PointerKind.Touch, "finger");
  var sequence = new ActionSequence(touch);
  var move = touch.CreatePointerMove(elementToTouch, elementToTouch.Location.X, elementToTouch.Location.Y,TimeSpan.FromSeconds(1));
  var actionPress = touch.CreatePointerDown(MouseButton.Touch);
  var pause = touch.CreatePause(TimeSpan.FromMilliseconds(250));
  var actionRelease = touch.CreatePointerUp(MouseButton.Touch);
 
  sequence.AddAction(move);
  sequence.AddAction(actionPress);
  sequence.AddAction(pause);
  sequence.AddAction(actionRelease);
  
  var actions_seq = new List<ActionSequence>
  {
      sequence
  };
 
  _driver.PerformActions(actions_seq);
 ```

### WinAppDriver Notice!

> [!WARNING]
> Because [WinAppDriver](https://github.com/microsoft/WinAppDriver) has been abandoned by MS, running Appium dotnet-client 5.x with WAD will not work since it has not been updated to support the W3C protocol. <br/>
> To run appium on Windows Applications, you will need to use [appium-windows-driver](https://github.com/appium/appium-windows-driver) which will act as a proxy to WAD.
> Examples of running Windows Applications with dotnet-client can be found here: [windows Integration test 5.0.0](https://github.com/appium/dotnet-client/tree/release/5.0.0/test/integration/Windows) <br/>
> Regardless, feel free to open an issue on the [WAD](https://github.com/microsoft/WinAppDriver/issues) repository that will help get MS to open-source that project.

## NuGet

[NuGet Package](http://www.nuget.org/packages/Appium.WebDriver/)

Dependencies:

- [Selenium.WebDriver](http://www.nuget.org/packages/Selenium.WebDriver/)
- [System.Drawing.Common](https://www.nuget.org/packages/System.Drawing.Common/)

Note: we will NOT publish a signed version of this assembly since the dependencies we access through NuGet do not have a signed version - thus breaking the chain and causing us headaches. With that said, you are more than welcome to download the code and build a signed version yourself.
 
## Usage

### basics

- You need to add the following namespace line: `using OpenQA.Selenium.Appium;`.
- Use the `AppiumDriver` class/subclass to construct the driver. It works the same as the Selenium Webdriver, except that
 the ports default to Appium values, and the driver does not know how to start the Appium independently.
- To use the Appium methods on Element, specify the parameter of `AppiumDriver` or its subclasses.

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

Visual Studio

- Open with [Visual Studio](https://www.visualstudio.com/)
- build solution

## Nuget Deployment (for maintainers)

### To Setup Nuget 
- Download [Nuget exe](http://nuget.org/nuget.exe).
- Setup the Api Key ([see here](http://docs.nuget.org/docs/creating-packages/creating-and-publishing-a-package#api-key)).
- `alias NuGet='mono <Nuget Path>/NuGet.exe'`

### To Release a New Version
- update assemblyInfo.cs, RELEASE_NOTES.md, and appium-dotnet-driver.nuspec with the new version number and release details, then check it in
- pull new code
- `Rebuild All` with `Release` target.
- `NuGet pack appium-dotnet-driver.nuspec`
- `NuGet push Appium.WebDriver.<version>.nupkg`
