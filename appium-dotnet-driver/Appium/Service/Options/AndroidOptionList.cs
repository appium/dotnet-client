//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//See the NOTICE file distributed with this work for additional
//information regarding copyright ownership.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.


using System;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.Service.Options
{
	///<summary>
	/// Here is the list of Android specific server arguments.
	/// All flags are optional, but some are required in conjunction with certain others.
	/// The full list is available here: http://appium.io/slate/en/master/?ruby#appium-server-arguments
	/// Android specific arguments are marked by (Android-only)
	/// </summary>
	public sealed class AndroidOptionList
	{
		private static void CheckArgumentAndThrowException(string argument, string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				throw new ArgumentException("The argument " + argument + " requires not empty value");
			}
		}

		///<summary>
		/// Port to use on device to talk to Appium<br/>
		/// Sample:<br/>
		/// --bootstrap-port 4724
		///</summary>
		public static KeyValuePair<string, string> BootstrapPort(string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return new KeyValuePair<string, string>("--bootstrap-port", "4724");
			}
			else
			{
				return new KeyValuePair<string, string>("--bootstrap-port", value);
			}
		}

		///<summary>
		/// Java package of the Android app you want to run (e.g.,
		/// com.example.android.MyApp)<br/>
		/// Sample:<br/>
		/// --app-pkg com.example.android.MyApp
		///</summary>
		public static KeyValuePair<string, string> Package(string value)
		{
			string argument = "--app-pkg";
			CheckArgumentAndThrowException(argument, value);
			return new KeyValuePair<string, string>(argument, value);
		}


		///<summary>
		/// Activity name for the Android activity you want to launch
		/// from your package (e.g., MainActivity)<br/>
		/// Sample:<br/>
		/// --app-activity MainActivity
		///</summary>
		public static KeyValuePair<string, string> Activity(string value)
		{
			string argument = "--app-activity";
			CheckArgumentAndThrowException(argument, value);
			return new KeyValuePair<string, string>(argument, value);
		}

		///<summary>
		/// Package name for the Android activity you want to wait for
		/// (e.g., com.example.android.MyApp)<br/>
		/// Sample:<br/>
		/// --app-wait-package com.example.android.MyApp
		///</summary>
		public static KeyValuePair<string, string> AppWaitPackage(string value)
		{
			string argument = "--app-wait-package";
			CheckArgumentAndThrowException(argument, value);
			return new KeyValuePair<string, string>(argument, value);
		}

		///<summary>
		/// Activity name for the Android activity you want to wait
		/// for (e.g., SplashActivity)
		/// Sample:<br/>
		/// --app-wait-activity SplashActivity
		///</summary>
		public static KeyValuePair<string, string> AppWaitActivity(string value)
		{
			string argument = "--app-wait-activity";
			CheckArgumentAndThrowException(argument, value);
			return new KeyValuePair<string, string>(argument, value);
		}


		///<summary>
		/// Fully qualified instrumentation class.
		/// Passed to -w in adb shell am instrument -e coverage true -w <br/>
		/// Sample: <br/>
		/// --android-coverage com.my.Pkg/com.my.Pkg.instrumentation.MyInstrumentation
		///</summary>
		public static KeyValuePair<string, string> AndroidCoverage(string value)
		{
			string argument = "--android-coverage";
			CheckArgumentAndThrowException(argument, value);
			return new KeyValuePair<string, string>(argument, value);
		}

		///<summary>
		/// Name of the avd to launch<br/>
		/// Sample:<br/>
		/// --avd @default
		///</summary>
		public static KeyValuePair<string, string> Avd(string value)
		{
			string argument = "--avd";
			CheckArgumentAndThrowException(argument, value);
			return new KeyValuePair<string, string>(argument, value);
		}


		///<summary>
		/// Additional emulator arguments to launch the avd<br/>
		/// Sample:<br/>
		/// --avd-args -no-snapshot-load
		///</summary>
		public static KeyValuePair<string, string> AvdArguments(string value)
		{
			string argument = "--avd-args";
			CheckArgumentAndThrowException(argument, value);
			return new KeyValuePair<string, string>(argument, value);
		}

		///<summary>
		/// Timeout in seconds while waiting for device to become
		/// ready<br/>
		/// Sample:<br/>
		/// --device-ready-timeout 5
		///</summary>
		public static KeyValuePair<string, string> DeviceReadyTimeout(string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return new KeyValuePair<string, string>("--device-ready-timeout", "5");
			}
			else
			{
				return new KeyValuePair<string, string>("--device-ready-timeout", value);
			}
		}


		///<summary>
		/// Local port used for communication with Selendroid<br/>
		/// Sample:<br/>
		/// --selendroid-port 8080
		///</summary>
		public static KeyValuePair<string, string> SelendroidPort(string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return new KeyValuePair<string, string>("--selendroid-port", "8080");
			}
			else
			{
				return new KeyValuePair<string, string>("--selendroid-port", value);
			}
		}

		///<summary>
		/// When set the keystore will be used to sign apks.<br/>
		///</summary>
		public static KeyValuePair<string, string> UseKeyStore()
		{
			return new KeyValuePair<string, string>("--use-keystore", string.Empty);
		}

		///<summary>
		/// Path to keystore<br/>
		/// Sample:<br/>
		/// --keystore-path /Users/user/.android/debug.keystore
		///</summary>
		public static KeyValuePair<string, string> KeyStorePath(string value)
		{
			string argument = "--keystore-path";
			CheckArgumentAndThrowException(argument, value);
			return new KeyValuePair<string, string>(argument, value);
		}

		///<summary>
		/// Password to keystore<br/>
		///</summary>
		public static KeyValuePair<string, string> KeystorePassword(string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return new KeyValuePair<string, string>("--keystore-password", "android");
			}
			else
			{
				return new KeyValuePair<string, string>("--keystore-password", value);
			}
		}

		///<summary>
		/// Key alias<br/>
		///</summary>
		public static KeyValuePair<string, string> KeyAlias(string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return new KeyValuePair<string, string>("--key-alias", "androiddebugkey");
			}
			else
			{
				return new KeyValuePair<string, string>("--key-alias", value);
			}
		}

		///<summary>
		/// Key password<br/>
		///</summary>
		public static KeyValuePair<string, string> KeyPassword(string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return new KeyValuePair<string, string>("--key-password", "android");
			}
			else
			{
				return new KeyValuePair<string, string>("--key-password", value);
			}
		}

		///<summary>
		/// Intent action which will be used to start activity<br/>
		/// Sample:<br/>
		/// --intent-action android.intent.action.MAIN
		///</summary>
		public static KeyValuePair<string, string> IntentAction(string value)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			if (String.IsNullOrEmpty(value))
			{
				return new KeyValuePair<string, string>("--intent-action", "android.intent.action.MAIN");
			}
			else
			{
				return new KeyValuePair<string, string>("--intent-action", value);
			}
		}

		///<summary>
		/// Intent category which will be used to start activity<br/>
		/// Sample:<br/>
		/// --intent-category android.intent.category.APP_CONTACTS
		///</summary>
		public static KeyValuePair<string, string> IntentCategory(string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return new KeyValuePair<string, string>("--intent-category", "android.intent.category.LAUNCHER");
			}
			else
			{
				return new KeyValuePair<string, string>("--intent-category", value);
			}
		}

		///<summary>
		/// Flags that will be used to start activity<br/>
		/// Sample:<br/>
		/// --intent-flags 0x10200000
		///</summary>
		public static KeyValuePair<string, string> IntentFlags(string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return new KeyValuePair<string, string>("--intent-flags", "0x10200000");
			}
			else
			{
				return new KeyValuePair<string, string>("--intent-flags", value);
			}
		}

		///<summary>
		/// Additional intent arguments that will be used to start
		/// activity<br/>
		/// Sample:<br/>
		/// --intent-args 0x10200000
		///</summary>
		public static KeyValuePair<string, string> IntentArgumants(string value)
		{
			string argument = "--intent-args";
			CheckArgumentAndThrowException(argument, value);
			return new KeyValuePair<string, string>(argument, value);
		}

		///<summary>
		/// When included, refrains from stopping the app before
		/// restart<br/>
		///</summary>
		public static KeyValuePair<string, string> DoNotStopAppOnReset()
		{
			return new KeyValuePair<string, string>("--dont-stop-app-on-reset", string.Empty);
		}

		///<summary>
		/// If set, prevents Appium from killing the adb server
		/// instance<br/>
		///</summary>
		public static KeyValuePair<string, string> SuppressAdbKillServer()
		{
			return new KeyValuePair<string, string>("--suppress-adb-kill-server", string.Empty);
		}
	}
}
