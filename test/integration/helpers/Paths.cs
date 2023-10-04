using Appium.Net.Integration.Tests.helpers;
using Appium.Net.Integration.Tests.Properties;
using System.IO;
using System.Text;

namespace Appium.Net.Integration.Tests.Helpers
{
    internal class Paths
    {
        private string _pathToCustomizedAppiumJs;

        public string PathToCustomizedAppiumJs
        {
            get
            {
                if (_pathToCustomizedAppiumJs == null)
                {
                    GetAppiumJsPath();
                }
                return _pathToCustomizedAppiumJs;
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
            _pathToCustomizedAppiumJs = Path.Combine(npmPath, appiumJsPath);

        }
    }
}
