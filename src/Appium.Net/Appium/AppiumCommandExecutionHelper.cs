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
using OpenQA.Selenium.Appium.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace OpenQA.Selenium.Appium
{
    public class AppiumCommandExecutionHelper
    {
        #region Device Commands

        #region Device Key Commands

        private static void ExecutePressKey(IExecuteMethod executeMethod, KeyEvent keyEvent, bool isLongPress = false)
        {
            var parameters = keyEvent.Build();
            parameters.Add("isLongPress", isLongPress);

            executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:pressKey",
                ["args"] = new object[] {parameters}
            });
        }

        private static void ExecutePressKeyWithMetastate(IExecuteMethod executeMethod, int keyCode, int metastate = -1, bool isLongPress = false)
        {
            var parameters = new Dictionary<string, object>()
                {["keycode"] = keyCode};
            if (metastate > 0)
            {
                parameters.Add("metastate", metastate);
            }
            executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:pressKey",
                ["args"] = new object[] {parameters}
            });
        }

        /// <summary>
        /// Press a key code on the device.
        /// Please check <see href="https://github.com/appium/appium-uiautomator2-driver#mobile-presskey">mobile:pressKey</see> for more details.
        /// </summary>
        /// <param name="executeMethod">The execute method</param>
        /// <param name="keyEvent">The key event</param>
        public static void PressKeyCode(IExecuteMethod executeMethod, KeyEvent keyEvent)
            => ExecutePressKey(executeMethod, keyEvent, false);

        /// <summary>
        /// Long press a key code on the device with `isLongPress` parameter in <see href="https://github.com/appium/appium-uiautomator2-driver#mobile-presskey">mobile:pressKey</see>.
        /// Please check <see href="https://github.com/appium/appium-uiautomator2-driver#mobile-presskey">mobile:pressKey</see> for more details.
        /// </summary>
        /// <param name="executeMethod">The execute method</param>
        /// <param name="keyEvent">The key event</param>
        public static void LongPressKeyCode(IExecuteMethod executeMethod, KeyEvent keyEvent)
            => ExecutePressKey(executeMethod, keyEvent, true);

        /// <summary>
        /// Press a key code on the device.
        /// Please check <see href="https://github.com/appium/appium-uiautomator2-driver#mobile-presskey">mobile:pressKey</see> for more details.
        /// </summary>
        /// <param name="executeMethod">The execute method</param>
        /// <param name="keyCode">The key code</param>
        /// <param name="metastate">A pressed meta key</param>
        public static void PressKeyCode(IExecuteMethod executeMethod, int keyCode, int metastate = -1)
            => ExecutePressKeyWithMetastate(executeMethod, keyCode, metastate, false);

        /// <summary>
        /// Long press a key code on the device with "isLongPress" parameter in <see href="https://github.com/appium/appium-uiautomator2-driver#mobile-presskey">mobile:pressKey</see>.
        /// Please check <see href="https://github.com/appium/appium-uiautomator2-driver#mobile-presskey">mobile:pressKey</see> for more details.
        /// </summary>
        /// <param name="executeMethod">The execute method</param>
        /// <param name="keyCode">The key code</param>
        /// <param name="metastate">A pressed meta key</param>
        public static void LongPressKeyCode(IExecuteMethod executeMethod, int keyCode, int metastate = -1)
            => ExecutePressKeyWithMetastate(executeMethod, keyCode, metastate, true);

        /// <summary>
        /// Hide keyboard with <see href="https://github.com/appium/appium-uiautomator2-driver#mobile-hidekeyboard">mobile:hideKeyboard</see> for Android
        /// or <see href="https://appium.github.io/appium-xcuitest-driver/latest/reference/execute-methods/#mobile-hidekeyboard">mobile:hideKeyboard</see> for iOS.
        /// </summary>
        /// <param name="executeMethod">The execute method</param>
        /// <param name="key">One or more keyboard key names used to close/hide it. It works only for Appium XCUITest driver.</param>
        public static void HideKeyboard(IExecuteMethod executeMethod, string key = null)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            if (key != null)
            {
                parameters.Add("keys", new List<string> { key });
            }

            executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:hideKeyboard",
                ["args"] = new object[] {parameters}
            });
        }

        /// <summary>
        /// Check the keyboad hidden state with <see href="https://github.com/appium/appium-uiautomator2-driver#mobile-iskeyboardshown">mobile:isKeyboardShown</see> for Android
        /// or <see href="https://appium.github.io/appium-xcuitest-driver/latest/reference/execute-methods/#mobile-iskeyboardshown">mobile:isKeyboardShown</see> for iOS.
        /// </summary>
        /// <param name="executeMethod">The execute method</param>
        public static bool IsKeyboardShown(IExecuteMethod executeMethod)
        {
            return Convert.ToBoolean(
                executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                    ["script"] = "mobile:isKeyboardShown",
                    ["args"] = Array.Empty<object>()
                }).Value
            );
        }

        #endregion

        /// <summary>
        /// Set clipboard content with <see href="https://github.com/appium/appium-uiautomator2-driver#mobile-setclipboard">mobile:setClipboard</see> for Android
        /// or <see href="https://appium.github.io/appium-xcuitest-driver/latest/reference/execute-methods/#mobile-setclipboard">mobile:setClipboard</see> for iOS.
        /// </summary>
        /// <param name="executeMethod">The execute method</param>
        /// <param name="clipboardContentType">The content type to set</param>
        /// <param name="base64Content">The content to set</param>
        public static void MobileSetClipboard(IExecuteMethod executeMethod, ClipboardContentType clipboardContentType,
            string base64Content)
        {
            var contentType = clipboardContentType.ToString().ToLowerInvariant();

            switch (clipboardContentType)
            {
                case ClipboardContentType.Image:
                case ClipboardContentType.Url:
                    if (executeMethod.GetType() == typeof(AndroidDriver))
                    {
                        throw new NotImplementedException(
                            $"Android only supports contentType: {nameof(ClipboardContentType.PlainText)}");
                    }
                    break;
                default:
                    break;
            }
            executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:setClipboard",
                ["args"] = new object[] {
                    new Dictionary<string, object> {
                        ["content"] = base64Content,
                        ["contentType"] = contentType,
                        ["label"] = null
                    }
                }
            });
        }

        /// <summary>
        /// Get clipboard content with with <see href="https://github.com/appium/appium-uiautomator2-driver#mobile-getclipboard">mobile:getClipboard</see> for Android
        /// or <see href="https://appium.github.io/appium-xcuitest-driver/latest/reference/execute-methods/#mobile-getclipboard">mobile:getClipboard</see> for iOS.
        /// </summary>
        /// <param name="executeMethod">The execute method</param>
        /// <param name="clipboardContentType">The content type to get</param>
        public static string MobileGetClipboard(IExecuteMethod executeMethod, ClipboardContentType clipboardContentType)
        {
            var contentType = clipboardContentType.ToString().ToLowerInvariant();
            switch (clipboardContentType)
            {
                case ClipboardContentType.Image:
                case ClipboardContentType.Url:
                    if (executeMethod.GetType() == typeof(AndroidDriver))
                    {
                        throw new NotImplementedException(
                            $"Android only supports contentType: {nameof(ClipboardContentType.PlainText)}");
                    }
                    break;
                default:
                    // including ClipboardContentType.PlainText
                    break;
            }
            return (string) executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                ["script"] = "mobile:getClipboard",
                ["args"] = new object[] {
                    new Dictionary<string, object> {
                        ["contentType"] = contentType
                    }
                }
            }).Value;
        }

        /// <summary>
        /// Set clipboard text content as "contentType" is "plaintext" with
        /// <see href="https://github.com/appium/appium-uiautomator2-driver#mobile-setclipboard">mobile:setClipboard</see> for Android
        /// or <see href="https://appium.github.io/appium-xcuitest-driver/latest/reference/execute-methods/#mobile-setclipboard">mobile:setClipboard</see> for iOS.
        /// </summary>
        /// <param name="executeMethod">The execute method</param>
        /// <param name="textContent">The content to set</param>
        /// <param name="label">The optinal label to identify the current clipboard payload for Android</param>
        public static string SetClipboardText(IExecuteMethod executeMethod, string textContent, string label)
        {
            if (textContent == null)
            {
                throw new ArgumentException($"{nameof(textContent)} cannot be null");
            }

            var encodedStringContentBytes = Encoding.UTF8.GetBytes(textContent);

            return (string) executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                    ["script"] = "mobile:setClipboard",
                    ["args"] = new object[] {
                        new Dictionary<string, object> {
                            ["content"] = Convert.ToBase64String(encodedStringContentBytes),
                            ["contentType"] = ClipboardContentType.PlainText.ToString().ToLowerInvariant(),
                            ["label"] = label
                    }
                }
            }).Value;
        }

        /// <summary>
        /// Get clipboard text content with <see href="https://github.com/appium/appium-uiautomator2-driver#mobile-setclipboard">mobile:setClipboard</see> for Android
        /// or <see href="https://appium.github.io/appium-xcuitest-driver/latest/reference/execute-methods/#mobile-setclipboard">mobile:setClipboard</see> for iOS.
        /// </summary>
        /// <param name="executeMethod">The execute method</param>
        public static string GetClipboardText(IExecuteMethod executeMethod)
        {
            var encodedContentBytes =
                Convert.FromBase64String(MobileGetClipboard(executeMethod, ClipboardContentType.PlainText));
            return Encoding.UTF8.GetString(encodedContentBytes);
        }

        public static void PushFile(IExecuteMethod executeMethod, string pathOnDevice, byte[] base64Data) =>
            executeMethod.Execute(AppiumDriverCommand.PushFile, new Dictionary<string, object>()
                {["path"] = pathOnDevice, ["data"] = base64Data});

        public static void FingerPrint(IExecuteMethod executeMethod, int fingerprintId) {
             executeMethod.Execute(DriverCommand.ExecuteScript, new Dictionary<string, object> {
                    ["script"] = "mobile:fingerprint",
                    ["args"] = new object[] {
                        new Dictionary<string, object> {
                            ["fingerprintId"] = fingerprintId
                    }
                }
            });
        }

        /// <summary>
        /// Push file with <see href="https://github.com/appium/appium-uiautomator2-driver#mobile-pushfile">mobile:pushFile</see> for Android
        /// or <see href="https://appium.github.io/appium-xcuitest-driver/latest/reference/execute-methods/#mobile-pushfile">mobile:pushFile</see> for iOS.
        /// </summary>
        /// <param name="executeMethod">The execute method</param>
        public static void PushFile(IExecuteMethod executeMethod, string pathOnDevice, FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentException("The file argument should not be null");
            }

            if (!file.Exists)
            {
                throw new ArgumentException("The file " + file.FullName + " doesn't exist");
            }

            byte[] bytes = File.ReadAllBytes(file.FullName);
            string fileBase64Data = Convert.ToBase64String(bytes);
            PushFile(executeMethod, pathOnDevice, Convert.FromBase64String(fileBase64Data));
        }

        #endregion Device Commands

        /// <summary>
        /// Prepares single command argument
        /// </summary>
        /// <param name="name">The parameter name</param>
        /// <param name="value">The parameter value</param>
        /// <returns></returns>
        internal static Dictionary<string, object> PrepareArgument(string name, object value)
        {
            return new Dictionary<string, object> {{name, value}};
        }

        /// <summary>
        /// Prepares a collection of command arguments
        /// </summary>
        /// <param name="names">The array of parameter names</param>
        /// <param name="values">The array of parameter values</param>
        /// <returns></returns>
        internal static Dictionary<string, object> PrepareArguments(string[] names, object[] values)
        {
            var parameterBuilder = new Dictionary<string, object>();
            if (names.Length != values.Length) return parameterBuilder;
            for (var i = 0; i < names.Length; i++)
            {
                if (names[i] != string.Empty && values[i] != null)
                {
                    parameterBuilder.Add(names[i], values[i]);
                }
            }

            return parameterBuilder;
        }
    }
}
