using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenQA.Selenium.Appium.MultiTouch
{
    public class MultiTouchAction
    {
        private readonly List<TouchActions> _MultiTouchActionList = new List<TouchActions>();

        private readonly AppiumDriver _Driver;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiTouchActions"/> class.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver"/> object on which the actions built will be performed.</param>
        public MultiTouchAction(AppiumDriver driver)
        {

            if (driver == null)
            {
                throw new ArgumentException("The IWebDriver object must implement or wrap a driver that implements IHasTouchScreen.", "driver");
            }

            this._Driver = driver;
        }

        /// <summary>
        /// Add touch actions to be performed
        /// </summary>
        /// <param name="touchAction"></param>
        public MultiTouchAction Add(TouchActions touchAction)
        {
            if (null == touchAction)
            {
                throw new ArgumentNullException("touchAction");
            }

            _MultiTouchActionList.Add(touchAction);
            return this;
        }


        /// <summary>
        /// Gets the actions parameter dictionary for this multi touch action
        /// </summary>
        /// <returns>empty dictionary if no actions found, else dictionary of actions</returns>
        public Dictionary<string, object> GetParameters()
        {
            var parameters = new Dictionary<string, object>();

            // get the json object for the multi click
            var actions = _MultiTouchActionList.Select(touchActions => touchActions.GetActionsList()).Where(xx => 0 < xx.Count).Cast<object>().ToList();

            if (0 < actions.Count)
            {
                parameters.Add("actions", actions);
            }

            return parameters;
        }

        /// <summary>
        /// Executes the Multi Touch Action
        /// </summary>
        public void Perform()
        {
            _Driver.PerformMultiTouchAction(this);
        }

    }
}
