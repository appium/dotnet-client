using OpenQA.Selenium.Appium.Appium.Android.Interfaces;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Appium.Android
{
    class AndroidDriver : AppiumDriver, IFindByAndroidUIAutomator, IStartsActivity, IHasNetworkConnection, IAndroidDeviceActionShortcuts, IPushesFiles
    {
        /// <summary>
        /// Initializes a new instance of the RemoteWebDriver class
        /// </summary>
        /// <param name="commandExecutor">An <see cref="ICommandExecutor"/> object which executes commands for the driver.</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities of the browser.</param>
        public AndroidDriver(ICommandExecutor commandExecutor, ICapabilities desiredCapabilities)
            : base(commandExecutor, desiredCapabilities)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the AndroidDriver class. This constructor defaults proxy to http://127.0.0.1:4723/wd/hub
        /// </summary>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities of the browser.</param>
        public AndroidDriver(ICapabilities desiredCapabilities)
            : base(desiredCapabilities)
        {
        }

        /// <summary>
        /// Initializes a new instance of the AppiumDriver class
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities of the browser.</param>
        public AndroidDriver(Uri remoteAddress, ICapabilities desiredCapabilities)
            : base(remoteAddress, desiredCapabilities)
        {
        }

        #region IFindByAndroidUIAutomator Members
        /// <summary>
        /// Finds the first element in the page that matches the Android UIAutomator selector supplied
        /// </summary>
        /// <param name="selector">Selector for the element.</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        /// <example>
        /// <code>
        /// IWebDriver driver = new RemoteWebDriver(DesiredCapabilities.Firefox());
        /// IWebElement elem = driver.FindElementByAndroidUIAutomator('elements()'))
        /// </code>
        /// </example>
        public IWebElement FindElementByAndroidUIAutomator(string selector)
        {
            return this.FindElement("-android uiautomator", selector);
        }

        /// <summary>
        /// Finds a list of elements that match the Android UIAutomator selector supplied
        /// </summary>
        /// <param name="selector">Selector for the elements.</param>
        /// <returns>ReadOnlyCollection of IWebElement object so that you can interact with those objects</returns>
        /// <example>
        /// <code>
        /// IWebDriver driver = new RemoteWebDriver(DesiredCapabilities.Firefox());
        /// ReadOnlyCollection<![CDATA[<IWebElement>]]> elem = driver.FindElementsByAndroidUIAutomator(elements())
        /// </code>
        /// </example>
        public ReadOnlyCollection<IWebElement> FindElementsByAndroidUIAutomator(string selector)
        {
            return this.FindElements("-android uiautomator", selector);
        }
        #endregion IFindByAndroidUIAutomator Members

        /// <summary>
        /// Opens an arbitrary activity during a test. If the activity belongs to
        /// another application, that application is started and the activity is opened.
        ///
        /// This is an Android-only method.
        /// </summary>
        /// <param name="appPackage">The package containing the activity to start.</param>
        /// <param name="appActivity">The activity to start.</param>
        /// <param name="appWaitPackage">Begin automation after this package starts. Can be null or empty.</param>
        /// <param name="appWaitActivity">Begin automation after this activity starts. Can be null or empty.</param>
        /// <example>
        /// driver.StartActivity("com.foo.bar", ".MyActivity");
        /// </example>
        public void StartActivity(string appPackage, string appActivity, string appWaitPackage = "", string appWaitActivity = "")
        {
            Contract.Requires(!String.IsNullOrWhiteSpace(appPackage));
            Contract.Requires(!String.IsNullOrWhiteSpace(appActivity));

            Dictionary<string, object> parameters = new Dictionary<string, object>() { {"appPackage", appPackage},
        																			   {"appActivity", appActivity},
        																			   {"appWaitPackage", appWaitPackage},
        																			   {"appWaitActivity", appWaitActivity} };

            this.Execute(AppiumDriverCommand.StartActivity, parameters);
        }

        #region Connection Type
        /// <summary>
        /// Get the Connection Type
        /// </summary>
        /// <returns>Connection Type of device</returns>
        /// <exception cref="System.InvalidCastException">Thrown when object return was not able to be converted to a ConnectionType Enum</exception>
        public ConnectionType GetConnectionType()
        {
            var commandResponse = this.Execute(AppiumDriverCommand.GetConnectionType, null);
            return commandResponse.Value.ConvertToConnectionType();
        }

        /// <summary>
        /// Set the connection type
        /// </summary>
        /// <param name="connectionType"></param>
        public void SetConnectionType(ConnectionType connectionType)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("type", (int)connectionType);
            this.Execute(AppiumDriverCommand.SetConnectionType, parameters);
        }
        #endregion Connection Type

        /// <summary>
        /// Triggers device key event with metastate for the keypress
        /// </summary>
        /// <param name="connectionType"></param>
        public void KeyEvent(int keyCode, int metastate)
        {
            var parameters = new Dictionary<string, object>();
            parameters.Add("keycode", keyCode);
            parameters.Add("metastate", metastate);
            this.Execute(AppiumDriverCommand.KeyEvent, parameters);
        }

        /// <summary>
        /// Toggles Location Services.
        /// </summary>
        public void ToggleLocationServices()
        {
            this.Execute(AppiumDriverCommand.ToggleLocationServices, null);
        }


        /// <summary>
        /// Check if the device is locked
        /// </summary>
        /// <returns>true if device is locked, false otherwise</returns>
        public bool IsLocked()
        {
            var commandResponse = this.Execute(AppiumDriverCommand.IsLocked, null);
            return (bool)commandResponse.Value;
        }


        /// <summary>
        /// Gets Current Device Activity.
        /// </summary>
        public string GetCurrentActivity()
        {
            var commandResponse = this.Execute(AppiumDriverCommand.GetCurrentActivity, null);
            return commandResponse.Value as string;
        }

        /// <summary>
        /// Get test-coverage data
        /// </summary>
        /// <param name="intent">a string containing the intent.</param>
        /// <param name="path">a string containing the path.</param>
        /// <return>a base64 string containing the data</return> 
        public string EndTestCoverage(string intent, string path)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("intent", intent);
            parameters.Add("path", path);
            var commandResponse = this.Execute(AppiumDriverCommand.EndTestCoverage, parameters);
            return commandResponse.Value as string;
        }

        /// <summary>
        /// Pushes a File.
        /// </summary>
        /// <param name="pathOnDevice">path on device to store file to</param>
        /// <param name="base64Data">base 64 data to store as the file</param>
        public void PushFile(string pathOnDevice, string base64Data)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("path", pathOnDevice);
            parameters.Add("data", base64Data);
            this.Execute(AppiumDriverCommand.PushFile, parameters);
        }


        /// <summary>
        /// Open the notifications 
        /// </summary>
        public void OpenNotifications()
        {
            this.Execute(AppiumDriverCommand.OpenNotifications, null);
        }

        protected override RemoteWebElement CreateElement(string elementId)
        {
            return new AndroidElement(this, elementId);
        }

    }
}
