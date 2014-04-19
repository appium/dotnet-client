using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.MultiTouch
{
    public class RemoteMultiTouchScreen : RemoteTouchScreen
    {
        private readonly AppiumDriver _Driver;

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoteTouchScreen"/> class.
        /// </summary>
        /// <param name="driver">The <see cref="RemoteWebDriver"/> for which the touch screen will be managed.</param>
        public RemoteMultiTouchScreen(AppiumDriver driver)
            : base(driver)
        {
            _Driver = driver;
        }
        #endregion Constructor

        #region Public Methods

        #region Pinch
        /// <summary>
        /// Pinch at the specified region
        /// </summary>
        /// <param name="x">x screen location</param>
        /// <param name="y">y screen location</param>
        public void Pinch(int x, int y)
        {
            _Driver.Pinch(x, y);
        }

        #endregion Pinch

        #region Zoom
        /// <summary>
        /// Zoom at the specified point
        /// </summary>
        /// <param name="x">x screen location</param>
        /// <param name="y">y screen location</param>
        public void Zoom(int x, int y)
        {
            _Driver.Zoom(x,y);
        }
        #endregion Zoom

        #endregion Public Methods
    }
}