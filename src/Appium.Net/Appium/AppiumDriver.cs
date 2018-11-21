//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//See the NOTICE file distributed with this work for additional
//information regarding copyright ownership.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using Appium.Interfaces.Generic.SearchContext;
using Newtonsoft.Json;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OpenQA.Selenium.Appium
{
    public abstract class AppiumDriver<W> : RemoteWebDriver, IExecuteMethod, IFindsByFluentSelector<W>,
        IHasSessionDetails,
        IFindByAccessibilityId<W>,
        IHidesKeyboard, IInteractsWithFiles,
        IInteractsWithApps, IPerformsTouchActions, IRotatable, IContextAware, IGenericSearchContext<W>,
        IGenericFindsByClassName<W>,
        IGenericFindsById<W>, IGenericFindsByCssSelector<W>, IGenericFindsByLinkText<W>, IGenericFindsByName<W>,
        IGenericFindsByPartialLinkText<W>, IGenericFindsByTagName<W>, IGenericFindsByXPath<W> where W : IWebElement
    {

        private const string NativeApp = "NATIVE_APP";
        
        #region Constructors

        public AppiumDriver(ICommandExecutor commandExecutor, ICapabilities appiumOptions)
            : base(commandExecutor, appiumOptions)
        {
            AppiumCommand.Merge(commandExecutor.CommandInfoRepository);
            ElementFactory = CreateElementFactory();
        }

        public AppiumDriver(ICapabilities appiumOptions)
            : this(AppiumLocalService.BuildDefaultService(), appiumOptions)
        {
        }

        public AppiumDriver(ICapabilities appiumOptions, TimeSpan commandTimeout)
            : this(AppiumLocalService.BuildDefaultService(), appiumOptions, commandTimeout)
        {
        }

        public AppiumDriver(AppiumServiceBuilder builder, ICapabilities appiumOptions)
            : this(builder.Build(), appiumOptions)
        {
        }

        public AppiumDriver(AppiumServiceBuilder builder, ICapabilities appiumOptions, TimeSpan commandTimeout)
            : this(builder.Build(), appiumOptions, commandTimeout)
        {
        }

        public AppiumDriver(Uri remoteAddress, ICapabilities appiumOptions)
            : this(remoteAddress, appiumOptions, DefaultCommandTimeout)
        {
        }

        public AppiumDriver(AppiumLocalService service, ICapabilities appiumOptions)
            : this(service, appiumOptions, DefaultCommandTimeout)
        {
        }

        public AppiumDriver(Uri remoteAddress, ICapabilities appiumOptions, TimeSpan commandTimeout)
            : this(new AppiumCommandExecutor(remoteAddress, commandTimeout), appiumOptions)
        {
        }

        public AppiumDriver(AppiumLocalService service, ICapabilities appiumOptions, TimeSpan commandTimeout)
            : this(new AppiumCommandExecutor(service, commandTimeout), appiumOptions)
        {
        }

        #endregion Constructors

        #region Generic FindMethods

        public new W FindElement(By by) =>
            (W) base.FindElement(by);
        public new ReadOnlyCollection<W> FindElements(By by) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElements(by));

        public new W FindElement(string by, string value) => (W) base.FindElement(by, value);

        public new IReadOnlyCollection<W> FindElements(string selector, string value) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElements(selector, value));

        public new W FindElementByClassName(string className) =>
            (W) base.FindElement(MobileSelector.ClassName, className);

        public new ReadOnlyCollection<W> FindElementsByClassName(string className) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElements(MobileSelector.ClassName, className));

        public new W FindElementById(string id) =>
            (W) base.FindElement(MobileSelector.Id, id);

        public new ReadOnlyCollection<W> FindElementsById(string id) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElements(MobileSelector.Id, id));

        public new W FindElementByCssSelector(string cssSelector) =>
            (W) base.FindElementByCssSelector(cssSelector);

        public new ReadOnlyCollection<W> FindElementsByCssSelector(string cssSelector) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElementsByCssSelector(cssSelector));

        public new W FindElementByLinkText(string linkText) =>
            (W) base.FindElementByLinkText(linkText);

        public new ReadOnlyCollection<W> FindElementsByLinkText(string linkText) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElementsByLinkText(linkText));

        public new W FindElementByName(string name) =>
            (W) base.FindElement(MobileSelector.Name, name);

        public new ReadOnlyCollection<W> FindElementsByName(string name) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElements(MobileSelector.Name, name));

        public new W FindElementByPartialLinkText(string partialLinkText) =>
            (W) base.FindElementByPartialLinkText(partialLinkText);

        public new ReadOnlyCollection<W> FindElementsByPartialLinkText(string partialLinkText) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElementsByPartialLinkText(partialLinkText));

        public new W FindElementByTagName(string tagName) =>
            (W) base.FindElement(MobileSelector.TagName, tagName);

        public new ReadOnlyCollection<W> FindElementsByTagName(string tagName) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElements(MobileSelector.TagName, tagName));

        public new W FindElementByXPath(string xpath) =>
            (W) base.FindElementByXPath(xpath);

        public new ReadOnlyCollection<W> FindElementsByXPath(string xpath) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElementsByXPath(xpath));

        #region IFindByAccessibilityId Members

        public W FindElementByAccessibilityId(string selector) => FindElement(MobileSelector.Accessibility, selector);

        public IReadOnlyCollection<W> FindElementsByAccessibilityId(string selector) =>
            FindElements(MobileSelector.Accessibility, selector);

        #endregion IFindByAccessibilityId Members

        #endregion

        #region Public Methods

        protected override Response Execute(string driverCommandToExecute, Dictionary<string, object> parameters) =>
            base.Execute(driverCommandToExecute, parameters);

        Response IExecuteMethod.Execute(string commandName, Dictionary<string, object> parameters) =>
            base.Execute(commandName, parameters);

        Response IExecuteMethod.Execute(string driverCommand) => Execute(driverCommand, null);

        #region MJsonMethod Members

        /// <summary>
        /// Rotates Device.
        /// </summary>
        /// <param name="opts">rotations options like the following:
        /// new Dictionary<string, int> {{"x", 114}, {"y", 198}, {"duration", 5}, 
        /// {"radius", 3}, {"rotation", 220}, {"touchCount", 2}}
        /// </param>
        public void Rotate(Dictionary<string, int> opts)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            foreach (KeyValuePair<string, int> opt in opts)
            {
                parameters.Add(opt.Key, opt.Value);
            }
            Execute(AppiumDriverCommand.Rotate, parameters);
        }

        public void InstallApp(string appPath) =>
            Execute(AppiumDriverCommand.InstallApp, AppiumCommandExecutionHelper.PrepareArgument("appPath", appPath));

        public void RemoveApp(string appId) =>
            Execute(AppiumDriverCommand.RemoveApp, AppiumCommandExecutionHelper.PrepareArgument("appId", appId));

        public bool IsAppInstalled(string bundleId) =>
            Convert.ToBoolean(Execute(AppiumDriverCommand.IsAppInstalled,
                AppiumCommandExecutionHelper.PrepareArgument("bundleId", bundleId)).Value.ToString());

        public byte[] PullFile(string pathOnDevice) =>
            Convert.FromBase64String(Execute(AppiumDriverCommand.PullFile,
                AppiumCommandExecutionHelper.PrepareArgument("path", pathOnDevice)).Value.ToString());

        public byte[] PullFolder(string remotePath) =>
            Convert.FromBase64String(Execute(AppiumDriverCommand.PullFolder,
                AppiumCommandExecutionHelper.PrepareArgument("path", remotePath)).Value.ToString());

        public void LaunchApp() => ((IExecuteMethod) this).Execute(AppiumDriverCommand.LaunchApp);

        public void CloseApp() => ((IExecuteMethod) this).Execute(AppiumDriverCommand.CloseApp);

        public void ResetApp() => ((IExecuteMethod) this).Execute(AppiumDriverCommand.ResetApp);

        public void BackgroundApp() =>
            Execute(AppiumDriverCommand.BackgroundApp,
                AppiumCommandExecutionHelper.PrepareArgument("seconds", AppiumCommandExecutionHelper.PrepareArgument("timeout", null)));

        public void BackgroundApp(int seconds) =>
            Execute(AppiumDriverCommand.BackgroundApp,
                AppiumCommandExecutionHelper.PrepareArgument("seconds", AppiumCommandExecutionHelper.PrepareArgument("timeout", seconds)));

        /// <summary>
        /// Get all defined Strings from an app for the specified language and
        /// strings filename
        /// </summary>
        /// <returns>a dictionary with localized strings defined in the app.</returns>
        /// <param name="language">strings language code</param>
        /// <param name="stringFile">strings filename</param>
        public Dictionary<string, object> GetAppStringDictionary(string language = null, string stringFile = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            if (language != null)
            {
                parameters.Add("language", language);
            }
            if (stringFile != null)
            {
                parameters.Add("stringFile", stringFile);
            }
            if (parameters.Count == 0)
            {
                parameters = null;
            }
            return (Dictionary<string, object>) Execute(AppiumDriverCommand.GetAppStrings, parameters).Value;
        }

        public void HideKeyboard() => AppiumCommandExecutionHelper.HideKeyboard(this, null, null);

        /// <summary>
        /// GPS Location
        /// </summary>
        public Location Location
        {
            get
            {
                var commandResponse = ((IExecuteMethod) this).Execute(AppiumDriverCommand.GetLocation);
                return JsonConvert.DeserializeObject<Location>((string) commandResponse.Value);
            }
            set
            {
                var location = value ?? new Location();
                Execute(AppiumDriverCommand.SetLocation, location.ToDictionary());
            }
        }

        #endregion MJsonMethod Members

        #region Context

        public string Context
        {
            get
            {
                var commandResponse = ((IExecuteMethod) this).Execute(AppiumDriverCommand.GetContext);
                return commandResponse.Value as string;
            }
            set
            {
                var parameters = AppiumCommandExecutionHelper.PrepareArgument("name", value);
                Execute(AppiumDriverCommand.SetContext, parameters);
            }
        }

        public ReadOnlyCollection<string> Contexts
        {
            get
            {
                var commandResponse = ((IExecuteMethod) this).Execute(AppiumDriverCommand.Contexts);
                var contexts = new List<string>();
                var objects = commandResponse.Value as object[];

                if (null != objects && 0 < objects.Length)
                {
                    contexts.AddRange(objects.Select(o => o.ToString()));
                }

                return contexts.AsReadOnly();
            }
        }

        #endregion Context

        #region Orientation

        public ScreenOrientation Orientation
        {
            get
            {
                var commandResponse = ((IExecuteMethod) this).Execute(AppiumDriverCommand.GetOrientation);
                return (commandResponse.Value as string).ConvertToScreenOrientation();
            }
            set
            {
                var parameters = AppiumCommandExecutionHelper.PrepareArgument("orientation", value.JSONWireProtocolString());
                Execute(AppiumDriverCommand.SetOrientation, parameters);
            }
        }

        #endregion Orientation

        #region Input Method (IME)

        /// <summary>
        /// Get a list of IME engines available on the device
        /// </summary>
        /// <returns>List of available </returns>
        public List<string> GetIMEAvailableEngines()
        {
            var retVal = new List<string>();
            var commandResponse = ((IExecuteMethod) this).Execute(AppiumDriverCommand.GetAvailableEngines);
            var objectArr = commandResponse.Value as object[];
            if (null != objectArr)
            {
                retVal.AddRange(objectArr.Select(val => val.ToString()));
            }
            return retVal;
        }

        /// <summary>
        /// Get the currently active IME Engine on the device
        /// </summary>
        /// <returns>Active IME Engine</returns>
        public string GetIMEActiveEngine() =>
            ((IExecuteMethod) this).Execute(AppiumDriverCommand.GetActiveEngine).Value as string;

        /// <summary>
        /// Is the IME active on the device (NOTE: on Android, this is always true)
        /// </summary>
        /// <returns>true if IME is active, false otherwise</returns>
        public bool IsIMEActive() =>
            (bool) (((IExecuteMethod) this).Execute(AppiumDriverCommand.IsIMEActive).Value);

        /// <summary>
        /// Activate the given IME on Device
        /// </summary>
        /// <param name="imeEngine">IME to activate</param>
        public void ActivateIMEEngine(string imeEngine) =>
            Execute(AppiumDriverCommand.ActivateEngine, AppiumCommandExecutionHelper.PrepareArgument("engine", imeEngine));

        /// <summary>
        /// Deactivate the currently Active IME Engine on device
        /// </summary>
        public void DeactiveIMEEngine() =>
            ((IExecuteMethod) this).Execute(AppiumDriverCommand.DeactivateEngine);

        #endregion Input Method (IME)

        #region Multi Actions

        public void PerformMultiAction(IMultiAction multiAction)
        {
            if (multiAction == null) return;
            var parameters = multiAction.GetParameters();
            Execute(AppiumDriverCommand.PerformMultiAction, parameters);
        }

        public void PerformTouchAction(ITouchAction touchAction)
        {
            if (touchAction == null) return;
            var parameters = AppiumCommandExecutionHelper.PrepareArgument("actions", touchAction.GetParameters());
            Execute(AppiumDriverCommand.PerformTouchAction, parameters);
        }

        #endregion Multi Actions

        #region Device Time

        /// <summary>
        /// Gets device date and time for both iOS(Supports only real device) and Android devices
        /// </summary>
        /// <returns>A string which consists of date and time</returns>
        public string DeviceTime => ((IExecuteMethod) this).Execute(AppiumDriverCommand.GetDeviceTime).Value.ToString();

        #endregion Device Time

        #region Session Data

        public IDictionary<string, object> SessionDetails
        {
            get
            {
                var session = 
                    (IDictionary<string, object>) ((IExecuteMethod) this).Execute(AppiumDriverCommand.GetSession)
                    .Value;
                return new ReadOnlyDictionary<string, object>(session.Where(entry =>
                {
                    var key = entry.Key;
                    var value = entry.Value;
                    return !string.IsNullOrEmpty(key) &&
                           value != null && !string.IsNullOrEmpty(Convert.ToString(value));
                }).ToDictionary(entry => entry.Key, entry => entry.Value));
            }
        }

        public object GetSessionDetail(string detail)
        {
            var details = SessionDetails;
            return details.ContainsKey(detail) ? details[detail] : null;
        }

        public string PlatformName => GetSessionDetail(MobileCapabilityType.PlatformName) as string 
                                      ?? GetSessionDetail(CapabilityType.Platform) as string;

        public string AutomationName => GetSessionDetail(MobileCapabilityType.AutomationName) as string;

        public bool IsBrowser => GetSessionDetail(MobileCapabilityType.BrowserName) != null
                                 && Context.IndexOf(NativeApp, StringComparison.OrdinalIgnoreCase) < 0;

        #endregion Session Data

        #region Recording Screen

        public string StartRecordingScreen() => ((IExecuteMethod)this).Execute(AppiumDriverCommand.StartRecordingScreen).Value.ToString();

        public string StartRecordingScreen(IScreenRecordingOptions options)
        {
            var parameters = AppiumCommandExecutionHelper.PrepareArgument("options", options.GetParameters());
            return Execute(AppiumDriverCommand.StartRecordingScreen, parameters).Value.ToString();
        }

        public string StopRecordingScreen() => ((IExecuteMethod)this).Execute(AppiumDriverCommand.StopRecordingScreen).Value.ToString();

        public string StopRecordingScreen(IScreenRecordingOptions options)
        {
            var parameters = AppiumCommandExecutionHelper.PrepareArgument("options", options.GetParameters());
            return Execute(AppiumDriverCommand.StopRecordingScreen, parameters).Value.ToString();
        }

        #endregion Recording Screen

        #endregion Public Methods

        #region Support methods

        protected abstract RemoteWebElementFactory CreateElementFactory();

        internal static ICapabilities SetPlatformToCapabilities(DriverOptions dc, string desiredPlatform)
        {
            dc.AddAdditionalCapability(MobileCapabilityType.PlatformName, desiredPlatform);
            return dc.ToCapabilities();
        }

        internal static ReadOnlyCollection<T> ConvertToExtendedWebElementCollection<T>(IEnumerable collection) where T : IWebElement
        {
            return collection.Cast<T>().ToList().AsReadOnly();
        }

        #endregion
    }
}