﻿//Licensed under the Apache License, Version 2.0 (the "License");
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

using System.Collections.Generic;

namespace OpenQA.Selenium.Appium
{
    /// <summary>
    /// Container class for the command tuple
    /// </summary>
    public class AppiumCommand
    {
        private static List<AppiumCommand> CommandList = new List<AppiumCommand>()
        {
            #region Context Commands

            new AppiumCommand(HttpCommandInfo.GetCommand, AppiumDriverCommand.Contexts, "/session/{sessionId}/contexts"),
            new AppiumCommand(HttpCommandInfo.GetCommand, AppiumDriverCommand.GetContext, "/session/{sessionId}/context"),
            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.SetContext, "/session/{sessionId}/context"),

            #endregion Context Commands

            #region Driver Commands

            #region Applications Management

            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.InstallApp,
                "/session/{sessionId}/appium/device/install_app"),
            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.RemoveApp,
                "/session/{sessionId}/appium/device/remove_app"),
            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.ActivateApp,
                "/session/{sessionId}/appium/device/activate_app"),
            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.TerminateApp,
                "/session/{sessionId}/appium/device/terminate_app"),
            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.IsAppInstalled,
                "/session/{sessionId}/appium/device/app_installed"),

	        #endregion

            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.PushFile,
                "/session/{sessionId}/appium/device/push_file"),
            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.PullFile,
                "/session/{sessionId}/appium/device/pull_file"),
            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.PullFolder,
                "/session/{sessionId}/appium/device/pull_folder"),

            new AppiumCommand(HttpCommandInfo.GetCommand, AppiumDriverCommand.GetSettings,
                "/session/{sessionId}/appium/settings"),
            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.UpdateSettings,
                "/session/{sessionId}/appium/settings"),

            #endregion Driver Commands


            // Enable W3C Actions on AppiumWebDriver

            #region W3C Actions

            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.Actions,
                "/session/{sessionId}/actions"),

            #endregion W3C Actions

            #region JSON Wire Protocol Commands

            new AppiumCommand(HttpCommandInfo.GetCommand, AppiumDriverCommand.GetOrientation,
                "/session/{sessionId}/orientation"),
            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.SetOrientation,
                "/session/{sessionId}/orientation"),
            new AppiumCommand(HttpCommandInfo.GetCommand, AppiumDriverCommand.GetLocation, "/session/{sessionId}/location"),
            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.SetLocation,
                "/session/{sessionId}/location"),

            #region Input Method (IME)

            new AppiumCommand(HttpCommandInfo.GetCommand, AppiumDriverCommand.GetAvailableEngines,
                "/session/{sessionId}/ime/available_engines"),
            new AppiumCommand(HttpCommandInfo.GetCommand, AppiumDriverCommand.GetActiveEngine,
                "/session/{sessionId}/ime/active_engine"),
            new AppiumCommand(HttpCommandInfo.GetCommand, AppiumDriverCommand.IsIMEActive,
                "/session/{sessionId}/ime/activated"),
            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.ActivateEngine,
                "/session/{sessionId}/ime/activate"),
            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.DeactivateEngine,
                "/session/{sessionId}/ime/deactivate"),

            #endregion Input Method (IME)

            #region SeassionData

            new AppiumCommand(HttpCommandInfo.GetCommand, AppiumDriverCommand.GetSession, "/session/{sessionId}/"),

            #endregion SeassionData

            #region Recording Screen

            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.StartRecordingScreen,
                "/session/{sessionId}/appium/start_recording_screen"),
            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.StopRecordingScreen,
                "/session/{sessionId}/appium/stop_recording_screen"),

            #endregion Recording Screen

            #endregion JSON Wire Protocol Commands

            #region Compare Images

            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.CompareImages,
                "/session/{sessionId}/appium/compare_images"),

            #endregion

            #region Logs

            new AppiumCommand(HttpCommandInfo.GetCommand, DriverCommand.GetAvailableLogTypes,
                "session/{sessionId}/se/log/types"),
            new AppiumCommand(HttpCommandInfo.PostCommand, DriverCommand.GetLog, "session/{sessionId}/se/log"),

            #endregion

            #region Event logs

            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.GetEvents, "session/{sessionId}/appium/events"),
            new AppiumCommand(HttpCommandInfo.PostCommand, AppiumDriverCommand.LogEvent, "session/{sessionId}/appium/log_event")

            #endregion
        };

        /// <summary>
        /// This method adds Appium-specific commands to the given
        /// CommandInfoRepository
        /// </summary>
        /// <param name="repo">is a CommandInfoRepository instance which is used</param>
        /// <returns>The given CommandInfoRepository instance with added Appium-specific commands</returns>
        internal static ICommandExecutor Merge(ICommandExecutor repo)
        {
            foreach (AppiumCommand entry in CommandList)
            {
                var commandInfo = new HttpCommandInfo(entry.CommandType, entry.ApiEndpoint);
                repo.TryAddCommand(entry.CommandName, commandInfo);
            }

            return repo;
        }

        /// <summary>
        /// command type
        /// </summary>
        internal readonly string CommandType;

        /// <summary>
        /// Command
        /// </summary>
        internal readonly string CommandName;

        /// <summary>
        /// API Endpoint
        /// </summary>
        internal readonly string ApiEndpoint;

        /// <summary>
        /// Constructor
        /// <param name="commandType">type of command (get/post/delete)</param>
        /// <param name="command">Command</param>
        /// <param name="apiEndpoint">api endpoint</param>
        /// </summary>
        public AppiumCommand(string commandType, string command, string apiEndpoint)
        {
            CommandType = commandType;
            CommandName = command;
            ApiEndpoint = apiEndpoint;
        }
    }
}