#Release Notes

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
 
