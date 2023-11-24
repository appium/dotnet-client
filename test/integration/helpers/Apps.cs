using System;
using System.Collections.Generic;
using System.IO;
using Appium.Net.Integration.Tests.Properties;

namespace Appium.Net.Integration.Tests.helpers
{
    public class Apps
    {
        private static bool _isInited;
        private static Dictionary<string, string> _testApps;
        private static readonly Dictionary<string, string> _testAppsIds = new Dictionary<string, string>
        {
            {androidApiDemos, "io.appium.android.apis"},
            {iosTestApp, "io.appium.TestApp"},
            {iosUICatalogApp, "com.example.apple-samplecode.UICatalog" }
        };

        private static void Init()
        {
            if (!_isInited)
            {
                if (Env.ServerIsRemote())
                {
                    _testApps = new Dictionary<string, string>
                    {
                        {iosTestApp, "https://github.com/appium/dotnet-client/blob/main/test/integration/apps/archives/TestApp.app.zip?raw=true"},
                        {iosWebviewApp, "https://github.com/appium/dotnet-client/blob/main/test/integration/apps/archives/WebViewApp.app.zip?raw=true"},
                        {iosUICatalogApp, "https://github.com/appium/dotnet-client/blob/main/test/integration/apps/archives/UICatalog.app.zip?raw=true"},
                        {androidApiDemos, "https://github.com/appium/dotnet-client/blob/main/test/integration/apps/archives/ApiDemos-debug.zip?raw=true"},
                        {vodqaApp, "https://github.com/appium/dotnet-client/blob/main/test/integration/apps/archives/vodqa.zip?raw=true"}
                    };
                }
                else
                {
                    var tempFolder = Path.GetTempPath();

                    File.WriteAllBytes($"{tempFolder}/ApiDemos-debug.apk", Resources.ApiDemos_debug);
                    File.WriteAllBytes($"{tempFolder}/TestApp.app.zip", Resources.TestApp_app);
                    File.WriteAllBytes($"{tempFolder}/WebViewApp.app.zip", Resources.WebViewApp_app);
                    File.WriteAllBytes($"{tempFolder}/UICatalog.app.zip", Resources.UICatalog_app);
                    File.WriteAllBytes($"{tempFolder}/vodqa.app.zip", Resources.vodqa);



                    _testApps = new Dictionary<string, string>
                    {
                        {iosTestApp, new FileInfo($"{Path.GetTempPath()}/TestApp.app.zip").FullName},
                        {iosWebviewApp, new FileInfo($"{Path.GetTempPath()}//WebViewApp.app.zip").FullName},
                        {iosUICatalogApp, new FileInfo($"{Path.GetTempPath()}/UICatalog.app.zip").FullName},
                        {androidApiDemos, new FileInfo($"{Path.GetTempPath()}/ApiDemos-debug.apk").FullName},
                        {vodqaApp, new FileInfo($"{Path.GetTempPath()}/vodqa.app.zip").FullName}

                    };
                }
                _isInited = true;
            }
        }

        public static string Get(string appKey)
        {
            Init();
            return _testApps[appKey];
        }

        public static string GetId(string appKey)
        {
            return _testAppsIds[appKey];
        }

        public const string iosTestApp = "iosTestApp";
        public const string iosWebviewApp = "iosWebviewApp";
        public const string iosUICatalogApp = "iosUICatalogApp";
        public const string androidApiDemos = "androidApiDemos";
        public const string vodqaApp = "vodqaApp";

    }
}