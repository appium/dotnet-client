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
	/// Here is the list of iOS specific server arguments.
	/// All flags are optional, but some are required in conjunction with certain others.
	/// The full list is available here: http://appium.io/slate/en/master/?ruby#appium-server-arguments
	/// Android specific arguments are marked by (IOS-only)
	/// </summary>
	public sealed class IOSOptionList
	{
		private static void CheckArgumentAndThrowException(string argument, string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				throw new ArgumentException("The argument " + argument + " requires not empty value");
			}
		}

		///<summary>
		/// the relative path of the dir where Localizable.strings file
		/// resides<br/>
		/// Sample:<br/>
		/// --localizable-strings-dir en.lproj
		///</summary>
		public static KeyValuePair<string, string> LocalizableStringsDir(string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return new KeyValuePair<string, string>("--localizable-strings-dir", "en.lproj");
			}
			else
			{
				return new KeyValuePair<string, string>("--localizable-strings-dir", value);
			}
		}

		///<summary>
		/// absolute path to compiled .ipa file
		/// Sample:<br/>
		/// --ipa /abs/path/to/my.ipa
		///</summary>
		public static KeyValuePair<string, string> Ipa(string value)
		{
			string argument = "--ipa";
			CheckArgumentAndThrowException(argument, value);
			return new KeyValuePair<string, string>(argument, value);
		}

		///<summary>
		/// How many times to retry launching Instruments before saying it
		/// crashed or timed out<br/>
		/// Sample:<br/>
		/// --backend-retries 3
		///</summary>
		public static KeyValuePair<string, string> BackEndRetries(string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return new KeyValuePair<string, string>("--backend-retries", "3");
			}
			else
			{
				return new KeyValuePair<string, string>("--backend-retries", value);
			}
		}

		///<summary>
		///  how long in ms to wait for Instruments to launch<br/>
		///</summary>
		public static KeyValuePair<string, string> LaunchTimeout(string value)
		{
			if (String.IsNullOrEmpty(value))
			{
				return new KeyValuePair<string, string>("--launch-timeout", "90000");
			}
			else
			{
				return new KeyValuePair<string, string>("--launch-timeout", value);
			}
		}

		///<summary>
		/// IOS has a weird built-in unavoidable delay. We patch this in
		/// appium. If you do not want it patched, pass in this flag.<br/>
		///</summary>
		public static KeyValuePair<string, string> UseNativeInstruments()
		{
			return new KeyValuePair<string, string>("--native-instruments-lib", string.Empty);
		}

		///<summary>
		/// Use the safari app<br/>
		///</summary>
		public static KeyValuePair<string, string> Safari()
		{
			return new KeyValuePair<string, string>("--safari", string.Empty);
		}

		///<summary>
		/// use the default simulator that instruments launches
		/// on its own<br/>
		///</summary>
		public static KeyValuePair<string, string> DefaultDevice()
		{
			return new KeyValuePair<string, string>("--default-device", string.Empty);
		}

		///<summary>
		/// Use the iPhone Simulator no matter what the app wants<br/>
		///</summary>
		public static KeyValuePair<string, string> ForceIPhoneSimulator()
		{
			return new KeyValuePair<string, string>("--force-iphone", string.Empty);
		}

		///<summary>
		/// Use the iPad Simulator no matter what the app wants<br/>
		///</summary>
		public static KeyValuePair<string, string> ForceIPadSimulator()
		{
			return new KeyValuePair<string, string>("--force-ipad", string.Empty);
		}

		///<summary>
		/// Calendar format for the iOS simulator<br/>
		/// Sample:<br/>
		/// --calendar-format gregorian
		///</summary>
		public static KeyValuePair<string, string> CalendarFormat(string value)
		{
			string argument = "--calendar-format";
			CheckArgumentAndThrowException(argument, value);
			return new KeyValuePair<string, string>(argument, value);
		}

		///<summary>
		/// use LANDSCAPE or PORTRAIT to initialize all requests to this
		/// orientation<br/>
		/// Sample:<br/>
		/// --orientation LANDSCAPE
		///</summary>
		public static KeyValuePair<string, string> Orientation(string value)
		{
			string argument = "--orientation";
			CheckArgumentAndThrowException(argument, value);
			return new KeyValuePair<string, string>(argument, value);
		}

		///<summary>
		/// .tracetemplate file to use with Instruments<br/>
		/// Sample:<br/>
		/// --tracetemplate /Users/me/Automation.tracetemplate
		///</summary>
		public static KeyValuePair<string, string> TraceTemplate(string value)
		{
			string argument = "--tracetemplate";
			CheckArgumentAndThrowException(argument, value);
			return new KeyValuePair<string, string>(argument, value);
		}


		///<summary>
		/// custom path to the instruments commandline tool<br/>
		/// Sample:<br/>
		/// --instruments /path/to/instruments
		///</summary>
		public static KeyValuePair<string, string> Intstruments(string value)
		{
			string argument = "--instruments";
			CheckArgumentAndThrowException(argument, value);
			return new KeyValuePair<string, string>(argument, value);
		}

		///<summary>
		/// if set, the iOS simulator log will be written to the console<br/>
		///</summary>
		public static KeyValuePair<string, string> ShowSimLog()
		{
			return new KeyValuePair<string, string>("--show-sim-log", string.Empty);
		}


		///<summary>
		/// if set, the iOS system log will be written to the console<br/>
		///</summary>
		public static KeyValuePair<string, string> ShowIosLog()
		{
			return new KeyValuePair<string, string>("--show-ios-log", string.Empty);
		}

		///<summary>
		/// Whether to keep keychains (Library/Keychains) when reset app
		/// between sessions<br/>
		///</summary>
		public static KeyValuePair<string, string> KeepKeychains()
		{
			return new KeyValuePair<string, string>("--keep-keychains", string.Empty);
		}


		///<summary>
		/// Xcode 6 has a bug on some platforms where a certain simulator can only be
		/// launched without error if all other simulator devices are first deleted.
		/// This option causes Appium to delete all devices other than the one being
		/// used by Appium. Note that this is a permanent deletion, and you are
		/// responsible for using simctl or xcode to manage the categories of devices
		/// used with Appium<br/>.
		///</summary>
		public static KeyValuePair<string, string> IsolateSimDevice()
		{
			return new KeyValuePair<string, string>("--isolate-sim-device", string.Empty);
		}


		///<summary>
		/// Absolute path to directory Appium use to save ios instruments traces,
		/// defaults to /appium-instruments<br/>
		///</summary>
		public static KeyValuePair<string, string> TraceDirectory(string value)
		{
			string argument = "--trace-dir";
			CheckArgumentAndThrowException(argument, value);
			return new KeyValuePair<string, string>(argument, value);
		}
	}
}
