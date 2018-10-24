using System;
using OpenQA.Selenium.Appium.ScreenRecording;

namespace OpenQA.Selenium.Appium.iOS
{
    public class IOSStartScreenRecordingOptions : BaseStartScreenRecordingOptions<IOSStartScreenRecordingOptions>
    {
        public static IOSStartScreenRecordingOptions StartScreenRecordingOptions()
        {
            return new IOSStartScreenRecordingOptions();
        }

        public enum VideoType
        {
            H264, MP4, FMP4
        }

        /// <summary>
        /// The format of the screen capture to be recorded.
        /// Available formats: "h264", "mp4" or "fmp4". Default is "mp4".
        /// Only works for Simulator.
        /// </summary>
        /// <param name="videoType">one of available format names.</param>
        /// <returns>self instance for chaining.</returns>
        public IOSStartScreenRecordingOptions WithVideoType(VideoType videoType)
        {
            Parameters["videoType"] = videoType.ToString();
            return this;
        }

        public enum VideoQuality
        {
            LOW, MEDIUM, HIGH, PHOTO
        }

        /// <summary>
        /// The video encoding quality (low, medium, high, photo - defaults to medium).
        /// Only works for real devices.
        /// </summary>
        /// <param name="videoQuality"></param>
        /// <returns></returns>
        public IOSStartScreenRecordingOptions WithVideoQuality(VideoQuality videoQuality)
        {
            Parameters["videoQuality"] = videoQuality.ToString();
            return this;
        }

        /// <summary>
        /// The maximum recording time.The default value is 180 seconds (3 minutes).
        /// The maximum value is 10 minutes.
        /// Setting values greater than this or less than zero will cause an exception. The minimum
        /// time resolution unit is one second.
        /// </summary>
        /// <param name="timeLimit">The actual time limit of the recorded video.</param>
        /// <returns>self instance for chaining.</returns>
        public new IOSStartScreenRecordingOptions WithTimeLimit(TimeSpan timeLimit)
        {
            return base.WithTimeLimit(timeLimit);
        }
    }
}
