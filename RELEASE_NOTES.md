# Release Notes

## *7.0.0*
- [Enhancement] Disable `FullReset` in IOS/ClipboardTest.
- [Enhancement] Update alert text assertion in IOS/AlertTests.
- [Enhancement] Replace deprecated `ByIosUIAutomation` in ScrollingSearchingTest.
- [Enhancement] Fix assertion for logs entry.
- [Enhancement] Fix IOS/SearchingTest.
- [BREAKING CHANGE] `AndroidDriver` to use modern `mobile:` commands for Lock, IsLocked, and Unlock.
- [Enhancement] Remove the selenium group.
- [Enhancement] 'IOSDriver' to use modern `mobile:` commands for Lock, IsLocked, and Unlock.
- [Updates] Bump Selenium.WebDriver from 4.26.1 to 4.27.0; Bump NUnit from 4.2.2 to 4.3.0; Bump Microsoft.NET.Test.Sdk from 17.11.1 to 17.12.0; Bump NUnit.Analyzers from 4.3.0 to 4.4.0; Bump System.Text.Json from 8.0.5 to 9.0.0.
- [Pipeline] Split tests into two separate tasks per framework.

## *6.0.1*
- [Enhancement] Add tests for `HideKeyboard(string strategy, string key)`.
- [Enhancement] Adjust tests namespaces to match folder structure.
- [Enhancement] Fix New Contributors var.
- [Bug Fix] Revert changes for NEW_CONTRIBUTORS.
- [Updates] Bump Selenium.WebDriver from 4.25.0 to 4.26.1.
- [Documentation] Add v6.0.0 to Compatibility Matrix.

## *6.0.0*
- [Enhancement] Include a notification for new contributors.
- [Enhancement] Update PR template to support test fixes.
- [Enhancement] Fix the Breaking Change label to match Release Drafter.
- [Enhancement] Update the Breaking Change title in labeler.yml.
- [Enhancement] Add a Documentation category in release-drafter.yml.
- [Enhancement] Fix .sln path for release-nuget.yml.
- [Enhancement] Add explicit wait for app package to load before assertion.
- [Enhancement] Move AppTests to the correct folder to match the namespace.
- [BREAKING CHANGE] Remove obsolete TouchActions, MultiActions methods.
- [Enhancement] Refactor: Use netstandard2.0 as the target framework to support a wider range of frameworks.
- [Updates] Bump Selenium.WebDriver from 4.24.0 to 4.25.0; Bump System.Drawing.Common from 8.0.8 to 8.0.10; Bump System.Text.Json from 8.0.4 to 8.0.5.
- [Enhancement] Remove Selenium.Support package as a dependency.
- [Enhancement] Migrate from Newtonsoft.Json to System.Text.Json.
- [Maintenance] Move the solution to the root folder.
- [Maintenance] Remove unnecessary .nuget binaries from source control.
- [Documentation] Remove the Deprecated Methods section.

## *5.2.0*
- [Enhancement] Override selenium default command timeout.
- [Enhancement] Bring back `LaunchApp` to `WindowsDriver`.
- [Updates] Bump Microsoft.NET.Test.Sdk from 17.10.0 to 17.11.1; Bump NUnit.Analyzers from 4.2.0 to 4.3.0; Bump NUnit from 4.1.0 to 4.2.2; Bump Selenium.WebDriver from 4.23.0 to 4.24.0; Bump System.Drawing.Common from 8.0.7 to 8.0.8; Bump NUnit3TestAdapter from 4.5.0 to 4.6.0.
- [Documentation] Update PULL_REQUEST_TEMPLATE.md; Add compatibility matrix for Appium .NET Client.

## *5.1.0*
- [BREAKING CHANGE] ExecuteAsync() under the hood implementation after bumping to Selenium 4.23.
- [Enhancement] Add `Breaking Change` labeler.
- [Enhancement] Set the prerelease flag to `false` in release-drafter.yml.
- [Enhancement] Add new Drawing Category to related tests.
- [Enhancement] Fix ios ElementTests.
- [Updates] Bump System.Drawing.Common from 8.0.6 to 8.0.7; Bump System.Drawing.Common from 4.21.0 to 4.23.0.

