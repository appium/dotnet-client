using System;
using System.Text.RegularExpressions;

namespace OpenQA.Selenium.Appium.Samples
{
	class MainClass
	{
		private static string getProjectRootPath()
		{
			return Regex.Replace(System.Reflection.Assembly.GetExecutingAssembly().Location, "/samples/.*$", "/samples");
			//return appiumDir + "/sample-code/apps/TestApp/build/Release-iphonesimulator/TestApp.app";
		}



		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
			// Console.WriteLine (getProjectRootPath());
			(new Basics()).Run(getProjectRootPath() + "/assets/TestApp.zip");
		}
	}
}
