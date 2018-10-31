using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.ScreenRecording
{
    public class ScreenRecordingUploadOptions
    {
        protected Dictionary<string, object> Parameters;

        /// <summary>
        /// The path to the remote location, where the resulting video should be uploaded.
        /// </summary>
        /// <param name="remotePath">The path to a writable remote location.</param>
        /// <returns>self instance for chaining.</returns>
        public ScreenRecordingUploadOptions WithRemotePath(string remotePath)
        {
            Parameters["remotePath"] = remotePath;
            return this;
        }

        /// <summary>
        /// Sets the credentials for remote ftp/http authentication (if needed).
        /// This option only has an effect if remotePath is provided.
        /// </summary>
        /// <param name="user">The name of the user for the remote authentication.</param>
        /// <param name="pass">The password for the remote authentication.</param>
        /// <returns>self instance for chaining.</returns>
        public ScreenRecordingUploadOptions WithAuthCredentials(string user, string pass)
        {
            Parameters["user"] = user;
            Parameters["pass"] = pass;
            return this;
        }

        public enum RequestMethod
        {
            POST, PUT
        }

        /// <summary>
        /// Sets the method name for http(s) upload. PUT is used by default.
        /// This option only has an effect if remotePath is provided.
        /// </summary>
        /// <param name="method">method The HTTP method name.</param>
        /// <returns>self instance for chaining.</returns>
        public ScreenRecordingUploadOptions WithHttpMethod(RequestMethod method)
        {
            Parameters["method"] = method.ToString();
            return this;
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