## *5.0.0*
- [Major Updates] Selenium 4 Integration: Refactored to work seamlessly with Selenium 4, incorporating the new element factory for improved element interactions.
- [Major Updates] .NET 6.0 and .NET Framework 4.8 Support: Upgraded the solution to target .NET 6.0 while reintroducing support for .NET Framework 4.8, ensuring compatibility across different .NET versions.
- [Major Updates] Appium 2 Migration: Migrated AppiumLocalService to Appium 2, bringing the latest Appium server capabilities.
- [New Features] Idempotency Header: Added an idempotency header to requests for new sessions, enhancing session management reliability.
- [New Features] ReplaceValue Method: Introduced the ReplaceValue method to facilitate efficient value updates in elements.
- [New Features] Direct Connect Feature: Implemented the directConnect feature optimized direct connections to Appium servers.
- [New Features] Activate App with Timeout: A new feature to activate an app with a specified timeout, providing more control over app interactions.
- [Bug Fixes] Mobile Bys and Element Finding: Fixed issues with mobile Bys and extended element finding methods to ensure accurate element interactions.
- [Bug Fixes] FindElementBy Methods: Deprecated and removed the FindElementBy* and FindElementsBy* methods in favor of the new Selenium 4-element locating strategy.
- [Bug Fixes] Legacy App Management Helpers: Deprecated old app management helpers, including resetApp and closeApp, to encourage updated methods.
- [Bug Fixes] Touch and MultiAction: Added deprecations for TouchAction and MultiAction methods to streamline touch interactions.
- [Enhancements] MobileCapabilityType for W3C Protocol: Updated MobileCapabilityType to support the W3C protocol, aligning with the latest WebDriver standards.
- [Enhancements] Improved AppiumWebElement: Renamed AppiumWebElement to AppiumElement for better clarity and consistency.
- [Enhancements] Command Timeout Parameter: Enhanced command timeout parameter usage, providing more control over command execution times.
- [Enhancements] Test and Documentation Improvements: Numerous updates to tests and documentation, including grammar fixes, XML comments updates, and missing tests to ensure comprehensive coverage and clarity.
- [Maintenance] Dependency Updates: Regular updates to various dependencies, including NUnit, Microsoft.NET.Test.Sdk, Selenium dependencies, and System.Drawing.Common, ensuring the latest features and security patches are included.
- [Maintenance] Code Cleanup: Removed redundant code, sorted using, and fixed typos to improve code quality and maintainability.
- [Maintenance] Pipeline Enhancements: Updated CI/CD pipelines, including adding tests to the pipeline and updating GitHub actions for more efficient and reliable builds.

