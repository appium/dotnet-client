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
                    GetAppiumPackageIndexPath();
                }
                return _pathToAppiumPackageIndex;
            }
        }

        private void GetAppiumPackageIndexPath()
        {
            byte[] bytes = Resources.PathToNode;
            string appiumJsPath = Encoding.UTF8.GetString(bytes);

            string npmPath = Npm.GetNpmPrefixPath();

            string tempPath = Path.Combine(npmPath, appiumJsPath);
            _pathToAppiumPackageIndex = tempPath.Replace("\\", "/");
        }
    }
}
