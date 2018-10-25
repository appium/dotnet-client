namespace OpenQA.Selenium.Appium.ScreenRecording
{
    public abstract class BaseStopScreenRecordingOptions<T> : BaseScreenRecordingOptions<T> where T : BaseStopScreenRecordingOptions<T>
    {
        /// <summary>
        /// The remotePath upload option is the path to the remote location,
        /// where the resulting video should be uploaded.
        /// The following protocols are supported: http/https (multipart), ftp.
        /// Missing value (the default setting) means the content of resulting
        /// file should be encoded as Base64 and passed as the endpoint response value, but
        /// an exception will be thrown if the generated media file is too big to
        /// fit into the available process memory.
        /// </summary>
        /// <param name="uploadOptions">Upload options</param>
        /// <returns>self instance for chaining.</returns>
        public new T WithUploadOptions(ScreenRecordingUploadOptions uploadOptions)
        {
            return base.WithUploadOptions(uploadOptions);
        }
    }
}
