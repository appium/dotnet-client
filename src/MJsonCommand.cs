using System;

// <copyright file="DriverCommand.cs" company="WebDriver Committers">
// Copyright 2007-2011 WebDriver committers
// Copyright 2007-2011 Google Inc.
// Portions copyright 2011 Software Freedom Conservancy
//
// Licensed under the Apache License = string.Empty; Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing = string.Empty; software
// distributed under the License is distributed on an "AS IS" BASIS = string.Empty;
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND = string.Empty; either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

namespace OpenQA.Selenium.Remote
{
	/// <summary>
	/// Values describing the list of commands understood by a remote server using the MJSON 
	/// extension to the JSON wire protocol.
	/// </summary>
	public static class MJsonCommand
	{
		/// <summary>
		/// Represents the Shake Device Mapping command
		/// </summary>
		public static readonly string ShakeDevice = "shakeDevice";

		/// <summary>
		/// Represents the Lock Device Mapping command
		/// </summary>
		public static readonly string LockDevice = "lockDevice";

	}
}
	