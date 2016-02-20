#Release Notes

##1.5.1.1 (still not released)
- Update to Selenium.Webdriver v2.52.0 and Selenium.Support v2.52.0
- FIXED The issue of compatibility of AppiumServiceBuilder with Appium node server v >= 1.5.x.
- Page object tools were updated. By.Name locator strategy is deprecated for Android and iOS. It is still valid for the Selendroid mode. 
- The DeviceTime property was added and it works with Appium node 1.5
- improvements of locking methods. The LockDevice(seconds) is obsolete and it is going to be removed in the next release. Since Appium node server v1.5.x it is recommended to use 
AndroidDriver.Lock()()...AndroidDriver.Unlock() or IOSDriver.Lock(int seconds) instead.

##1.5.0.1
- Update to Selenium.Webdriver v2.48.2 and Selenium.Support v2.48.2
- The ability to start appium server programmatically was provided. The ICommandServer implementation (AppiumLocalService).
- The new boolean parameter of the AndroidDdriver.StartActivity method. It allows to start a new activity without closing of a target app.
- All possible key codes were added to AndroidKeyCode.
- The API refactoring.
- The "ReplaceValue" method was added to AndroidElement
- The "SetImmediateValue" was moved from the AppiumWebElement to IOSElement

##1.4.1.1
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
 
