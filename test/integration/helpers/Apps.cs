using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

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

        private static readonly Dictionary<string, string> _appSources = new Dictionary<string, string>
        {
            {iosTestApp, "https://github.com/appium/dotnet-client/blob/main/test/integration/apps/archives/TestApp.app.zip?raw=true"},
            {iosWebviewApp, "https://github.com/appium/dotnet-client/blob/main/test/integration/apps/archives/WebViewApp.app.zip?raw=true"},
            {iosUICatalogApp, "https://github.com/appium/ios-uicatalog/releases/download/v4.0.1/UIKitCatalog-iphonesimulator.zip"},
            {androidApiDemos, "https://github.com/appium/android-apidemos/releases/download/v6.0.2/ApiDemos-debug.apk"},
            {vodqaApp, "https://github.com/appium/dotnet-client/blob/main/test/integration/apps/archives/vodqa.zip?raw=true"}
        };

        private static readonly HttpClient _httpClient = new HttpClient();

        private static void Init()
        {
            if (!_isInited)
            {
                if (Env.ServerIsRemote())
                {
                    _testApps = new Dictionary<string, string>(_appSources);
                }
                else
                {
                    var tempFolder = Path.GetTempPath();

                    _testApps = new Dictionary<string, string>();

                    foreach (var app in _appSources)
                    {
                        var destination = Path.Combine(tempFolder, GetFileNameFromUrl(app.Value));
                        DownloadIfMissing(app.Value, destination);
                        _testApps[app.Key] = new FileInfo(destination).FullName;
                    }
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

        private static string GetFileNameFromUrl(string url)
        {
            var uri = new Uri(url);
            return Path.GetFileName(uri.AbsolutePath);
        }

        private static void DownloadIfMissing(string url, string destination)
        {
            if (File.Exists(destination))
            {
                return;
            }

            var data = _httpClient.GetByteArrayAsync(url).GetAwaiter().GetResult();
            File.WriteAllBytes(destination, data);
        }
    }
}