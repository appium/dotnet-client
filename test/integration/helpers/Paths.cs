using Appium.Net.Integration.Tests.helpers;
using Appium.Net.Integration.Tests.Properties;
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
                    GetAppiumJsPath();
                }
                return _pathToAppiumPackageIndex;
            }
        }

        private void GetAppiumJsPath()
        {
            byte[] bytes;
            string appiumJsPath;
            string npmPath;
           
            bytes = Resources.PathToNode;
            appiumJsPath = Encoding.UTF8.GetString(bytes); 

            npmPath = Npm.GetNpmPrefixPath();

            string tempPath = Path.Combine(npmPath, appiumJsPath);
            _pathToAppiumPackageIndex = tempPath.Replace("\\", "/");
        }
    }
}
