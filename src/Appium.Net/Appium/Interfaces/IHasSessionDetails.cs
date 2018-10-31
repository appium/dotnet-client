using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface IHasSessionDetails
    {
        /// <summary>
        /// a dictionary of values that hold session details.
        /// </summary>
        IDictionary<string, object> SessionDetails { get; }

        /// <summary>
        /// This property returns a certain session detail by it key 
        /// or null if there is no such detail.
        /// </summary>
        /// <param name="detail"> is the detail key-name</param>
        /// <returns>an object of null if there is no such detail</returns>
        object GetSessionDetail(string detail);

        /// <summary>
        /// This property returns a name of the mobile plathorm of the current 
        /// session.
        /// </summary>
        string PlatformName { get; }
        
        /// <summary>
        /// This property returns a name of the automation type of the current 
        /// session.
        /// </summary>
        string AutomationName { get; }
        
        /// <summary>
        /// This property should return <code>true</code> when
        /// user's code is currently works on browser or web view.
        /// <code>false</code> should be returned otherwice.
        /// </summary>
        bool IsBrowser{ get; }
    }
}