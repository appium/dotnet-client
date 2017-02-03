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
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

namespace OpenQA.Selenium.Appium
{
    public abstract class AppiumDriver<W> : RemoteWebDriver, IExecuteMethod, ITouchShortcuts, IFindsByFluentSelector<W>, IFindByAccessibilityId<W>,
        IHidesKeyboard, IInteractsWithFiles,
        IInteractsWithApps, IPerformsTouchActions, IRotatable, IContextAware, IGenericSearchContext<W>, IGenericFindsByClassName<W>,
        IGenericFindsById<W>, IGenericFindsByCssSelector<W>, IGenericFindsByLinkText<W>, IGenericFindsByName<W>,
        IGenericFindsByPartialLinkText<W>, IGenericFindsByTagName<W>, IGenericFindsByXPath<W> where W : IWebElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the AppiumDriver class
        /// </summary>
        /// <param name="commandExecutor">An <see cref="ICommandExecutor"/> object which executes commands for the driver.</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        public AppiumDriver(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities)
            : base(commandExecutor, desiredCapabilities)
        {
            AppiumCommand.Merge(commandExecutor.CommandInfoRepository);
        }

        /// <summary>
        /// Initializes a new instance of the AppiumDriver class using desired capabilities
        /// </summary>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        public AppiumDriver(ICapabilities desiredCapabilities)
            : this(AppiumLocalService.BuildDefaultService(), desiredCapabilities)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AppiumDriver class using desired capabilities and command timeout
        /// </summary>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public AppiumDriver(ICapabilities desiredCapabilities, TimeSpan commandTimeout)
            : this(AppiumLocalService.BuildDefaultService(), desiredCapabilities, commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AppiumDriver class using the AppiumServiceBuilder instance and desired capabilities
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        public AppiumDriver(AppiumServiceBuilder builder, ICapabilities desiredCapabilities)
            : this(builder.Build(), desiredCapabilities)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AppiumDriver class using the AppiumServiceBuilder instance, desired capabilities and command timeout
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public AppiumDriver(AppiumServiceBuilder builder, ICapabilities desiredCapabilities, TimeSpan commandTimeout)
            : this(builder.Build(), desiredCapabilities, commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AppiumDriver class using the specified remote address and desired capabilities
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        public AppiumDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
            : this(remoteAddress, desiredCapabilities, RemoteWebDriver.DefaultCommandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AppiumDriver class using the specified Appium local service and desired capabilities
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        public AppiumDriver(AppiumLocalService service, ICapabilities desiredCapabilities)
            : this(service, desiredCapabilities, RemoteWebDriver.DefaultCommandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AppiumDriver class using the specified remote address, desired capabilities, and command timeout.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public AppiumDriver(Uri remoteAddress, ICapabilities desiredCapabilities, TimeSpan commandTimeout)
            : this(new AppiumCommandExecutor(remoteAddress, commandTimeout), desiredCapabilities)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AppiumDriver class using the specified Appium local service, desired capabilities, and command timeout.
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public AppiumDriver(AppiumLocalService service, ICapabilities desiredCapabilities, TimeSpan commandTimeout)
            : this(new AppiumCommandExecutor(service, commandTimeout), desiredCapabilities)
        {
        }
        #endregion Constructors

        #region Generic FindMethods
        /// <summary>
        /// Finds the first element in the page that matches the OpenQA.Selenium.By object 
        /// </summary>
        /// <param name="by">Mechanism to find element</param>
        /// <returns>first element found</returns>
        public new W FindElement(By by) =>
            (W)base.FindElement(by);


        /// <summary>
        /// Find the elements on the page by using the <see cref="T:OpenQA.Selenium.By"/> object and returns a ReadonlyCollection of the Elements on the page 
        /// </summary>
        /// <param name="by">Mechanism to find element</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<W> FindElements(By by) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElements(by));

        public new W FindElement(string by, string value) => (W)base.FindElement(by, value);

        public new ReadOnlyCollection<W> FindElements(string selector, string value) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElements(selector, value));

        /// <summary>
        /// Finds the first element in the page that matches the class name supplied
        /// </summary>
        /// <param name="className">CSS class name on the element</param>
        /// <returns>first element found</returns>
        public new W FindElementByClassName(string className) =>
            (W)base.FindElementByClassName(className);

        /// <summary>
        /// Finds a list of elements that match the class name supplied
        /// </summary>
        /// <param name="className">CSS class name on the element</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<W> FindElementsByClassName(string className) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElementsByClassName(className));

        /// <summary>
        /// Finds the first element in the page that matches the ID supplied
        /// </summary>
        /// <param name="id">ID of the element</param>
        /// <returns>First element found</returns>
        public new W FindElementById(string id) =>
            (W)base.FindElementById(id);

        /// <summary>
        /// Finds a list of elements that match the ID supplied
        /// </summary>
        /// <param name="id">ID of the element</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<W> FindElementsById(string id) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElementsById(id));

        /// <summary>
        /// Finds the first element matching the specified CSS selector
        /// </summary>
        /// <param name="cssSelector">The CSS selector to match</param>
        /// <returns>First element found</returns>
        public new W FindElementByCssSelector(string cssSelector) =>
            (W)base.FindElementByCssSelector(cssSelector);

        /// <summary>
        /// Finds a list of elements that match the CSS selector
        /// </summary>
        /// <param name="cssSelector">The CSS selector to match</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<W> FindElementsByCssSelector(string cssSelector) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElementsByCssSelector(cssSelector));

        /// <summary>
        /// Finds the first of elements that match the link text supplied
        /// </summary>
        /// <param name="linkText">Link text of element</param>
        /// <returns>First element found</returns>
        public new W FindElementByLinkText(string linkText) =>
             (W)base.FindElementByLinkText(linkText);

        /// <summary>
        /// Finds a list of elements that match the link text supplied
        /// </summary>
        /// <param name="linkText">Link text of element</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<W> FindElementsByLinkText(string linkText) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElementsByLinkText(linkText));

        /// <summary>
        /// Finds the first of elements that match the name supplied
        /// </summary>
        /// <param name="name">Name of the element on the page</param>
        /// <returns>First element found</returns>
        public new W FindElementByName(string name) =>
            (W)base.FindElementByName(name);

        /// <summary>
        /// Finds a list of elements that match the name supplied
        /// </summary>
        /// <param name="name">Name of the element on the page</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<W> FindElementsByName(string name) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElementsByName(name));

        /// <summary>
        /// Finds the first of elements that match the part of the link text supplied
        /// </summary>
        /// <param name="partialLinkText">Part of the link text</param>
        /// <returns>First element found</returns>
        public new W FindElementByPartialLinkText(string partialLinkText) =>
            (W)base.FindElementByPartialLinkText(partialLinkText);

        /// <summary>
        /// Finds a list of elements that match the part of the link text supplied
        /// </summary>
        /// <param name="partialLinkText">Part of the link text</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<W> FindElementsByPartialLinkText(string partialLinkText) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElementsByPartialLinkText(partialLinkText));

        /// <summary>
        /// Finds the first of elements that match the DOM Tag supplied
        /// </summary>
        /// <param name="tagName">DOM tag name of the element being searched</param>
        /// <returns>First element found</returns>
        public new W FindElementByTagName(string tagName) =>
            (W)base.FindElementByTagName(tagName);

        /// <summary>
        /// Finds a list of elements that match the DOM Tag supplied
        /// </summary>
        /// <param name="tagName">DOM tag name of the element being searched</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<W> FindElementsByTagName(string tagName) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElementsByTagName(tagName));

        /// <summary>
        /// Finds the first of elements that match the XPath supplied
        /// </summary>
        /// <param name="xpath">xpath to the element</param>
        /// <returns>First element found</returns>
        public new W FindElementByXPath(string xpath) =>
            (W)base.FindElementByXPath(xpath);

        /// <summary>
        /// Finds a list of elements that match the XPath supplied
        /// </summary>
        /// <param name="xpath">xpath to the element</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public new ReadOnlyCollection<W> FindElementsByXPath(string xpath) =>
            ConvertToExtendedWebElementCollection<W>(base.FindElementsByXPath(xpath));

        #region IFindByAccessibilityId Members

        /// <summary>
        /// Finds the first element that match the accessibility id supplied
        /// </summary>
        /// <param name="selector">accessibility id</param>
        /// <returns>First element found</returns>
        public W FindElementByAccessibilityId(string selector) => FindElement(MobileSelector.Accessibility, selector);

        /// <summary>
        /// Finds a list of elements that match the accessibillity id supplied
        /// </summary>
        /// <param name="selector">accessibility id</param>
        /// <returns>ReadOnlyCollection of elements found</returns>
        public ReadOnlyCollection<W> FindElementsByAccessibilityId(string selector) => FindElements(MobileSelector.Accessibility, selector);

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

        /// <summary>
        /// Installs an App.
        /// </summary>
        /// <param name="appPath">a string containing the file path or url of the app.</param>
        public void InstallApp(string appPath) =>
            Execute(AppiumDriverCommand.InstallApp, new Dictionary<string, object>() { ["appPath"] = appPath });

        /// <summary>
        /// Removes an App.
        /// </summary>
        /// <param name="appPath">a string containing the id of the app.</param>
        public void RemoveApp(string appId) =>
            Execute(AppiumDriverCommand.RemoveApp, new Dictionary<string, object>() { ["appId"] = appId });

        /// <summary>
        /// Checks If an App Is Installed.
        /// </summary>
        /// <param name="appPath">a string containing the bundle id.</param>
        /// <return>a bol indicating if the app is installed.</return>
        public bool IsAppInstalled(string bundleId) =>
            Convert.ToBoolean(Execute(AppiumDriverCommand.IsAppInstalled,
                new Dictionary<string, object>() { ["bundleId"] = bundleId }).Value.ToString());

        /// <summary>
        /// Pulls a File.
        /// </summary>
        /// <param name="pathOnDevice">path on device to pull</param>
        public byte[] PullFile(string pathOnDevice) =>
            Convert.FromBase64String(Execute(AppiumDriverCommand.PullFile,
                new Dictionary<string, object>() { ["path"] = pathOnDevice }).Value.ToString());

        /// <summary>
        /// Pulls a Folder
        /// </summary>
        /// <param name="remotePath">remote path to the folder to return</param>
        /// <returns>a base64 encoded string representing a zip file of the contents of the folder</returns>
        public byte[] PullFolder(string remotePath) =>
             Convert.FromBase64String(Execute(AppiumDriverCommand.PullFolder,
                new Dictionary<string, object>() { ["path"] = remotePath }).Value.ToString());

        /// <summary>
        /// Launches the current app.
        /// </summary>
        public void LaunchApp() => ((IExecuteMethod)this).Execute(AppiumDriverCommand.LaunchApp);

        /// <summary>
        /// Closes the current app.
        /// </summary>
        public void CloseApp() => ((IExecuteMethod)this).Execute(AppiumDriverCommand.CloseApp);

        /// <summary>
        /// Resets the current app.
        /// </summary>
        public void ResetApp() => ((IExecuteMethod)this).Execute(AppiumDriverCommand.ResetApp);

        /// <summary>
        /// Backgrounds the current app for the given number of seconds.
        /// </summary>
        /// <param name="seconds">a string containing the number of seconds.</param>
        public void BackgroundApp(int seconds) =>
            Execute(AppiumDriverCommand.BackgroundApp,
                new Dictionary<string, object>() { ["seconds"] = seconds });

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
            if (language != null) { parameters.Add("language", language); }
            if (stringFile != null) { parameters.Add("stringFile", stringFile); }
            if (parameters.Count == 0) { parameters = null; }
            return (Dictionary<string, object>)Execute(AppiumDriverCommand.GetAppStrings, parameters).Value;
        }

        public void HideKeyboard() => AppiumCommandExecutionHelper.HideKeyboard(this, null, null);

        /// <sumary>
        /// GPS Location
        /// </summary>
        public Location Location
        {
            get
            {
                var commandResponse = ((IExecuteMethod)this).Execute(AppiumDriverCommand.GetLocation);
                return JsonConvert.DeserializeObject<Location>((string)commandResponse.Value);
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
                var commandResponse = ((IExecuteMethod)this).Execute(AppiumDriverCommand.GetContext);
                return commandResponse.Value as string;
            }
            set
            {
                var parameters = new Dictionary<string, object>();
                parameters.Add("name", value);
                Execute(AppiumDriverCommand.SetContext, parameters);
            }
        }

        public ReadOnlyCollection<string> Contexts
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
        /// <summary>
        /// Sets/Gets the Orientation
        /// </summary>
        public ScreenOrientation Orientation
        {
            get
            {
                var commandResponse = ((IExecuteMethod)this).Execute(AppiumDriverCommand.GetOrientation);
                return (commandResponse.Value as string).ConvertToScreenOrientation();
            }
            set
            {
                var parameters = new Dictionary<string, object>();
                parameters.Add("orientation", value.JSONWireProtocolString());
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
            Execute(AppiumDriverCommand.ActivateEngine, new Dictionary<string, object>() { ["engine"] = imeEngine });

        /// <summary>
        /// Deactivate the currently Active IME Engine on device
        /// </summary>
        public void DeactiveIMEEngine() =>
            ((IExecuteMethod)this).Execute(AppiumDriverCommand.DeactivateEngine);

        #endregion Input Method (IME)

        #region Multi Actions

        /// <summary>
        /// Perform the multi action
        /// </summary>
        /// <param name="multiAction">multi action to perform</param>
        public void PerformMultiAction(IMultiAction multiAction)
        {
            if (multiAction != null)
            {
                var parameters = multiAction.GetParameters();
                Execute(AppiumDriverCommand.PerformMultiAction, parameters);
            }
        }

        /// <summary>
        /// Perform the touch action
        /// </summary>
        /// <param name="touchAction">touch action to perform</param>
        public void PerformTouchAction(ITouchAction touchAction)
        {
            if (touchAction != null)
            {
                var parameters = new Dictionary<string, object>();
                parameters.Add("actions", touchAction.GetParameters());
                Execute(AppiumDriverCommand.PerformTouchAction, parameters);
            }
        }

        #endregion Multi Actions

        #region tap, swipe, pinch, zoom

        /// <summary>
        /// Creates a tap on an element for a given time
        /// </summary>
        private ITouchAction CreateTap(IWebElement element, int duration) =>
            new TouchAction(this).Press(element).Wait(duration).Release();

        /// <summary>
        /// Creates a tap on x-y-coordinates for a given time
        /// </summary>
        private ITouchAction CreateTap(int x, int y, int duration)
            => new TouchAction(this).Press(x, y).Wait(duration).Release();

        /// <summary>
        /// Convenience method for tapping the center of an element on the screen
        /// </summary>
        /// <param name="fingers">number of fingers/appendages to tap with</param>
        /// <param name="element">element to tap</param>
        /// <param name="duration">how long between pressing down, and lifting fingers/appendages</param>
        public void Tap(int fingers, IWebElement element, int duration)
        {
            MultiAction multiTouch = new MultiAction(this);

            for (int i = 0; i < fingers; i++)
            {
                multiTouch.Add(CreateTap(element, duration));
            }

            multiTouch.Perform();
        }

        /// <summary>
        /// Convenience method for tapping a position on the screen
        /// </summary>
        /// <param name="fingers">number of fingers/appendages to tap with</param>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <param name="duration">how long between pressing down, and lifting fingers/appendages</param>
        public void Tap(int fingers, int x, int y, int duration)
        {
            MultiAction multiTouch = new MultiAction(this);

            for (int i = 0; i < fingers; i++)
            {
                multiTouch.Add(CreateTap(x, y, duration));
            }

            multiTouch.Perform();
        }

        protected void DoSwipe(int startx, int starty, int endx, int endy, int duration)
        {
            TouchAction touchAction = new TouchAction(this);

            // appium converts Press-wait-MoveTo-Release to a swipe action
            touchAction.Press(startx, starty).Wait(duration)
                    .MoveTo(endx, endy).Release();

            touchAction.Perform();
        }

        /// <summary>
        /// Convenience method for swiping across the screen
        /// </summary>
        /// <param name="startx">starting x coordinate</param>
        /// <param name="starty">starting y coordinate</param>
        /// <param name="endx">ending x coordinate</param>
        /// <param name="endy">ending y coordinate</param>
        /// <param name="duration">amount of time in milliseconds for the entire swipe action to take</param>
        public abstract void Swipe(int startx, int starty, int endx, int endy, int duration);

        /// <summary>
        /// Convenience method for pinching an element on the screen.
        /// "pinching" refers to the action of two appendages Pressing the screen and sliding towards each other.
        /// NOTE:
        /// driver convenience method places the initial touches around the element, if driver would happen to place one of them
        /// off the screen, appium with return an outOfBounds error. In driver case, revert to using the MultiAction api
        /// instead of driver method.
        /// </summary>
        /// <param name="el">The element to pinch</param>
        public void Pinch(IWebElement el)
        {
            MultiAction multiTouch = new MultiAction(this);

            Size dimensions = el.Size;
            Point upperLeft = el.Location;
            Point center = new Point(upperLeft.X + dimensions.Width / 2, upperLeft.Y + dimensions.Height / 2);
            int yOffset = center.Y - upperLeft.Y;

            ITouchAction action0 = new TouchAction(this).Press(el, center.X, center.Y - yOffset).MoveTo(el).Release();
            ITouchAction action1 = new TouchAction(this).Press(el, center.X, center.Y + yOffset).MoveTo(el).Release();

            multiTouch.Add(action0).Add(action1);

            multiTouch.Perform();
        }

        /// <summary>
        /// Convenience method for pinching an element on the screen.
        /// "pinching" refers to the action of two appendages Pressing the screen and sliding towards each other.
        /// NOTE:
        /// driver convenience method places the initial touches around the element at a distance, if driver would happen to place
        /// one of them off the screen, appium will return an outOfBounds error. In driver case, revert to using the
        /// MultiAction api instead of driver method.
        /// </summary>
        /// <param name="x">x coordinate to terminate the pinch on</param>
        /// <param name="y">y coordinate to terminate the pinch on></param>
        public void Pinch(int x, int y)
        {
            MultiAction multiTouch = new MultiAction(this);

            int scrHeight = Manage().Window.Size.Height;
            int yOffset = 100;

            if (y - 100 < 0)
            {
                yOffset = y;
            }
            else if (y + 100 > scrHeight)
            {
                yOffset = scrHeight - y;
            }

            ITouchAction action0 = new TouchAction(this).Press(x, y - yOffset).MoveTo(0, yOffset).Release();
            ITouchAction action1 = new TouchAction(this).Press(x, y + yOffset).MoveTo(0, -yOffset).Release();

            multiTouch.Add(action0).Add(action1);

            multiTouch.Perform();
        }

        /// <summary>
        /// Convenience method for "zooming in" on an element on the screen.
        /// "zooming in" refers to the action of two appendages Pressing the screen and sliding away from each other.
        /// NOTE:
        /// driver convenience method slides touches away from the element, if driver would happen to place one of them
        /// off the screen, appium will return an outOfBounds error. In driver case, revert to using the MultiAction api
        /// instead of driver method.
        /// <param name="x">x coordinate to terminate the zoom on</param>
        /// <param name="y">y coordinate to terminate the zoom on></param>
        /// </summary>
        public void Zoom(int x, int y)
        {
            MultiAction multiTouch = new MultiAction(this);

            int scrHeight = Manage().Window.Size.Height;
            int yOffset = 100;

            if (y - 100 < 0)
            {
                yOffset = y;
            }
            else if (y + 100 > scrHeight)
            {
                yOffset = scrHeight - y;
            }

            ITouchAction action0 = new TouchAction(this).Press(x, y).MoveTo(0, -yOffset).Release();
            ITouchAction action1 = new TouchAction(this).Press(x, y).MoveTo(0, yOffset).Release();

            multiTouch.Add(action0).Add(action1);

            multiTouch.Perform();
        }

        /// <summary>
        /// Convenience method for "zooming in" on an element on the screen.
        /// "zooming in" refers to the action of two appendages Pressing the screen and sliding away from each other.
        /// NOTE:
        /// driver convenience method slides touches away from the element, if driver would happen to place one of them
        /// off the screen, appium will return an outOfBounds error. In driver case, revert to using the MultiAction api
        /// instead of driver method.
        /// <param name="el">The element to pinch</param>
        /// </summary>
        public void Zoom(IWebElement el)
        {
            MultiAction multiTouch = new MultiAction(this);

            Size dimensions = el.Size;
            Point upperLeft = el.Location;
            Point center = new Point(upperLeft.X + dimensions.Width / 2, upperLeft.Y + dimensions.Height / 2);
            int yOffset = center.Y - upperLeft.Y;

            ITouchAction action0 = new TouchAction(this).Press(el).MoveTo(el, 0, -yOffset).Release();
            ITouchAction action1 = new TouchAction(this).Press(el).MoveTo(el, 0, yOffset).Release();

            multiTouch.Add(action0).Add(action1);

            multiTouch.Perform();
        }

        #endregion

        #region Device Time
        /// <summary>
        /// Gets device date and time for both iOS(Supports only real device) and Android devices
        /// </summary>
        /// <returns>A string which consists of date and time</returns>
        public string DeviceTime
        {
            get
            {
                return ((IExecuteMethod)this).Execute(AppiumDriverCommand.GetDeviceTime).Value.ToString();
            }
        }

        #endregion Device Time

        #region Session Data
        /// <summary>
        /// This property returns a dictionary of the current session data
        /// </summary>
        public Dictionary<string, object> SessionDetails
        {
            get
            {
                return (Dictionary<string, object>)((IExecuteMethod)this).Execute(AppiumDriverCommand.GetSession).Value;
            }
        }
        #endregion Session Data

        #endregion Public Methods

        #region Support methods

        internal static DesiredCapabilities SetPlatformToCapabilities(DesiredCapabilities dc, string desiredPlatform)
        {
            dc.SetCapability(MobileCapabilityType.PlatformName, desiredPlatform);
            return dc;
        }

        internal static ReadOnlyCollection<T> ConvertToExtendedWebElementCollection<T>(IList list) where T : IWebElement
        {
            List<T> result = new List<T>();
            foreach (var element in list)
            {
                result.Add((T)element);
            }
            return result.AsReadOnly();
        }
        #endregion
    }
}