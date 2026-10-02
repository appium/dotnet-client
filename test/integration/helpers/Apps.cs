using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace Appium.Net.Integration.Tests.helpers
{
    public class Apps : IDisposable
    {
        private const int MaxDownloadAttempts = 4;

        private static readonly object _lock = new object();
        private static readonly Dictionary<string, string> _testApps = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> _testAppsIds = new Dictionary<string, string>
        {
            {androidApiDemos, "io.appium.android.apis"},
            {iosTestApp, "io.appium.TestApp"},
            {iosUICatalogApp, "com.example.apple-samplecode.UICatalog" }
        };

        private static readonly Dictionary<string, string> _appSources = new Dictionary<string, string>
        {
            {iosTestApp, "https://github.com/appium/dotnet-client/blob/main/test/integration/apps/archives/TestApp.app.zip?raw=true"},
            {iosUICatalogApp, "https://github.com/appium/ios-uicatalog/releases/download/v4.0.1/UIKitCatalog-iphonesimulator.zip"},
            {androidApiDemos, "https://github.com/appium/android-apidemos/releases/download/v6.0.2/ApiDemos-debug.apk"},
        };

        // Apps checked into this repository. A local Appium server uses these copies (from the build output)
        // instead of downloading them from github.com, which can fail on CI runners with 503 responses.
        private static readonly Dictionary<string, string> _localAppArchives = new Dictionary<string, string>
        {
            {iosTestApp, Path.Combine("apps", "archives", "TestApp.app.zip")},
        };

        private static HttpClient _httpClient = new HttpClient();

        public static string Get(string appKey)
        {
            lock (_lock)
            {
                if (!_testApps.TryGetValue(appKey, out var app))
                {
                    app = Resolve(appKey);
                    _testApps[appKey] = app;
                }
                return app;
            }
        }

        public static string GetId(string appKey)
        {
            return _testAppsIds[appKey];
        }

        public const string iosTestApp = "iosTestApp";
        public const string iosUICatalogApp = "iosUICatalogApp";
        public const string androidApiDemos = "androidApiDemos";

        public void Dispose()
        {
            _httpClient?.Dispose();
        }

        private static string Resolve(string appKey)
        {
            var url = _appSources[appKey];
            if (Env.ServerIsRemote())
            {
                return url;
            }

            if (_localAppArchives.TryGetValue(appKey, out var relativePath))
            {
                var localPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
                if (File.Exists(localPath))
                {
                    return new FileInfo(localPath).FullName;
                }
            }

            var destination = Path.Combine(Path.GetTempPath(), GetFileNameFromUrl(url));
            DownloadIfMissing(url, destination);
            return new FileInfo(destination).FullName;
        }

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

            for (var attempt = 1; ; attempt++)
            {
                try
                {
                    Download(url, destination);
                    return;
                }
                catch (Exception ex)
                {
                    if (File.Exists(destination))
                    {
                        File.Delete(destination);
                    }
                    if (attempt >= MaxDownloadAttempts)
                    {
                        throw new Exception($"Failed to download {url} after {attempt} attempts", ex);
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(Math.Pow(2, attempt)));
                }
            }
        }

        private static void Download(string url, string destination)
        {
            using (var cts = new CancellationTokenSource(TimeSpan.FromHours(1)))
            {
                // GetByteArrayAsync with CancelationToken doesn't work with .NET 4.8.
                var response = _httpClient.GetAsync(url, HttpCompletionOption.ResponseContentRead, cts.Token).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
                var data = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                File.WriteAllBytes(destination, data);
            }
        }
    }
}