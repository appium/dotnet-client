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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace OpenQA.Selenium.Appium
{
    public enum VideoQuality
    {
        LOW, MEDIUM, HIGH, PHOTO
    }

    public enum VideoType
    {
        H264, MP4, FMP4
    }

    public class RecordScreenUploadOptions
    {
        /// <summary>
        /// The RemotePath upload option is the path to the remote location,
        /// where the resulting video from the previous screen recording should be uploaded.
        /// The following protocols are supported: http/https (multipart), ftp.
        /// Missing value (the default setting) means the content of the resulting
        /// file should be encoded as Base64 and passed as the endpoint response value, but
        /// an exception will be thrown if the generated media file is too big to
        /// fit into the available process memory.
        /// This option only has an effect if there is/was an active screen recording session
        /// and forced restart is not enabled (the default setting).
        /// </summary>
        [JsonProperty(PropertyName = "remotePath")]
        public string RemotePath;

        /// <summary>
        /// The name of the user for the remote authentication.
        /// Only has an effect if both `remotePath` and `password` are set.
        /// </summary>
        [JsonProperty(PropertyName = "username")]
        public string Username;

        /// <summary>
        /// The password for the remote authentication.
        /// Only has an effect if both `remotePath` and `user` are set.
        /// </summary>
        [JsonProperty(PropertyName = "password")]
        public string Password;

        /// <summary>
        /// The HTTP method name ('PUT'/'POST'). PUT method is used by default.
        /// Only has an effect if `remotePath` is set.
        /// </summary>
        [JsonProperty(PropertyName = "method")]
        public string Method;
    }

    public class RecordScreenBaseOptions : RecordScreenUploadOptions
    {

        /// <summary>
        /// Whether to ignore the result of previous capture and start a new recording
        /// immediately (`True` value). By default  (`False`) the endpoint will try to catch and return the result of
        /// the previous capture if it's still available.
        /// </summary>
        [JsonProperty(PropertyName = "forceRestart")]
        public bool? ForceRestart;

        /// <summary>
        /// The actual time limit of the recorded video in seconds.
        /// The default value for both iOS and Android is 180 seconds (3 minutes).
        /// The maximum value for Android is 3 minutes.
        /// The maximum value for iOS is 10 minutes.
        /// Time resolution unit is one second.
        /// </summary>
        [JsonProperty(PropertyName = "timeLimit")]
        public string TimeLimit;
    }

    /// <summary>
    /// Android specific class that is an extension of BaseRecordScreenOptions <see cref="BaseRecordScreenOptions"/> 
    /// </summary>
    public class AndroidScreenRecordOptions : RecordScreenBaseOptions
    {
        /// <summary>
        /// The video size of the generated media file. The format is WIDTHxHEIGHT.
        /// The default value is the device's native display resolution (if supported),
        /// 1280x720 if not. For best results, use a size supported by your device's
        /// Advanced Video Coding (AVC) encoder.
        /// </summary>
        [JsonProperty(PropertyName = "videoSize")]
        public string VideoSize;

        /// <summary>
        /// Makes the recorder to display an additional information on the video overlay,
        /// such as a timestamp, that is helpful in videos captured to illustrate bugs.
        /// This option is only supported since API level 27 (Android P).
        /// </summary>
        [JsonProperty(PropertyName = "bugReport")]
        public string BugReport;

        /// <summary>
        /// The video bit rate for the video, in megabits per second.
        /// The default value is 4. You can increase the bit rate to improve video quality,
        /// but doing so results in larger movie files.
        /// </summary>
        [JsonProperty(PropertyName = "bitRate")]
        public string BitRate;
    }

    /// <summary>
    /// iOS specific class that is an extension of BaseRecordScreenOptions <see cref="BaseRecordScreenOptions"/> 
    /// </summary>
    /// 
    public class IosRecordScreenOptions : RecordScreenBaseOptions
    {
        /// <summary>
        /// The format of the screen capture to be recorded.
        /// Available formats: 'h264', 'mp4' or 'fmp4'. Default is 'mp4'.
        /// Only works for Simulator.
        /// </summary>
        [JsonProperty(PropertyName = "videoType")]
        public string VideoType { get; }

        /// <summary>
        /// The video encoding quality: 'low', 'medium', 'high', 'photo'. Defaults to 'medium'.
        /// Only works for real devices.
        /// </summary>
        [JsonProperty(PropertyName = "videoQuality")]
        public string VideoQuality { get; }

        public IosRecordScreenOptions(VideoType videoThing)
        {
            VideoType = videoThing.ToString().ToLower();
        }

        public IosRecordScreenOptions(VideoQuality videoQuality)
        {
            VideoQuality = videoQuality.ToString().ToLower();
        }

        public IosRecordScreenOptions(VideoType videoType, VideoQuality videoQuality)
        {
            VideoQuality = videoQuality.ToString().ToLower();
            VideoType = videoType.ToString().ToLower();
        }
    }
}
