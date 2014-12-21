using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Appium.Interfaces
{
    public interface IInteractsWithFiles
    {
        /// <summary>
        /// Pulls a File.
        /// </summary>
        /// <param name="pathOnDevice">path on device to pull</param>
        byte[] PullFile(string pathOnDevice);

        /// <summary>
        /// Pulls a Folder
        /// </summary>
        /// <param name="remotePath">remote path to the folder to return</param>
        /// <returns>a base64 encoded string representing a zip file of the contents of the folder</returns>
        byte[] PullFolder(string remotePath);
    }
}
