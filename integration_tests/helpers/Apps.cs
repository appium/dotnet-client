using System;
using System.Collections.Generic;
using System.IO;

namespace Appium.Integration.Tests.Helpers
{
    public class Apps
    {
        private static bool isInited;
        private static Dictionary<string, string> Appz;

        private static void Init()
        {
            if (!isInited)
            {
                if (Env.isSauce())
                {
                    Appz = new Dictionary<string, string>
                    {
                        {"iosTestApp", "https://github.com/akinsolb/appium-dotnet-driver/tree/update-test-apps/integration_tests/Test%20Apps.app.zip"},
                        {"intentApp", "https://github.com/akinsolb/appium-dotnet-driver/tree/update-test-apps/integration_tests/Test%20Apps/IntentExample.apk"},
                        {"iosWebviewApp", "https://github.com/akinsolb/appium-dotnet-driver/tree/update-test-apps/integration_tests/Test%20Apps/WebViewApp.app.zip"},
                        {"iosUICatalogApp", "https://github.com/akinsolb/appium-dotnet-driver/tree/update-test-apps/integration_tests/Test%20Apps/UICatalog.app.zip"},
                        {"androidApiDemos", "https://github.com/akinsolb/appium-dotnet-driver/tree/update-test-apps/integration_tests/Test%20Apps/ApiDemos-debug.apk"},
                        {"vodqaApp", "https://github.com/akinsolb/appium-dotnet-driver/tree/update-test-apps/integration_tests/Test%20Apps/vodqa.zip"}
                    };
                }
                else
                {
                    File.WriteAllBytes("ApiDemos-debug.apk", Properties.Resources.ApiDemos_debug);
                    File.WriteAllBytes("selendroid-test-app-0.10.0.apk",
                        Properties.Resources.selendroid_test_app_0_10_0);
                    File.WriteAllBytes("TestApp7.1.app.zip", Properties.Resources.TestApp7_1_app);
                    File.WriteAllBytes("WebViewApp7.1.app.zip", Properties.Resources.WebViewApp7_1_app);
                    File.WriteAllBytes("UICatalog7.1.app.zip", Properties.Resources.UICatalog7_1_app);
                    File.WriteAllBytes("IntentExample.apk", Properties.Resources.IntentExample);

                    Appz = new Dictionary<string, string>
                    {
                        {"iosTestApp", new FileInfo("TestApp7.1.app.zip").FullName},
                        {"iosWebviewApp", new FileInfo("WebViewApp7.1.app.zip").FullName},
                        {"iosUICatalogApp", new FileInfo("UICatalog7.1.app.zip").FullName},
                        {"androidApiDemos", new FileInfo("ApiDemos-debug.apk").FullName},
                        {"selendroidTestApp", new FileInfo("selendroid-test-app-0.10.0.apk").FullName},
                        {"intentApp", new FileInfo("IntentExample.apk").FullName}
                    };
                }
                isInited = true;
            }
        }

        public static string get(string appKey)
        {
            Init();
            return Appz[appKey];
        }
    }
}