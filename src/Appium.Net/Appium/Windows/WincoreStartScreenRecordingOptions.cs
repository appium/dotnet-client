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
using OpenQA.Selenium.Appium.ScreenRecording;

namespace OpenQA.Selenium.Appium.Windows
{
    public class WincoreStartScreenRecordingOptions : BaseStartScreenRecordingOptions<WincoreStartScreenRecordingOptions>
    {
        public static WincoreStartScreenRecordingOptions GetWincoreStartScreenRecordingOptions()
        {
            return new WincoreStartScreenRecordingOptions();
        }

        public static WincoreStartScreenRecordingOptions StartScreenRecordingOptions()
        {
            return new WincoreStartScreenRecordingOptions();
        }

        /// <summary>
        /// The rate of frames per second in the recorded video.
        /// Defaults to 15.
        /// </summary>
        /// <param name="videoFps">Frames per second rate.</param>
        /// <returns>self instance for chaining.</returns>
        public WincoreStartScreenRecordingOptions WithVideoFps(int videoFps)
        {
            Parameters["videoFps"] = videoFps;
            return this;
        }

        /// <summary>
        /// The rate of frames per second in the recorded video.
        /// Defaults to 15.
        /// </summary>
        /// <param name="fps">Frames per second rate.</param>
        /// <returns>self instance for chaining.</returns>
        public WincoreStartScreenRecordingOptions WithFps(int fps)
        {
            return WithVideoFps(fps);
        }

        /// <summary>
        /// Whether to capture the mouse cursor while recording the screen.
        /// Disabled by default.
        /// </summary>
        /// <returns>self instance for chaining.</returns>
        public WincoreStartScreenRecordingOptions EnableCursorCapture()
        {
            Parameters["captureCursor"] = true;
            return this;
        }

        /// <summary>
        /// Whether to capture mouse clicks while recording the screen.
        /// Disabled by default.
        /// </summary>
        /// <returns>self instance for chaining.</returns>
        public WincoreStartScreenRecordingOptions EnableClicksCapture()
        {
            Parameters["captureClicks"] = true;
            return this;
        }

        /// <summary>
        /// DirectShow audio input device name to use for audio capture.
        /// Run <c>ffmpeg -list_devices true -f dshow -i dummy</c> to list available audio devices.
        /// No audio is recorded by default.
        /// </summary>
        /// <param name="audioInput">The DirectShow audio device name.</param>
        /// <returns>self instance for chaining.</returns>
        public WincoreStartScreenRecordingOptions WithAudioInput(string audioInput)
        {
            Parameters["audioInput"] = audioInput;
            return this;
        }

        /// <summary>
        /// The video filter spec to apply for ffmpeg.
        /// See https://trac.ffmpeg.org/wiki/FilteringGuide for more details on the syntax.
        /// Example: Set it to <c>scale=ifnot(gte(iw\,1024)\,iw\,1024):-2</c> in order to constrain
        /// the video width to 1024px. The height will be adjusted automatically to match the actual
        /// media aspect ratio.
        /// </summary>
        /// <param name="videoFilter">Valid ffmpeg video filter spec string.</param>
        /// <returns>self instance for chaining.</returns>
        public WincoreStartScreenRecordingOptions WithVideoFilter(string videoFilter)
        {
            Parameters["videoFilter"] = videoFilter;
            return this;
        }

        /// <summary>
        /// A preset is a collection of options that will provide a certain encoding speed
        /// to compression ratio. A slower preset will provide better compression (compression is
        /// quality per filesize). This means that, for example, if you target a certain file size
        /// or constant bit rate, you will achieve better quality with a slower preset.
        /// Read https://trac.ffmpeg.org/wiki/Encode/H.264 for more details.
        /// One of the supported encoding presets. Possible values are:
        /// - ultrafast
        /// - superfast
        /// - veryfast (default)
        /// - faster
        /// - fast
        /// - medium
        /// - slow
        /// - slower
        /// - veryslow
        /// </summary>
        /// <param name="preset">The preset value.</param>
        /// <returns>self instance for chaining.</returns>
        public WincoreStartScreenRecordingOptions WithPreset(string preset)
        {
            Parameters["preset"] = preset;
            return this;
        }

        /// <summary>
        /// The maximum recording time. The default value is 600 seconds (10 minutes).
        /// The minimum time resolution unit is one second.
        /// </summary>
        /// <param name="timeLimit">The actual time limit of the recorded video.</param>
        /// <returns>self instance for chaining.</returns>
        public new WincoreStartScreenRecordingOptions WithTimeLimit(TimeSpan timeLimit)
        {
            return base.WithTimeLimit(timeLimit);
        }
    }
}
