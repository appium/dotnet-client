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

using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Appium.ImageComparison;

namespace OpenQA.Selenium.Appium
{
    public abstract class AppiumDriver : WebDriver,
        IHasSessionDetails,
        IHasLocation,
        IHidesKeyboard, IInteractsWithFiles, IFindsByFluentSelector<AppiumElement>,
        IInteractsWithApps, IRotatable, IContextAware
    {
        private const string NativeApp = "NATIVE_APP";

        /// <summary>
        /// The default command timeout for HTTP requests in a AppiumDriver instance.
        /// </summary>
        protected static new readonly TimeSpan DefaultCommandTimeout = TimeSpan.FromSeconds(600.0);

        #region Constructors

        public AppiumDriver(ICommandExecutor commandExecutor, ICapabilities appiumOptions)
            : base(commandExecutor, appiumOptions)
        {
            AppiumCommand.Merge(commandExecutor);
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
            : this(remoteAddress, appiumOptions, commandTimeout, AppiumClientConfig.DefaultConfig())
        {
        }

        public AppiumDriver(AppiumLocalService service, ICapabilities appiumOptions, TimeSpan commandTimeout)
            : this(service, appiumOptions, commandTimeout, AppiumClientConfig.DefaultConfig())
        {
        }


        public AppiumDriver(Uri remoteAddress, ICapabilities appiumOptions, AppiumClientConfig clientConfig)
            : this(new AppiumCommandExecutor(remoteAddress, DefaultCommandTimeout, clientConfig), appiumOptions)
        {
        }

        public AppiumDriver(AppiumLocalService service, ICapabilities appiumOptions, AppiumClientConfig clientConfig)
            : this(new AppiumCommandExecutor(service, DefaultCommandTimeout, clientConfig), appiumOptions)
        {
        }

        public AppiumDriver(Uri remoteAddress, ICapabilities appiumOptions, TimeSpan commandTimeout, AppiumClientConfig clientConfig)
            : this(new AppiumCommandExecutor(remoteAddress, commandTimeout, clientConfig), appiumOptions)
        {
        }

        public AppiumDriver(AppiumLocalService service, ICapabilities appiumOptions, TimeSpan commandTimeout, AppiumClientConfig clientConfig)
            : this(new AppiumCommandExecutor(service, commandTimeout, clientConfig), appiumOptions)
        {
        }


        #endregion Constructors

        #region Public Methods

        protected override Response Execute(string driverCommandToExecute, Dictionary<string, object> parameters) =>
            base.Execute(driverCommandToExecute, parameters);

        Response IExecuteMethod.Execute(string commandName, Dictionary<string, object> parameters) =>
            base.Execute(commandName, parameters);


        #region Generic FindMethods

        public new AppiumElement FindElement(By by) =>
            (AppiumElement)base.FindElement(by);

        public new ReadOnlyCollection<AppiumElement> FindElements(By by) =>
            ConvertToExtendedWebElementCollection<AppiumElement>(base.FindElements(by));

        public new AppiumElement FindElement(string by, string value) => (AppiumElement)base.FindElement(by, value);

        public new IReadOnlyCollection<AppiumElement> FindElements(string selector, string value) =>
            ConvertToExtendedWebElementCollection<AppiumElement>(base.FindElements(selector, value));
        #endregion Generic FindMethods

        Response IExecuteMethod.Execute(string driverCommand) => Execute(driverCommand, null);

        #region MJsonMethod Members

        public void InstallApp(string appPath) =>
            Execute(AppiumDriverCommand.InstallApp, AppiumCommandExecutionHelper.PrepareArgument("appPath", appPath));

        public void RemoveApp(string appId) =>
            Execute(AppiumDriverCommand.RemoveApp, AppiumCommandExecutionHelper.PrepareArgument("appId", appId));

        public void ActivateApp(string appId) =>
            Execute(AppiumDriverCommand.ActivateApp, AppiumCommandExecutionHelper.PrepareArgument("appId", appId));
        
        public void ActivateApp(string appId, TimeSpan timeout) =>
            Execute(AppiumDriverCommand.ActivateApp,
                    AppiumCommandExecutionHelper.PrepareArguments(new string[] {"appId", "options"},
                        new object[]
                            {appId, new Dictionary<string, object>() {{"timeout", (long) timeout.TotalMilliseconds}}}));

        public bool TerminateApp(string appId) =>
            Convert.ToBoolean(Execute(AppiumDriverCommand.TerminateApp,
                AppiumCommandExecutionHelper.PrepareArgument("appId", appId)).Value.ToString());

        public bool TerminateApp(string appId, TimeSpan timeout) =>
            Convert.ToBoolean(Execute(AppiumDriverCommand.TerminateApp,
                    AppiumCommandExecutionHelper.PrepareArguments(new string[] { "appId", "options" },
                        new object[]
                            {appId, new Dictionary<string, object>() {{"timeout", (long) timeout.TotalMilliseconds}}}))
                .Value.ToString());

        public bool IsAppInstalled(string bundleId) =>
            Convert.ToBoolean(Execute(AppiumDriverCommand.IsAppInstalled,
                AppiumCommandExecutionHelper.PrepareArgument("bundleId", bundleId)).Value.ToString());

        public byte[] PullFile(string pathOnDevice) =>
            Convert.FromBase64String(Execute(AppiumDriverCommand.PullFile,
                AppiumCommandExecutionHelper.PrepareArgument("path", pathOnDevice)).Value.ToString());

        public byte[] PullFolder(string remotePath) =>
            Convert.FromBase64String(Execute(AppiumDriverCommand.PullFolder,
                AppiumCommandExecutionHelper.PrepareArgument("path", remotePath)).Value.ToString());

        public void PushFile(string pathOnDevice, string stringData) => AppiumCommandExecutionHelper.PushFile(this,
            pathOnDevice, Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(stringData))));

