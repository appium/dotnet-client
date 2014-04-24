using System;
using OpenQA.Selenium.Interactions;

namespace OpenQA.Selenium.Appium.MultiTouch
{
    /// <summary>
    /// Wait Command 
    /// </summary>
    public class WaitAction : IAction
    {
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="durationInMS">duration in milliseconds</param>
        public WaitAction(int durationInMS = 0)
        {
            Duration = durationInMS;
        }
        #endregion Constructor

        #region Public Properties
        /// <summary>
        /// Duration in ms to wait
        /// </summary>
        public int Duration { get; private set; }
        #endregion Public Properties

        #region Public Methods
        /// <summary>
        /// Throws since we don't have to implement this yet
        /// </summary>
        public void Perform()
        {
            throw new NotImplementedException("Do not need to implement this yet");
        }
        #endregion Public Methods
    }
}