## *4.0.0.1-beta*
 - [Enhancement] The searching by ios class chain was implemented.
 - [Enhancement] Details of a session. Interface `IHasSessionDetails` and its implementation. [#201](https://github.com/appium/appium-dotnet-driver/pull/201)
 - [BUG FIX] Fix of the `LongPress` action parameters. The report [#197](https://github.com/appium/appium-dotnet-driver/issues/197). FIX [#198](https://github.com/appium/appium-dotnet-driver/pull/198)
 - [BREAKING CHANGE] Unnecessary interface `ITouchShortcuts` was removed. [#200](https://github.com/appium/appium-dotnet-driver/pull/200)
 - [BREAKING CHANGE] Deprecated gesture methods were removed. [#204](https://github.com/appium/appium-dotnet-driver/pull/204)
 - [UPDATES]:
  	- Newtonsoft.Json was updated to 10.0.3
  	- Selenium.Support was updated 3.8.0
  	- Selenium.WebDriver was updated 3.8.0
  	- Castle.Core was updated to 4.2.1

## *3.0.0.2*
 - [Refactor] The obsolete interface `OpenQA.Selenium.Appium.Android.Interfaces.ISendsKeyEvents` was removed
 - [Refactor] The obsolete interface `OpenQA.Selenium.Appium.iOS.Interfaces.IIOSHidesKeyboard` was removed
 - [Enhancement] The new `YouiEngine` automation type was added to the `OpenQA.Selenium.Appium.Enums.AutomationName`
 - [BUG FIX] The issue that was preventing the overriding of the `Execute` method was fixed.

## *3.0.0.1*
 - [Major update] Update to Selenium 3.0.1
 - [Major enhancement] Windows automation: 
	- `OpenQA.Selenium.Appium.Interfaces.IFindByWindowsUIAutomation` was added. [#158](https://github.com/appium/appium-dotnet-driver/pull/158)
	- `OpenQA.Selenium.Appium.Windows.WindowsDriver` was added. [#158](https://github.com/appium/appium-dotnet-driver/pull/158)
	- `OpenQA.Selenium.Appium.Windows.WindowsElement` was added. [#158](https://github.com/appium/appium-dotnet-driver/pull/158)
	- `OpenQA.Selenium.Appium.Windows.Enums.WindowsKeyCode` was added. [#158](https://github.com/appium/appium-dotnet-driver/pull/158)

 - [Major enhancement] API redesign
    - ```OpenQA.Selenium.Appium.Android.Interfaces.ISendsKeyEvents``` was marked obsolete.
    - ```Appium.Interfaces.ISendsKeyEvents``` was added. It is implemented by AndroidDriver and WindowsDriver.
    - ```OpenQA.Selenium.Appium.iOS.Interfaces.IIOSHidesKeyboard```  was marked obsolete.
    - ```OpenQA.Selenium.Appium.Interfaces.IHidesKeyboardWithKeyName``` was added. It extends ```OpenQA.Selenium.Appium.Interfaces.IHidesKeyboard``` and it is implemented by IOSDriver and WindowsDriver.
    - ```OpenQA.Selenium.Appium.Android.Interfaces.IHasSettings``` was added. It is implemented by AndroidDriver. Also the ```OpenQA.Selenium.Appium.Android.Enims.AutomatorSetting``` was provided.
    - ```OpenQA.Selenium.Appium.iOS.Interfaces.IPerformsTouchID``` was added. It is implemented by IOSDriver.
    - almost all interfaces extend the ```OpenQA.Selenium.Appium.Interfaces.IExecuteMethod```
	
	- Command execution helpers:
		- ```OpenQA.Selenium.Appium.AppiumCommandExecutionHelper``` was added.
		- ```OpenQA.Selenium.Appium.Android.AndroidCommandExecutionHelper``` was added.
		- ```OpenQA.Selenium.Appium.iOS.IOSCommandExecutionHelper``` was added.

	These classes consist of static methods which accept ```IExecuteMethod``` as one of parameters. 

 - [Major enhancement] Now the `OpenQA.Selenium.Appium.AppiumDriver` can use an instance of any `OpenQA.Selenium.Appium.MobileBy` subclass for the searching. 
   It should work as expected when current session supports the given selector.
 - [Major enhancement] The new interface `OpenQA.Selenium.Appium.Interfaces.IFindsByFluentSelector` was added. It is implemented by `OpenQA.Selenium.Appium.AppiumDriver`.
 - [Major enhancement] The new interface `OpenQA.Selenium.Appium.Interfaces.IFindsByIosNSPredicate` was added.
 - [Major enhancement] The new By-selector `OpenQA.Selenium.Appium.ByWindowsAutomation` was added. 
 - [Major enhancement] The new By-selector `OpenQA.Selenium.Appium.ByIosNSPredicate` was added. 
 - [Enhancement]. The method `Response Execute(string)` was added to `OpenQA.Selenium.Appium.Interfaces.IExecuteMethod`. It is implemented by `OpenQA.Selenium.Appium.AppiumDriver`.
 - [Enhancement]. New automation types were added:
 	- `OpenQA.Selenium.Appium.Enums.AutomationName.iOSXcuiTest`
 	- `OpenQA.Selenium.Appium.Enums.AutomationName.AndroidUIAutomator2`	  


## 2.0.1.1
- All obsolete code was removed.
- Following capabilities were added:
	- OpenQA.Selenium.Appium.Enums.AndroidMobileCapabilityType.AndroidInstallTimeout
	- OpenQA.Selenium.Appium.Enums.AndroidMobileCapabilityType.NativeWebScreenshot
	- OpenQA.Selenium.Appium.Enums.AndroidMobileCapabilityType.AndroidScreenshotPath
	- OpenQA.Selenium.Appium.Enums.AndroidMobileCapabilityType.AppWaitDuration.
- The new interface `OpenQA.Selenium.Appium.Interfaces.IExecuteMethod` was added.

## 2.0.0.1
- All obsolete code was removed. #C client won't support Appium server v lower than 1.5.0
- Source code migration to C# 6.0. Now this bundle requires .Net Framework > v4.5 or Mono Framework version which supports .Net Framework v4.5 with Lang Level 6.
- Update to Selenium.Webdriver v2.53.1 and Selenium.Support v2.53.1.
- Update to Newtonsoft.Json v9.0.1.
- Re-design of mobile searching strategies:
	- The class `OpenQA.Selenium.Appium.MobileBy` was added.
	- Classes `ByAccessibilityId`, `ByAndroidUIAutomator`, `ByIosUIAutomation` were grouped by the new class.
	- Static methods that create instances of `ByAccessibilityId`, `ByAndroidUIAutomator`, `ByIosUIAutomation` were added.
- The method public `W GetNamedTextField(String name)` was marked obsolete. It is going to be removed.	   
- `ScrollTo()` and `ScrollToExact()` became deprecated. They are going to be removed in the next release. The swiping action and `OpenQA.Selenium.Appium.ByAndroidUIAutomator` or
`OpenQA.Selenium.Appium.ByIosUIAutomatio`n are recommended to use instead.
- Server flags were added:
	- `GeneralOptionList.AsyncTrace`
	- `IOSOptionList.WebkitDebugProxyPort`
- The `SessionDetails` property was added.  This property returns a dictionary of the current session data.
- [#153](https://github.com/appium/appium-dotnet-driver/issues/153) fix & [#152](https://github.com/appium/appium-dotnet-driver/issues/152) fix. These changes are supposed to be the temporary workaround. We are searching for a more convenient solution. 
  It seems it requires some changes on the server side.
- FIX of the swiping issue (iOS, server version >= 1.5.0). Now the swiping is implemented differently by AndroidDriver and IOSDriver.
- the ability to start an activity using Android intent actions, intent categories, flags and arguments was added to OpenQA.Selenium.Appium.Android.AndroidDriver. 
The StartActivityWithIntent method.
- [Android] ability to push a common string as a file to the remote mobile device. The method was redesigned.
- [Android] ability to push base64 encoded bytes as a file to the remote mobile device. The method was added.
- [Android] ability to push a file as the file to the remote mobile device. The method was added.
- Constructors like `AppiumDriver(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities)` were added to `OpenQA.Selenium.Appium.Android.AndroidDriver` and `OpenQA.Selenium.Appium.iOS.IOSDriver`. Also 
      `OpenQA.Selenium.Appium.AppiumCommand` became public. The binding of these features may allow to use realated solutions of other vendors/modified Appium server builds which support JSONWP commands 
      that default Appium/Selenium do not support.
- The `SetImmediateValue` method was moved to `OpenQA.Selenium.Appium.AppiumWebElement`. It works against text input elements on Android.

## 1.5.1.1
- Update to Selenium.Webdriver v2.53.0 and Selenium.Support v2.53.0
- Update to Newtonsoft.Json v8.0.2
- FIXED The issue of compatibility of AppiumServiceBuilder with Appium node server v >= 1.5.x.
- Page object tools were updated. By.Name locator strategy is deprecated for Android and iOS. It is still valid for the Selendroid mode. 
- The DeviceTime property was added and it works with Appium node 1.5
- improvements of locking methods. The LockDevice(seconds) is obsolete and it is going to be removed in the next release. Since Appium node server v1.5.x it is recommended to use 
AndroidDriver.Lock()()...AndroidDriver.Unlock() or IOSDriver.Lock(int seconds) instead.
- AndroidDriver.KeyEvent() is obsolete and it is going to be removed soon. Please use AndroidDriver.PressKeyCode or AndroidDriver.LongPressKeyCode instead.
- The GetAppStrings(string language = null) method is obsolete now. It is going to be removed. 
- The  GetAppStringDictionary(string language = null, string stringFile = null) was added instead. It returns a dictionary with app strings (keys and values) instead of a string.
Also it allows the searching app strings in the specified file.
- All capabilities were added according to https://github.com/appium/appium/blob/1.5/docs/en/writing-running-appium/caps.md. There are three classes: 
	OpenQA.Selenium.Appium.Enums.MobileCapabilityType (just modified), 
	OpenQA.Selenium.Appium.Enums.AndroidMobileCapabilityType (android-specific capabilities), 
	OpenQA.Selenium.Appium.Enums.IOSMobileCapabilityType (iOS-specific capabilities).
- Some server flags were marked as obsolete because they are deprecated since server node v1.5.x. These options are going to be removed at the next .Net client release.
- The ability to start Appium node programmatically using desired capabilities. This feature is compatible with Appium node server v >= 1.5.x.

## 1.5.0.1
- Update to Selenium.Webdriver v2.48.2 and Selenium.Support v2.48.2
- The ability to start appium server programmatically was provided. The ICommandServer implementation (AppiumLocalService).
- The new boolean parameter of the AndroidDdriver.StartActivity method. It allows to start a new activity without closing of a target app.
- All possible key codes were added to AndroidKeyCode.
- The API refactoring.
- The "ReplaceValue" method was added to AndroidElement
- The "SetImmediateValue" was moved from the AppiumWebElement to IOSElement

## 1.4.1.1
- Update to Selenium.Webdriver v2.48.1 and Selenium.Support v2.48.1
- .Net client is completely following the Apache 2.0 license now.
- IMobileElement implementations are able to perform gestures such as Pinch, Tap and Zoom.	
- Constructor set of MultiAction and TouchAction was improved. Redundant constructors were removed.

## 1.4.0.3
- the bug which prevented the using of TouchAction/MultiTouchActions with IWebElement was fixed. This problem is reproduced with 
IWebElement instances created via Selenium PageFactory.

## 1.4.0.2

- features ported from the Java-Appium-Driver
	AppiumDriver:

		Tap
		Swipe
		Pinch
		Zoom
		ScrollTo
		ScrollToExact
	
	IOSDriver

		GetNamedTextField
		
	IOSElement

		also ScrollTo & ScrollToExact implementations with extra parameter for a resource ID to 
		scroll on a particular View.
		
- Integration with Selenium PageFactory. Now it is possible to develop UI tests using Page Object design pattern


## 1.3.0.1
- Generic AppiumDriver class and subclasses
- Fixes for backward compatabliltiy for TryAddCommand

## 1.2.0.8
- Fix and add tests for Hide Keyboard

## 1.2.0.7
- Improved namespaces.
- Fixed tests
- Redesigned methods and interfaces.
- Separate android and ios drivers.

## 1.2.0.6
- Update NuGet packages  - fixes locator strategy bug.

## 1.2.0.5
- Add GetSettings and IgnoreUnimportantViews methods.

## 1.2.0.4
- Update version to match assembly and NuGet package
 
## 1.2.0.3
- Needed to update version due to mismanaged NuGet Package.

## 1.2.0.2
- Update Newtonsoft.Json and WebDriver packages
- Add IsLocked Method
- Add Start Activity 

## 1.2.0.1

- Update for new Selenium version
- hideKeyboard update
- Add network connection methods
- Add android input methods
- Add PullFolder command

## 1.0.0

- Reorganized project
- TouchAction/MultiAction rewritting
- Added sample
 
