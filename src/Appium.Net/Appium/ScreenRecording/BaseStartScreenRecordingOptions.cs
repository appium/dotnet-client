using System;
using System.Globalization;

namespace OpenQA.Selenium.Appium.ScreenRecording
{
    public abstract class BaseStartScreenRecordingOptions<T> : BaseScreenRecordingOptions<T>
        where T : BaseStartScreenRecordingOptions<T>
    {
        /// <summary>
        /// The maximum recording time.
        /// </summary>
        /// <param name="timeLimit">The actual time limit of the recorded video.</param>
        /// <returns>self instance for chaining.</returns>
        public T WithTimeLimit(TimeSpan timeLimit)
        {
            Parameters["timeLimit"] = (int)timeLimit.TotalSeconds;;
            return (T) this;
        }

        /// <summary>
        /// Whether to ignore the result of previous capture and start a new recording
        /// immediately. By default the endpoint will try to catch and return the result of
        /// the previous capture if it's still available.
        /// </summary>
        /// <returns>self instance for chaining.</returns>
        public T EnableForcedRestart()
        {
            Parameters["forceRestart"] = true;
            return (T) this;
        }

        /// <summary>
        /// The remotePath upload option is the path to the remote location,
        /// where the resulting video should be uploaded.
        /// The following protocols are supported: http/https (multipart), ftp.
        ///
        /// Missing value (the default setting) means the content of the resulting
        /// file should be encoded as Base64 and passed as the endpoint response value, but
        /// an exception will be thrown if the generated media file is too big to
        /// fit into the available process memory.
        /// This option only has an effect if there is a screen recording session in progress
        /// and forced restart is not enabled (the default setting).
        /// </summary>
        /// <param name="uploadOptions">Upload options</param>
        /// <returns>self instance for chaining.</returns>
        public new T WithUploadOptions(ScreenRecordingUploadOptions uploadOptions)
        {
            return base.WithUploadOptions(uploadOptions);
        }
    }
}
