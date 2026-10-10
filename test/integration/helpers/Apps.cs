using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace Appium.Net.Integration.Tests.helpers
{
    public class Apps : IDisposable
    {
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

        // Apps checked into the repo and copied to the test output folder (see the .csproj),
        // used instead of downloading them when the Appium server runs locally.
        private static readonly Dictionary<string, string> _bundledApps = new Dictionary<string, string>
        {
            {iosTestApp, Path.Combine("apps", "TestApp.app.zip")},
        };

        private const int DownloadAttempts = 3;

        private static HttpClient _httpClient = new HttpClient();

        public static string Get(string appKey)
        {
            lock (_testApps)
            {
                if (!_testApps.TryGetValue(appKey, out var app))
                {
                    app = Resolve(appKey);
                    _testApps[appKey] = app;
                }
                return app;
            }
        }

        private static string Resolve(string appKey)
        {
            var source = _appSources[appKey];

            // A remote server cannot read local paths, so it downloads the app itself.
            if (Env.ServerIsRemote())
            {
                return source;
            }

            if (_bundledApps.TryGetValue(appKey, out var bundled))
            {
                var bundledPath = Path.Combine(AppContext.BaseDirectory, bundled);
                if (File.Exists(bundledPath))
                {
                    return bundledPath;
                }
            }

            var destination = Path.Combine(Path.GetTempPath(), GetFileNameFromUrl(source));
            DownloadIfMissing(source, destination);
            return new FileInfo(destination).FullName;
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

        private static string GetFileNameFromUrl(string url)
        {
            var uri = new Uri(url);
            return Path.GetFileName(uri.AbsolutePath);
        }

        // httpClient and sleep can be replaced in tests; by default the shared client and Thread.Sleep are used.
        internal static void DownloadIfMissing(string url, string destination, HttpClient httpClient = null, Action<TimeSpan> sleep = null)
        {
            if (File.Exists(destination))
            {
                return;
            }

            httpClient = httpClient ?? _httpClient;
            sleep = sleep ?? Thread.Sleep;

            for (var attempt = 1; ; attempt++)
            {
                using (var cts = new CancellationTokenSource(TimeSpan.FromHours(1)))
                {
                    try
                    {
                        // GetByteArrayAsync with CancelationToken doesn't work with .NET 4.8.
                        var response = httpClient.GetAsync(url, HttpCompletionOption.ResponseContentRead, cts.Token).GetAwaiter().GetResult();
                        response.EnsureSuccessStatusCode();
                        var data = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                        File.WriteAllBytes(destination, data);
                        return;
                    }
                    catch (Exception ex)
                    {
                        if (File.Exists(destination))
                        {
                            File.Delete(destination);
                        }
                        if (attempt >= DownloadAttempts)
                        {
                            throw new Exception($"Failed to download {url} after {attempt} attempts", ex);
                        }
                    }
                }
                // Back off on transient failures such as a 503 from GitHub: 5s, then 10s.
                sleep(TimeSpan.FromSeconds(5 * attempt));
            }
        }
    }
}