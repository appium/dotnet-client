using Appium.Net.Integration.Tests.helpers;
using Appium.Net.Integration.Tests.Properties;
using System;
using System.IO;
using System.Text;

namespace Appium.Net.Integration.Tests.Helpers
{
    internal class Paths
    {
        private string _pathToAppiumPackageIndex;

        public string PathToAppiumPackageIndex
        {
            get
            {
                if (_pathToAppiumPackageIndex == null)
                {
                    GetAppiumPackageIndexPath();
                }
                return _pathToAppiumPackageIndex;
            }
        }

        /// <summary>
        /// Combines the path components from the appiumJsPath with the npmPath.
        /// </summary>
        /// <remarks>
        /// This function reads a byte array from the PathToPackageIndex resource, converts it to a string,
        /// processes the relative path to remove '\r\n', and combines it with the npm prefix path.
        /// The resulting combined path is assigned to the _pathToAppiumPackageIndex variable.
        /// </remarks>
        private void GetAppiumPackageIndexPath()
        {
            byte[] bytes = Resources.PathToPackageIndex;
            string appiumJsPath = Encoding.UTF8.GetString(bytes);
            string[] appiumJsPathComponents = appiumJsPath.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            string npmPath = Npm.GetNpmPrefixPath();

            _pathToAppiumPackageIndex = Path.Combine(npmPath, Path.Combine(appiumJsPathComponents));
        }
    }
}
