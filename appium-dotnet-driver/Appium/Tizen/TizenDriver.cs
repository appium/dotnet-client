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

using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace OpenQA.Selenium.Appium.Tizen
{
    public class TizenDriver<W> : AppiumDriver<W>
         where W : IWebElement
    {
        private static readonly string Platform = MobilePlatform.Tizen;

        /// <summary>
        /// Initializes a new instance of the TizenDriver class
        /// </summary>
        /// <param name="commandExecutor">An <see cref="ICommandExecutor"/> object which executes commands for the driver.</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        public TizenDriver(ICommandExecutor commandExecutor, DesiredCapabilities desiredCapabilities)
            : base(commandExecutor, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using desired capabilities
        /// </summary>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities of the browser.</param>
        public TizenDriver(DesiredCapabilities desiredCapabilities)
            : base(SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using desired capabilities and command timeout
        /// </summary>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public TizenDriver(DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the AppiumServiceBuilder instance and desired capabilities
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        public TizenDriver(AppiumServiceBuilder builder, DesiredCapabilities desiredCapabilities)
            : base(builder, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the AppiumServiceBuilder instance, desired capabilities and command timeout
        /// </summary>
        /// <param name="builder"> object containing settings of the Appium local service which is going to be started</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public TizenDriver(AppiumServiceBuilder builder, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(builder, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the specified remote address and desired capabilities
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities.</param>
        public TizenDriver(Uri remoteAddress, DesiredCapabilities desiredCapabilities)
            : base(remoteAddress, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the specified Appium local service and desired capabilities
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities of the browser.</param>
        public TizenDriver(AppiumLocalService service, DesiredCapabilities desiredCapabilities)
            : base(service, SetPlatformToCapabilities(desiredCapabilities, Platform))
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the specified remote address, desired capabilities, and command timeout.
        /// </summary>
        /// <param name="remoteAddress">URI containing the address of the WebDriver remote server (e.g. http://127.0.0.1:4723/wd/hub).</param>
        /// <param name="desiredCapabilities">An <see cref="DesiredCapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public TizenDriver(Uri remoteAddress, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(remoteAddress, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TizenDriver class using the specified Appium local service, desired capabilities, and command timeout.
        /// </summary>
        /// <param name="service">the specified Appium local service</param>
        /// <param name="desiredCapabilities">An <see cref="ICapabilities"/> object containing the desired capabilities.</param>
        /// <param name="commandTimeout">The maximum amount of time to wait for each command.</param>
        public TizenDriver(AppiumLocalService service, DesiredCapabilities desiredCapabilities, TimeSpan commandTimeout)
            : base(service, SetPlatformToCapabilities(desiredCapabilities, Platform), commandTimeout)
        {
        }

        public bool CompareImages(Image firstImage, Image secondImage, out Image resultImage)
        {
            resultImage = null;
            if (firstImage == null || secondImage == null)
            {
                throw new NullReferenceException("Images should not be null");
            }

            if (firstImage.Width != secondImage.Width || firstImage.Height != secondImage.Height)
            {
                return false;
            }

            bool result = true;
            Bitmap firstBitmap = new Bitmap(firstImage);
            Bitmap secondBitmap = new Bitmap(secondImage);
            Bitmap resultBitmap = new Bitmap(secondImage);

            BitmapData firstBitmapData = firstBitmap.LockBits(new Rectangle(0, 0, firstImage.Width, firstImage.Height), ImageLockMode.ReadOnly, firstImage.PixelFormat);
            BitmapData secondBitmapData = secondBitmap.LockBits(new Rectangle(0, 0, secondImage.Width, secondImage.Height), ImageLockMode.ReadOnly, secondImage.PixelFormat);
            BitmapData resultBitmapData = resultBitmap.LockBits(new Rectangle(0, 0, secondImage.Width, secondImage.Height), ImageLockMode.ReadOnly, secondImage.PixelFormat);

            int Depth = Image.GetPixelFormatSize(firstBitmap.PixelFormat);
            int size = firstBitmapData.Stride * firstBitmapData.Height;

            byte[] firstPixels = new byte[size];
            byte[] secondPixels = new byte[size];
            byte[] resultPixels = new byte[size];

            Marshal.Copy(firstBitmapData.Scan0, firstPixels, 0, size);
            Marshal.Copy(secondBitmapData.Scan0, secondPixels, 0, size);
            Marshal.Copy(resultBitmapData.Scan0, resultPixels, 0, size);

            int firstGrayScale = 0;
            int secondGrayScale = 0;
            int diff = 0;

            for (int i = 0; i < size; i += Depth / 8)
            {
                firstGrayScale = (int)((firstPixels[i] * 0.11) + (firstPixels[i + 1] * 0.59) + (firstPixels[i + 2] * 0.3));
                secondGrayScale = (int)((secondPixels[i] * 0.11) + (secondPixels[i + 1] * 0.59) + (secondPixels[i + 2] * 0.3));

                diff = firstGrayScale - secondGrayScale;

                if (Math.Abs(diff) > 3)
                {
                    if (resultPixels[i + 2] > 200)
                    {
                        resultPixels[i] = 255;
                        resultPixels[i + 1] = 0;
                        resultPixels[i + 2] = 0;
                    }
                    else
                    {
                        resultPixels[i] = 0;
                        resultPixels[i + 1] = 0;
                        resultPixels[i + 2] = 255;
                    }

                    result = false;
                }
            }

            Marshal.Copy(firstPixels, 0, firstBitmapData.Scan0, firstPixels.Length);
            Marshal.Copy(secondPixels, 0, secondBitmapData.Scan0, secondPixels.Length);
            Marshal.Copy(resultPixels, 0, resultBitmapData.Scan0, secondPixels.Length);

            firstBitmap.UnlockBits(firstBitmapData);
            secondBitmap.UnlockBits(secondBitmapData);
            resultBitmap.UnlockBits(resultBitmapData);

            if (!result)
            {
                resultImage = Image.FromHbitmap(resultBitmap.GetHbitmap());
            }

            return result;
        }

        public Image TransformScreenshot(Screenshot screenshot, Rectangle rectangle)
        {
            if (screenshot == null)
            {
                throw new NullReferenceException("Screenshot should not be null");
            }

            MemoryStream imageStream = new MemoryStream(screenshot.AsByteArray);
            Image screenshotImage = Image.FromStream(imageStream);

            Bitmap bmpImage = new Bitmap(screenshotImage);
            return bmpImage.Clone(rectangle, bmpImage.PixelFormat);
        }

        protected override RemoteWebElement CreateElement(string elementId) => new TizenElement(this, elementId);
    }
}
