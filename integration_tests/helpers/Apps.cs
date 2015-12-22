using System;
using System.Collections.Generic;

namespace Appium.Integration.Tests.Helpers
{
	public class Apps
	{
		static Dictionary<string, string> TestApps = new Dictionary<string, string> {
			{ "iosTestApp", "http://appium.github.io/appium/assets/TestApp7.1.app.zip" },
			{ "iosWebviewApp", "http://appium.github.io/appium/assets/WebViewApp7.1.app.zip" },
			{ "iosUICatalogApp", "http://appium.github.io/appium/assets/UICatalog7.1.app.zip" },
			{ "androidApiDemos", "http://appium.github.io/appium/assets/ApiDemos-debug.apk" },
			{ "selendroidTestApp", "http://appium.github.io/appium/assets/selendroid-test-app-0.10.0.apk" },
			{ "iosWebviewAppLocal", "http://localhost:3000/WebViewApp7.1.app.zip" },
			{ "androidApiDemosLocal", "http://localhost:3001/ApiDemos-debug.apk" }
		};
			
		public static string get(string appKey) {
			return TestApps[appKey];			
		}
	}
}
