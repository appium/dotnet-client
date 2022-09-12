## 5.0.0 Beta 1
- chore: remove redundant pageobject feature by @akinsolb in #472
- fix: Add and test missing mobile Bys by @TroyWalshProf in #477
- fix: Allow users to connect to mobile web browsers by @TroyWalshProf in #478
- refactor!: Rename AppiumWebElement AppiumElement by @TroyWalshProf in #481

## 5.0.0 Alpha 2

- build: reenable GeneratePackageOnBuild by @akinsolb in #449
- feat: Add idempotency header to request for new sessions by @akinsolb in #450
- Settings implementation for iOS driver by @LyubomirStoimchev in #466
- refactor!: Update to work with Selenium 4 (with element factory) by @TroyWalshProf in #469
- chore: Update MobileCapabilityType to support W3C protocol by @Dor-bl in #475
- chore: drop FindElementBy* and FindElementsBy* by @TroyWalshProf in #470

## 5.0.0 Alpha

- refactor: Update to work with Selenium 4 (with element factory) (#469)
- Get proect to build
- Use new platform setting  more consistantly
- Get WinAppTests kind of working
- Drop one off web elements
- Add 'App' as a known capablity
- Transition from AddAdditionalCapability to AddAdditionalOption
- Use Selenium 4 RC2 and remove extra using statements
- Cleanup mobile selectors and image compare test
- Fix mobile By bug
- Undo local test code
- Address PR comments
- Add back IdempotencyHeader
- PR related cleanup
- Update to latest Selenium RC
- Update PageObjects code to work with new Element and driver paradigm
- Update tests to leverage configuration DeviceName and AddAdditionalAppiumOption
- Work with web element factory
- Update to work with upcoming RC4
- Move to official release as there will be no RC4
- Update copyright
- Put Newtonsoft back
- Use ternary operator
- Remove outdated using statements

## 4.2.0
- IPerformsTouchActions no longer implement (inherit) IExecuteMethod - inheritance redundancy and a cleaner interface; Resolve (#392)
- IHidesKeyboard is now exposes "key" and "strategy" overloads; Resolve (#390)
- IHidesKeyboard no longer implement (inherit) IExecuteMethod - inheritance redundancy and a cleaner interface; Resolve (#389)
- Add MacDriver to support [mac driver](https://github.com/appium/appium-mac-driver) (#383)
- Add support for retrieving log type and server side logs (#376) resolves #280
- Resolved #306 by updating the way appium's Location endpoint response is mapped to the client's Location model
- Location property under AppiumDriver is now exposable by IHasLocation interface; Resolve (#386)
- Resolve #364. Add IsKeyboardShown to IHidesKeyboard intefrace; Add IsKeyboardShown command 
- Added AndroidDataMatcher selector strategy #408
- feat: add ability to send flags to PressKeyCode (#433) @akinsolb
- feat: Add image locator type (#434) @akinsolb
- feat: add missing iOS Screen recording options (#432) @akinsolb
- feat: System Bars & Display Density (#430) @akinsolb
- feat: Send sms (#431) @akinsolb
- feat: GSM Voice State (#429) @akinsolb
- feat: Set Gsm Signal Strength (#428) @akinsolb
- feat: Make GSM Call (Emulator Only) (#427) @akinsolb
- feat: Toggle Wifi (#425) @akinsolb
- feat: add Toggle Airplane Mode & INetworkActions Interface (#423) @akinsolb
- feat: Get Performance Data & Performance Data Types (#422) @akinsolb
- feat: Provide way to pass arguments to NodeJS (#417) @sparerd
- feat: Added builder pattern for creating valid ByAndroidUIAutomator statements (#410) @sparerd

## 4.1.1
- Resolved #372 by fixing the endpoint for Activating an App

## 4.1.0
- Added helpers to access Appiums image comparison tools (#355)
- Removed the need to embed an application resource for finding the node instance path (#357). Fixes (#356): InvalidCastException error when locating node path on non-windows host & net45 target
- Added the ability to activate a given application on a device (#359). [documentation](http://appium.io/docs/en/commands/device/app/activate-app)

## 4.0
- Added the ability to terminate a given application on a device (#340). See [documentation](http://appium.io/docs/en/commands/device/app/terminate-app)
- Added the ability to query the given app status on a device (#341). See [documentation](http://appium.io/docs/en/commands/device/app/app-state/)
- Added property on AndroidDriver.cs to provide the ability to get the name of the current Android package (#343) See [documentation](http://appium.io/docs/en/commands/device/activity/current-package/)
- Updated AppiumWebElement.cs to strictly compatible locator strategies (#345) fixes #328
- Updated ToDictionary() method on AppiumOptions.cs to temporarily use reflection (#346) resolves #338

## 4.0.0.6-beta
- [Enhancement] Adds pointer input device extension (#315) - with help from Timotius Margo
- [Enhancement] Add finger print authentication command for Android emulator (#324)
- [Enhancement] Add toggle data command to switch the state of data service on Android devices (#324)

### 4.0.0.5-beta
- [Enhancement] Add screen recording mechanism (#277)
- [Enhancement] Add setting/getting clipboard mechanism (#278)
- [Enhancement] Update support for backgrounding (android) app (#292)
- [Enhancement] Add file push mechanism for ios and android driver (#301)
- [Enhancement] Add windows support for page Object pattern (#276)
- [Enhancement] Redirect local appium server log to Visual Studio output (#303)
- [BUG FIX] Override IFindsBy methods in appium driver (#302) fixes #265
- [BUG FIX] Refactor - removing the use of reflection (#297) fixes #291
- [BUG FIX] Allow log file to be parsed  appium log file  (#286)

### 4.0.0.4-beta
- [BUG FIX] Fix for W3C locator strategies

### 4.0.0.3-beta
- [BUG FIX] Fix for Displayed endpoint on AppiumWebElement

### 4.0.0.2-beta
- [Enhancement] Added support for Tizen driver
- [BREAKING CHANGE] Move to Dotnet Standard with the exception of PageFactory.
- [BREAKING CHANGE] Replace DesiredCapabilities with AppiumOptions and AppiumCapablilities.
- [UPDATES]:
 - Selenium.Support was updated 3.14.0
 - Selenium.WebDriver was updated 3.14.0

### 4.0.0.1-beta
- [Enhancement] The searching by ios class chain was implemented.
- [Enhancement] Details of a session. Interface `IHasSessionDetails` and its implementation.
- [BUG FIX] Fix of the `LongPress` action parameters.
- [BREAKING CHANGE] Unnecessary interface `ITouchShortcuts` was removed.
- [BREAKING CHANGE] Deprecated gesture methods were removed.
- [UPDATES]:
 - Newtonsoft.Json was updated to 10.0.3
 - Selenium.Support was updated 3.8.0
 - Selenium.WebDriver was updated 3.8.0
 - Castle.Core was updated to 4.2.1

### 3.0.0.2
- [Refactor] The obsolete interface OpenQA.Selenium.Appium.Android.Interfaces.ISendsKeyEvents was removed
- [Refactor] The obsolete interface OpenQA.Selenium.Appium.iOS.Interfaces.IIOSHidesKeyboard was removed
- [Enhancement] The new `YouiEngine` automation type was added to the
OpenQA.Selenium.Appium.Enums.AutomationName
- [BUG FIX] The issue that was preventing the overriding of the Execute method was fixed.

### 3.0.0.1
- [Major update] Update to Selenium 3.0.1
- [Major enhancement] Windows automation:
 - OpenQA.Selenium.Appium.Interfaces.IFindByWindowsUIAutomation was added.
 - OpenQA.Selenium.Appium.Windows.WindowsDriver was added.
 - OpenQA.Selenium.Appium.Windows.WindowsElement was added.
 - OpenQA.Selenium.Appium.Windows.Enums.WindowsKeyCode was added.
- [Major enhancement] API redesign
 - OpenQA.Selenium.Appium.Android.Interfaces.ISendsKeyEvents was marked obsolete.
 - Appium.Interfaces.ISendsKeyEvents was added. It is implemented by AndroidDriver and WindowsDriver.
 - OpenQA.Selenium.Appium.iOS.Interfaces.IIOSHidesKeyboard was marked obsolete.
 - OpenQA.Selenium.Appium.Interfaces.IHidesKeyboardWithKeyName was added. It extends
 - OpenQA.Selenium.Appium.Interfaces.IHidesKeyboard and it is implemented by IOSDriver and WindowsDriver.
 - OpenQA.Selenium.Appium.Android.Interfaces.IHasSettings was added. It is implemented by AndroidDriver. Also
- The OpenQA.Selenium.Appium.Android.Enims.AutomatorSetting was provided.
- OpenQA.Selenium.Appium.iOS.Interfaces.IPerformsTouchID was added. It is implemented by IOSDriver.
- Updated almost all interfaces extend the OpenQA.Selenium.Appium.Interfaces.IExecuteMethod
- Command execution helpers:
 - OpenQA.Selenium.Appium.AppiumCommandExecutionHelper was added.
 - OpenQA.Selenium.Appium.Android.AndroidCommandExecutionHelper was added.
 - OpenQA.Selenium.Appium.iOS.IOSCommandExecutionHelper was added.
These classes consist of static methods which accept IExecuteMethod as one of parameters.
- [Major enhancement] Now the OpenQA.Selenium.Appium.AppiumDriver can use an instance of any
OpenQA.Selenium.Appium.MobileBy subclass for the searching. It should work as expected when current session supports the given selector.
- [Major enhancement] The new interface OpenQA.Selenium.Appium.Interfaces.IFindsByFluentSelector was added. It
is implemented by OpenQA.Selenium.Appium.AppiumDriver.
- [Major enhancement] The new interface OpenQA.Selenium.Appium.Interfaces.IFindsByIosNSPredicate was added.
- [Major enhancement] The new By-selector OpenQA.Selenium.Appium.ByWindowsAutomation was added.
- [Major enhancement] The new By-selector OpenQA.Selenium.Appium.ByIosNSPredicate was added.
- [Enhancement]. The method Response Execute(string) was added to
- OpenQA.Selenium.Appium.Interfaces.IExecuteMethod. It is implemented by OpenQA.Selenium.Appium.AppiumDriver.
- [Enhancement]. New automation types were added:
 - OpenQA.Selenium.Appium.Enums.AutomationName.iOSXcuiTest
 - OpenQA.Selenium.Appium.Enums.AutomationName.AndroidUIAutomator2

### 2.0.1.1
- All obsolete code was removed.
Following capabilities were added:
 - OpenQA.Selenium.Appium.Enums.AndroidMobileCapabilityType.AndroidInstallTimeout
 - OpenQA.Selenium.Appium.Enums.AndroidMobileCapabilityType.NativeWebScreenshot
 - OpenQA.Selenium.Appium.Enums.AndroidMobileCapabilityType.AndroidScreenshotPath
 - OpenQA.Selenium.Appium.Enums.AndroidMobileCapabilityType.AppWaitDuration.
-The new interface OpenQA.Selenium.Appium.Interfaces.IExecuteMethod was added.

### 2.0.0.1
- All obsolete code was removed. #C client won't support Appium server v lower than 1.5.0
- Source code migration to C# 6.0. Now this bundle requires .Net Framework &gt; v4.5 or Mono Framework version
which supports .Net Framework v4.5 with Lang Level 6.
- Update to Selenium.Webdriver v2.53.1 and Selenium.Support v2.53.1.
- Update to Newtonsoft.Json v9.0.1.
- Re-design of mobile searching strategies:
 - The class OpenQA.Selenium.Appium.MobileBy was added.
 - Classes ByAccessibilityId, ByAndroidUIAutomator, ByIosUIAutomation were grouped by the new class.
 - Static methods that create instances of ByAccessibilityId, ByAndroidUIAutomator, ByIosUIAutomation were
added.
- The method public W GetNamedTextField(String name) was marked obsolete. It is going to be removed.
- ScrollTo() and ScrollToExact() became deprecated. They are going to be removed in the next release. The
swiping action and OpenQA.Selenium.Appium.ByAndroidUIAutomator or
OpenQA.Selenium.Appium.ByIosUIAutomation are recommended to use instead.
- Server flags were added:
- GeneralOptionList.AsyncTrace
- IOSOptionList.WebkitDebugProxyPort
- The SessionDetails property was added. This property returns a dictionary of the current session data.
- FIX of the pinch/zoom issue. This change is supposed to be the temporary workaround. We are searching for a
more convenient solution. It seems it requires some changes on the server side.
- FIX of the swiping issue (iOS, server version &gt;= 1.5.0). Now the swiping is implemented differently by
AndroidDriver and IOSDriver.
- Added the ability to start an activity using Android intent actions, intent categories, flags and arguments was
added to OpenQA.Selenium.Appium.Android.AndroidDriver.
            The StartActivityWithIntent method.
            [Android] ability to push a common string as a file to the remote mobile device. The method was redesigned.
            [Android] ability to push base64 encoded bytes as a file to the remote mobile device. The method was added.
            [Android] ability to push a file as the file to the remote mobile device. The method was added.
            Constructors like AppiumDriver(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities) were
            added to OpenQA.Selenium.Appium.Android.AndroidDriver and OpenQA.Selenium.Appium.iOS.IOSDriver. Also
            OpenQA.Selenium.Appium.AppiumCommand became public. The binding of these features may allow to use realated
            solutions of other vendors/modified Appium server builds which support JSONWP commands
            that default Appium/Selenium do not support.
            The SetImmediateValue method was moved to OpenQA.Selenium.Appium.AppiumWebElement. It works against text
            input elements on Android.

### 1.5.1.1
- Update to Selenium.Webdriver v2.53.0 and Selenium.Support v2.53.0
- Update to Newtonsoft.Json v8.0.2
- FIXED The issue of compatibility of AppiumServiceBuilder with Appium node server v &gt;= 1.5.x.
- Page object tools were updated. By.Name locator strategy is deprecated for Android and iOS. It is still valid for the Selendroid mode.
- The DeviceTime property was added and it works with Appium node 1.5
- improvements of locking methods. The LockDevice(seconds) is obsolete and it is going to be removed in the
next release. Since Appium node server v1.5.x it is recommended to use
- AndroidDriver.Lock()()...AndroidDriver.Unlock() or IOSDriver.Lock(int seconds) instead.
- AndroidDriver.KeyEvent() is obsolete and it is going to be removed soon. Please use
- AndroidDriver.PressKeyCode or AndroidDriver.LongPressKeyCode instead.
- The GetAppStrings(string language = null) method is obsolete now. It is going to be removed.
- The GetAppStringDictionary(string language = null, string stringFile = null) was added instead. It returns a
- dictionary with app strings (keys and values) instead of a string. Also it allows the searching app strings
in the specified file.
- All capabilities were added according to https://github.com/appium/appium/blob/1.5/docs/en/writing-running-appium/caps.md. 
There are three classes:
 - OpenQA.Selenium.Appium.Enums.MobileCapabilityType (just modified),
 - OpenQA.Selenium.Appium.Enums.AndroidMobileCapabilityType (android-specific capabilities),
 - OpenQA.Selenium.Appium.Enums.IOSMobileCapabilityType (iOS-specific capabilities).
- Some server flags were marked as obsolete because they are deprecated since server node v1.5.x. These
options are going to be removed at the next .Net client release.
- The ability to start Appium node programmatically using desired capabilities. This feature is compatible 
with Appium node server v &gt;= 1.5.x.

### 1.5.0.1
- Update to Selenium.Webdriver v2.48.2 and Selenium.Support v2.48.2
- The ability to start appium server programmatically was provided. The ICommandServer implementation (AppiumLocalService).
- The new boolean parameter of the AndroidDdriver.StartActivity method. It allows to start a new activity
without closing of a target app.
- All possible key codes were added to AndroidKeyCode.
- The API refactoring.
- The "ReplaceValue" method was added to AndroidElement
- The "SetImmediateValue" was moved from the AppiumWebElement to IOSElement

#### 1.4.1.1
- Update to Selenium.Webdriver v2.48.1 and Selenium.Support v2.48.1
- .Net client is completely following the Apache 2.0 license now.
- IMobileElement implementations are able to perform gestures such as Pinch, Tap and Zoom.
- Constructor set of MultiAction and TouchAction was improved. Redundant constructors were removed.

# 1.4.0.3
- the bug which prevented the using of TouchAction/MultiTouchActions with IWebElement was fixed. This problem is reproduced
- with IWebElement instances created via Selenium PageFactory.

### 1.4.0.2
- features ported from the Java-Appium-Driver. Integration with Selenium PageFactory.

### 1.3.0.1
- Generic AppiumDriver class/subclasses and fix for TryAddCommand.

### 1.2.0.8
- Fix/Test for hide keyboard.

### 1.2.0.7
- Separate android and ios drivers, improved namespaces, fixed tests, and redesigned methods and interfaces

### 1.2.0.6
- Update Selenium NuGet Package to get fix for locator strategy.

### 1.2.0.5
- Add GetSettings and IgnoreUnimportantViews methods.

### 1.2.0.4
- Update version due to assembly not matching NuGet.

### 1.2.0.3
- Update version due to NuGet Package mismanagement.

- Update NuGet packages. Add IsLocked() and Start Activity.

### 1.2.0.1
- Add Pull Folder, Android Input Methods (IME), and Network Connection Methods for use with Appium 1.2

### 1.0.0
- TouchAction + multiActions rewriting,
- Project structure overhaul.
