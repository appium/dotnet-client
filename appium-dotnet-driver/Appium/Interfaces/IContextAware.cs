using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

/// <summary>
/// This interface is temporary. Let it be here till 
///https://github.com/SeleniumHQ/selenium/pull/301
///is merged and new Webdriver for .Net is published.
///
namespace OpenQA.Selenium.Appium.Interfaces
{
    /// <summary>
    /// Some implementations of WebDriver, notably those that support native testing, need the ability
    /// to switch between the native and web-based contexts. This can be achieved by using this
    /// interface.
    /// </summary>
    public interface IContextAware
    {

        /// <summary>
        /// Switches the focus of future commands for this driver to the context with the given name
        /// AND
        /// returns an opaque handle to this context that uniquely identifies it within this driver
        /// instance.
        /// </summary>
        string Context { get; set; }

        /// <summary>
        /// Return a list of context handles which can be used to iterate over all contexts of this
        /// WebDriver instance
        /// </summary>       
        ReadOnlyCollection<string> Contexts { get;}
    }
}
