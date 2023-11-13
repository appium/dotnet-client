using Appium.Net.Integration.Tests.helpers;
using System.IO;

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
                    InitAppiumPackageIndexPath();
                }
                return _pathToAppiumPackageIndex;
            }
        }

        /// <summary>
        /// Initializes the Appium package index path by combining the components "appium" and "index.js" with the npm prefix path.
        /// </summary>
        /// <remarks>
        /// This method sets the _pathToAppiumPackageIndex variable by combining the specified components with the npm prefix path.
        /// </remarks>
        private void InitAppiumPackageIndexPath()
        {
            string[] appiumJsPathComponents = { "appium", "index.js" }; 
            string npmPath = Npm.GetNpmPrefixPath();

            _pathToAppiumPackageIndex = Path.Combine(npmPath, Path.Combine(appiumJsPathComponents));
        }
    }
}
