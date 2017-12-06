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

using OpenQA.Selenium.Remote;
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

            new AppiumCommand(CommandInfo.GetCommand, AppiumDriverCommand.Contexts, "/session/{sessionId}/contexts"),
            new AppiumCommand(CommandInfo.GetCommand, AppiumDriverCommand.GetContext, "/session/{sessionId}/context"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.SetContext, "/session/{sessionId}/context"),

            #endregion Context Commands

            #region Appium Commands

            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.ShakeDevice,
                "/session/{sessionId}/appium/device/shake"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.LockDevice,
                "/session/{sessionId}/appium/device/lock"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.IsLocked,
                "/session/{sessionId}/appium/device/is_locked"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.UnlockDevice,
                "/session/{sessionId}/appium/device/unlock"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.ToggleAirplaneMode,
                "/session/{sessionId}/appium/device/toggle_airplane_mode"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.PressKeyCode,
                "/session/{sessionId}/appium/device/press_keycode"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.LongPressKeyCode,
                "/session/{sessionId}/appium/device/long_press_keycode"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.Rotate,
                "/session/{sessionId}/appium/device/rotate"),
            new AppiumCommand(CommandInfo.GetCommand, AppiumDriverCommand.GetCurrentActivity,
                "/session/{sessionId}/appium/device/current_activity"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.InstallApp,
                "/session/{sessionId}/appium/device/install_app"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.RemoveApp,
                "/session/{sessionId}/appium/device/remove_app"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.IsAppInstalled,
                "/session/{sessionId}/appium/device/app_installed"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.PushFile,
                "/session/{sessionId}/appium/device/push_file"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.PullFile,
                "/session/{sessionId}/appium/device/pull_file"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.PullFolder,
                "/session/{sessionId}/appium/device/pull_folder"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.ToggleWiFi,
                "/session/{sessionId}/appium/device/toggle_wifi"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.ToggleLocationServices,
                "/session/{sessionId}/appium/device/toggle_location_services"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.LaunchApp,
                "/session/{sessionId}/appium/app/launch"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.CloseApp,
                "/session/{sessionId}/appium/app/close"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.ResetApp,
                "/session/{sessionId}/appium/app/reset"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.BackgroundApp,
                "/session/{sessionId}/appium/app/background"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.EndTestCoverage,
                "/session/{sessionId}/appium/app/end_test_coverage"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.GetAppStrings,
                "/session/{sessionId}/appium/app/strings"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.HideKeyboard,
                "/session/{sessionId}/appium/device/hide_keyboard"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.OpenNotifications,
                "/session/{sessionId}/appium/device/open_notifications"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.StartActivity,
                "/session/{sessionId}/appium/device/start_activity"),
            new AppiumCommand(CommandInfo.GetCommand, AppiumDriverCommand.GetSettings,
                "/session/{sessionId}/appium/settings"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.UpdateSettings,
                "/session/{sessionId}/appium/settings"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.TouchID,
                "/session/{sessionId}/appium/simulator/touch_id"),

            #endregion Appium Commands

            #region Touch Commands

            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.PerformMultiAction,
                "/session/{sessionId}/touch/multi/perform"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.PerformTouchAction,
                "/session/{sessionId}/touch/perform"),

            #endregion Touch Commands

            #region JSON Wire Protocol Commands

            new AppiumCommand(CommandInfo.GetCommand, AppiumDriverCommand.GetOrientation,
                "/session/{sessionId}/orientation"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.SetOrientation,
                "/session/{sessionId}/orientation"),
            new AppiumCommand(CommandInfo.GetCommand, AppiumDriverCommand.GetConnectionType,
                "/session/{sessionId}/network_connection"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.SetConnectionType,
                "/session/{sessionId}/network_connection"),
            new AppiumCommand(CommandInfo.GetCommand, AppiumDriverCommand.GetLocation, "/session/{sessionId}/location"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.SetLocation,
                "/session/{sessionId}/location"),

            #region Input Method (IME)

            new AppiumCommand(CommandInfo.GetCommand, AppiumDriverCommand.GetAvailableEngines,
                "/session/{sessionId}/ime/available_engines"),
            new AppiumCommand(CommandInfo.GetCommand, AppiumDriverCommand.GetActiveEngine,
                "/session/{sessionId}/ime/active_engine"),
            new AppiumCommand(CommandInfo.GetCommand, AppiumDriverCommand.IsIMEActive,
                "/session/{sessionId}/ime/activated"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.ActivateEngine,
                "/session/{sessionId}/ime/activate"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.DeactivateEngine,
                "/session/{sessionId}/ime/deactivate"),

            #endregion Input Method (IME) 

            #region Input value

            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.ReplaceValue,
                "/session/{sessionId}/appium/element/{id}/replace_value"),
            new AppiumCommand(CommandInfo.PostCommand, AppiumDriverCommand.SetValue,
                "/session/{sessionId}/appium/element/{id}/value"),

            #endregion Input value

            #region Device Time

            new AppiumCommand(CommandInfo.GetCommand, AppiumDriverCommand.GetDeviceTime,
                "/session/{sessionId}/appium/device/system_time"),

            #endregion Device Time

            #region SeassionData

            new AppiumCommand(CommandInfo.GetCommand, AppiumDriverCommand.GetSession, "/session/{sessionId}/")

            #endregion SeassionData

            #endregion JSON Wire Protocol Commands
        };

        /// <summary>
        /// This method adds Appium-specific commands to the given 
        /// CommandInfoRepository
        /// </summary>
        /// <param name="repo">is a CommandInfoRepository instance which is used</param>
        /// <returns>The given CommandInfoRepository instance with added Appium-specific commands</returns>
        internal static CommandInfoRepository Merge(CommandInfoRepository repo)
        {
            foreach (AppiumCommand entry in CommandList)
            {
                var commandInfo = new CommandInfo(entry.CommandType, entry.ApiEndpoint);
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
        /// </summary>
        /// <param name="commandType">type of command (get/post/delete)</param>
        /// <param name="command">Command</param>
        /// <param name="apiEndpoint">api endpoint</param>
        /// <summary>
        public AppiumCommand(string commandType, string command, string apiEndpoint)
        {
            CommandType = commandType;
            CommandName = command;
            ApiEndpoint = apiEndpoint;
        }
    }
}