        public void PushFile(string pathOnDevice, byte[] base64Data) =>
            AppiumCommandExecutionHelper.PushFile(this, pathOnDevice, base64Data);

        public void PushFile(string pathOnDevice, FileInfo file) =>
            AppiumCommandExecutionHelper.PushFile(this, pathOnDevice, file);

        public void FingerPrint(int fingerprintId) =>
            AppiumCommandExecutionHelper.FingerPrint(this, fingerprintId);

        public void BackgroundApp() =>
            Execute(AppiumDriverCommand.BackgroundApp,
                AppiumCommandExecutionHelper.PrepareArgument("seconds", -1));

        public void BackgroundApp(TimeSpan timeSpan) =>
            Execute(AppiumDriverCommand.BackgroundApp,
                AppiumCommandExecutionHelper.PrepareArgument("seconds", timeSpan.TotalSeconds));

        public AppState GetAppState(string appId) =>
            (AppState)Convert.ToInt32(Execute(AppiumDriverCommand.GetAppState,
                AppiumCommandExecutionHelper.PrepareArgument("appId", appId)).Value.ToString());

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

            return (Dictionary<string, object>)Execute(AppiumDriverCommand.GetAppStrings, parameters).Value;
        }

        /// <summary>
        /// Hides the device keyboard.
        /// </summary>
        public void HideKeyboard()
            => AppiumCommandExecutionHelper.HideKeyboard(this, null, null);

        /// <summary>
        /// Hides the device keyboard.
        /// </summary>
        /// <param name="key">The button pressed by the mobile driver to attempt hiding the keyboard.</param>
        public void HideKeyboard(string key)
            => AppiumCommandExecutionHelper.HideKeyboard(executeMethod: this, key: key);

        /// <summary>
        /// Hides the device keyboard.
        /// </summary>
        /// <param name="strategy">Hide keyboard strategy (optional, UIAutomation only). Available strategies - 'press', 'pressKey', 'swipeDown', 'tapOut', 'tapOutside', 'default'.</param>
        /// <param name="key">The button pressed by the mobile driver to attempt hiding the keyboard.</param>
        public void HideKeyboard(string strategy, string key)
            => AppiumCommandExecutionHelper.HideKeyboard(executeMethod: this, strategy: strategy, key: key);

        /// <summary>
        /// Whether or not the soft keyboard is shown.
        /// </summary>
        /// <returns>True if the keyboard is shown. (boolean)</returns>
        public bool IsKeyboardShown()
            => AppiumCommandExecutionHelper.IsKeyboardShown(executeMethod: this);

        /// <summary>
        /// GPS Location
        /// </summary>
        public Location Location
        {
            get
            {
                var commandResponse = ((IExecuteMethod)this).Execute(AppiumDriverCommand.GetLocation);
                var locationValues = commandResponse.Value as Dictionary<string, object>;
                return new Location
                {
                    Altitude = Convert.ToDouble(locationValues["altitude"]),
                    Latitude = Convert.ToDouble(locationValues["latitude"]),
                    Longitude = Convert.ToDouble(locationValues["longitude"])
                };
            }
            set
            {
                var location = value ?? new Location();
                Execute(AppiumDriverCommand.SetLocation, location.ToDictionary());
            }
        }

        #endregion MJsonMethod Members

        #region Context

        public virtual string Context
        {
            get
            {
                var commandResponse = ((IExecuteMethod)this).Execute(AppiumDriverCommand.GetContext);
                return commandResponse.Value as string;
            }
            set
            {
                var parameters = AppiumCommandExecutionHelper.PrepareArgument("name", value);
                Execute(AppiumDriverCommand.SetContext, parameters);
            }
        }

        public virtual ReadOnlyCollection<string> Contexts
        {
            get
            {
                var commandResponse = ((IExecuteMethod)this).Execute(AppiumDriverCommand.Contexts);
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
                var commandResponse = ((IExecuteMethod)this).Execute(AppiumDriverCommand.GetOrientation);
                return (commandResponse.Value as string).ConvertToScreenOrientation();
            }
            set
            {
                var parameters =
                    AppiumCommandExecutionHelper.PrepareArgument("orientation", value.JSONWireProtocolString());
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
            var commandResponse = ((IExecuteMethod)this).Execute(AppiumDriverCommand.GetAvailableEngines);
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
            ((IExecuteMethod)this).Execute(AppiumDriverCommand.GetActiveEngine).Value as string;

        /// <summary>
        /// Is the IME active on the device (NOTE: on Android, this is always true)
        /// </summary>
        /// <returns>true if IME is active, false otherwise</returns>
        public bool IsIMEActive() =>
            (bool)(((IExecuteMethod)this).Execute(AppiumDriverCommand.IsIMEActive).Value);

        /// <summary>
        /// Activate the given IME on Device
        /// </summary>
        /// <param name="imeEngine">IME to activate</param>
        public void ActivateIMEEngine(string imeEngine) =>
            Execute(AppiumDriverCommand.ActivateEngine,
                AppiumCommandExecutionHelper.PrepareArgument("engine", imeEngine));

        /// <summary>
        /// Deactivate the currently Active IME Engine on device
        /// </summary>
        public void DeactiveIMEEngine() =>
            ((IExecuteMethod)this).Execute(AppiumDriverCommand.DeactivateEngine);

        #endregion Input Method (IME)

        #region W3C Actions

        // Replace or hide the original RemoteWebElement.PerformActions base method so that it does not require
        // AppiumDriver to be fully compliant with W3CWireProtocol specification to execute W3C actions command.
        public new void PerformActions(IList<ActionSequence> actionSequenceList)
        {
            if (actionSequenceList == null)
            {
                throw new ArgumentNullException("actionSequenceList", "List of action sequences must not be null");
            }

            List<object> objectList = new List<object>();
            foreach (ActionSequence sequence in actionSequenceList)
            {
                objectList.Add(sequence.ToDictionary());
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["actions"] = objectList;
            this.Execute(DriverCommand.Actions, parameters);
        }

        #endregion W3C Actions

        #region Device Time

        /// <summary>
        /// Gets device date and time for both iOS(Supports only real device) and Android devices
        /// </summary>
        /// <returns>A string which consists of date and time</returns>
        public string DeviceTime => ((IExecuteMethod)this).Execute(AppiumDriverCommand.GetDeviceTime).Value.ToString();

        #endregion Device Time

        #region Session Data

        public IDictionary<string, object> SessionDetails
        {
            get
            {
                var session =
                    (IDictionary<string, object>)((IExecuteMethod)this).Execute(AppiumDriverCommand.GetSession)
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

        public string StartRecordingScreen() =>
            ((IExecuteMethod)this).Execute(AppiumDriverCommand.StartRecordingScreen).Value.ToString();

        public string StartRecordingScreen(IScreenRecordingOptions options)
        {
            var parameters = AppiumCommandExecutionHelper.PrepareArgument("options", options.GetParameters());
            return Execute(AppiumDriverCommand.StartRecordingScreen, parameters).Value.ToString();
        }

        public string StopRecordingScreen() =>
            ((IExecuteMethod)this).Execute(AppiumDriverCommand.StopRecordingScreen).Value.ToString();

        public string StopRecordingScreen(IScreenRecordingOptions options)
        {
            var parameters = AppiumCommandExecutionHelper.PrepareArgument("options", options.GetParameters());
            return Execute(AppiumDriverCommand.StopRecordingScreen, parameters).Value.ToString();
        }

        #endregion Recording Screen

        #region Compare Images

        /// <summary>
        /// Performs images matching by features. Read
        /// https://docs.opencv.org/3.0-beta/doc/py_tutorials/py_feature2d/py_matcher/py_matcher.html
        /// for more details on this topic.
        /// </summary>
        /// <param name="base64Image1">base64-encoded representation of the first image</param>
        /// <param name="base64Image2">base64-encoded representation of the second image</param>
        /// <param name="options">comparison options</param>
        /// <returns>The matching result. The configuration of fields in the result depends on comparison options</returns>
        public FeaturesMatchingResult MatchImageFeatures(string base64Image1, string base64Image2,
            FeaturesMatchingOptions options = null)
        {
            var parameters = new Dictionary<string, object>
            {
                {"mode", ComparisonMode.MatchFeatures},
                {"firstImage", base64Image1},
                {"secondImage", base64Image2}
            };

            if (options != null)
            {
                parameters.Add("options", options.GetParameters());
            }

            var result = Execute(AppiumDriverCommand.CompareImages, parameters);
            return new FeaturesMatchingResult(result.Value as Dictionary<string, object>);
        }

        /// <summary>
        /// Performs images matching by template to find possible occurrence of 
        /// the partial image in the full image with default options. Read
        /// https://docs.opencv.org/2.4/doc/tutorials/imgproc/histograms/template_matching/template_matching.html
        /// for more details on this topic.
        /// </summary>
        /// <param name="fullImage">base64-encoded representation of the full image</param>
        /// <param name="partialImage">base64-encoded representation of the partial image</param>
        /// <param name="options">comparison options</param>
        /// <returns>The matching result. The configuration of fields in the result depends on comparison options.</returns>
        public OccurenceMatchingResult FindImageOccurence(string fullImage, string partialImage,
            OccurenceMatchingOptions options = null)
        {
            var parameters = new Dictionary<string, object>
            {
                {"mode", ComparisonMode.MatchTemplate},
                {"firstImage", fullImage},
                {"secondImage", partialImage}
            };

            if (options != null)
            {
                parameters.Add("options", options.GetParameters());
            }

            var result = Execute(AppiumDriverCommand.CompareImages, parameters);
            return new OccurenceMatchingResult(result.Value as Dictionary<string, object>);
        }

        /// <summary>
        /// Performs images matching to calculate the similarity score between them.
        /// The flow there is similar to the one used in
        /// <see cref="FindImageOccurence(string, string, OccurenceMatchingOptions)"/>
        /// but it is mandatory that both images are of equal size.
        /// </summary>
        /// <param name="base64Image1">base64-encoded representation of the first image</param>
        /// <param name="base64Image2">base64-encoded representation of the second image</param>
        /// <param name="options">comparison options</param>
        /// <returns>Matching result. The configuration of fields in the result depends on comparison options.</returns>
        public SimilarityMatchingResult GetImagesSimilarity(string base64Image1, string base64Image2,
            SimilarityMatchingOptions options = null)
        {
            var parameters = new Dictionary<string, object>()
            {
                {"mode", ComparisonMode.GetSimilarity},
                {"firstImage", base64Image1},
                {"secondImage", base64Image2},
            };

            if (options != null)
            {
                parameters.Add("options", options.GetParameters());
            }

            var result = Execute(AppiumDriverCommand.CompareImages, parameters);
            return new SimilarityMatchingResult(result.Value as Dictionary<string, object>);
        }

        #endregion Compare Images

        #endregion Public Methods

        #region Support methods

        protected WebElementFactory CreateElementFactory() => new AppiumElementFactory(this);

        internal static ICapabilities SetPlatformToCapabilities(DriverOptions dc, string desiredPlatform)
        {
            dc.PlatformName = desiredPlatform;
            return dc.ToCapabilities();
        }

        internal static ReadOnlyCollection<T> ConvertToExtendedWebElementCollection<T>(IEnumerable collection)
            where T : IWebElement
        {
            return collection.Cast<T>().ToList().AsReadOnly();
        }

        #endregion
    }
}