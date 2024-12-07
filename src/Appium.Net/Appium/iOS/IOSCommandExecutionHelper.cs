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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using OpenQA.Selenium.Appium.Enums;

namespace OpenQA.Selenium.Appium.iOS
{
    public sealed class IOSCommandExecutionHelper
    {
        public static void ShakeDevice(IExecuteMethod executeMethod) =>
            executeMethod.Execute(AppiumDriverCommand.ShakeDevice);

        public static void PerformTouchID(IExecuteMethod executeMethod, bool match) =>
            executeMethod.Execute(AppiumDriverCommand.TouchID,
                new Dictionary<string, object> {["match"] = match});

        public static void SetClipboardUrl(IExecuteMethod executeMethod, string url)
        {
            var urlEncoded = WebUtility.UrlEncode(url);
            var base64UrlBytes = Encoding.UTF8.GetBytes(urlEncoded);
            AppiumCommandExecutionHelper.SetClipboard(executeMethod, ClipboardContentType.Url, Convert.ToBase64String(base64UrlBytes));
        }

        public static string GetClipboardUrl(IExecuteMethod executeMethod)
        {
            var content = AppiumCommandExecutionHelper.GetClipboard(executeMethod, ClipboardContentType.Url);
            var urlEncodedBytes = Convert.FromBase64String(content);
            var urlDecodedBytes = Encoding.UTF8.GetString(urlEncodedBytes);

            return WebUtility.UrlDecode(urlDecodedBytes);
        }

        public static void SetClipboardImage(IExecuteMethod executeMethod, Image image)
        {
            byte[] imageBytes;
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);
                imageBytes = memoryStream.ToArray();
            }

            AppiumCommandExecutionHelper.SetClipboard(executeMethod,
                ClipboardContentType.Image,
                Convert.ToBase64String(imageBytes));
        }

        public static void SetClipboardImage(IExecuteMethod executeMethod, string base64EncodeImage)
        {
            AppiumCommandExecutionHelper.SetClipboard(executeMethod,ClipboardContentType.Image, base64EncodeImage);
        }

        public static Image GetClipboardImage(IExecuteMethod executeMethod)
        {
            var imageBytes = Convert.FromBase64String(
                AppiumCommandExecutionHelper.GetClipboard(executeMethod, ClipboardContentType.Image));

            if (imageBytes.Length > 0)
            {
                return Image.FromStream(new MemoryStream(imageBytes));
            }

            return null;
        }

        public static Dictionary<string, object> GetSettings(IExecuteMethod executeMethod) =>
            (Dictionary<string, object>)executeMethod.Execute(AppiumDriverCommand.GetSettings).Value;

        public static void SetSetting(IExecuteMethod executeMethod, string setting, object value)
        {
            var settings = new Dictionary<string, object>()
                { [setting] = value };
            var parameters = new Dictionary<string, object>()
                { ["settings"] = settings };
            executeMethod.Execute(AppiumDriverCommand.UpdateSettings, parameters);
        }
    }
}