using System;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium
{
    /// <summary>
    /// Appium capabilities
    /// </summary>
    public class AppiumCapabilities : ICapabilities
    {
        /// <summary>
        /// The dictionary of capabilities
        /// </summary>
        private readonly Dictionary<string, object> capabilities = new Dictionary<string, object>();

        /// <summary>
        /// Gets the capability value for the specified name
        /// </summary>
        /// <param name="name">The name of the capability</param>
        /// <returns>The capability value</returns>
        public object this[string name]
        {
            get
            {
                if (!this.capabilities.ContainsKey(name))
                {
                    throw new ArgumentException($"The capability {name} is not present in this set of capabilities");
                }

                return this.capabilities[name];
            }

            set
            {
                this.capabilities[name] = value;
            }
        }

        /// <summary>
        /// Get a capability by name
        /// </summary>
        /// <param name="capability">Name of the capability</param>
        /// <returns>The capability value or null if the capability does not exist</returns>
        public object GetCapability(string capability)
        {
            object capabilityValue = null;

            if (this.capabilities.ContainsKey(capability))
            {
                return this.capabilities[capability];
            }

            return capabilityValue;
        }

        /// <summary>
        /// Does the given capability exist
        /// </summary>
        /// <param name="capability">Name of the capability</param>
        /// <returns>True if it exists</returns>
        public bool HasCapability(string capability)
        {
            return this.capabilities.ContainsKey(capability);
        }

        /// <summary>
        /// Sets a capability
        /// </summary>
        /// <param name="name">The capability name</param>
        /// <param name="value">The value for the capability</param>
        public void SetCapability(string name, object value)
        {
            this.capabilities[name] = value;
        }

        /// <summary>
        /// Get the capabilities back as a dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionary()
        {
            return this.capabilities;
        }
    }
}