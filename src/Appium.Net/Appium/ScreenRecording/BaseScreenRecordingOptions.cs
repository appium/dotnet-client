using System.Collections.Generic;
using OpenQA.Selenium.Appium.Interfaces;

namespace OpenQA.Selenium.Appium.ScreenRecording
{
    public abstract class BaseScreenRecordingOptions<T> : IScreenRecordingOptions where T : BaseScreenRecordingOptions<T>
    {
        protected Dictionary<string, object> Parameters;

        protected BaseScreenRecordingOptions()
        {
            Parameters = new Dictionary<string, object>();
        }

        /// <summary>
        /// Upload options set for the recorded screen capture.
        /// </summary>
        /// <param name="uploadOptions">Upload options</param>
        /// <returns></returns>
        public T WithUploadOptions(ScreenRecordingUploadOptions uploadOptions)
        {
            foreach (var parameter in uploadOptions.GetParameters())
            {
                Parameters[parameter.Key] = parameter.Value;
            }

            return (T) this;
        }

        /// <summary>
        /// Get all setted parameters
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetParameters()
        {
            return Parameters;
        }
    }
}
