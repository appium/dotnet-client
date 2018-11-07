using System;
using OpenQA.Selenium.Appium.ScreenRecording;

namespace OpenQA.Selenium.Appium.Android
{
    public class AndroidStartScreenRecordingOptions : BaseStartScreenRecordingOptions<AndroidStartScreenRecordingOptions>
    {
        public static AndroidStartScreenRecordingOptions GetAndroidStartScreenRecordingOptions()
        {
            return new AndroidStartScreenRecordingOptions();
        }

        /// <summary>
        /// The video bit rate for the video, in megabits per second.
        /// The default value is 4000000 (4 Mb/s) for Android API level below 27
        /// and 20000000 (20 Mb/s) for API level 27 and above.
        /// You can increase the bit rate to improve video quality,
        /// but doing so results in larger movie files.
        /// </summary>
        /// <param name="bitRate">The actual bit rate (Mb/s).</param>
        /// <returns>self instance for chaining.</returns>
        public AndroidStartScreenRecordingOptions WithBitRate(int bitRate)
        {
            Parameters["bitRate"] = bitRate;
            return this;
        }

        /// <summary>
        /// The video size of the generated media file. The format is WIDTHxHEIGHT.
        /// The default value is the device's native display resolution (if supported),
        /// 1280x720 if not. For best results,
        /// use a size supported by your device's Advanced Video Coding (AVC) encoder.
        /// </summary>
        /// <param name="videoSize">The actual video size: WIDTHxHEIGHT.</param>
        /// <returns>self instance for chaining.</returns>
        public AndroidStartScreenRecordingOptions WithVideoSize(string videoSize)
        {
            Parameters["videoSize"] = videoSize;
            return this;
        }

        /// <summary>
        /// Makes the recorder to display an additional information on the video overlay,
        /// such as a timestamp, that is helpful in videos captured to illustrate bugs.
        /// This option is only supported since API level 27 (Android P).
        /// </summary>
        /// <returns>self instance for chaining.</returns>
        public AndroidStartScreenRecordingOptions EnableBugReport()
        {
            Parameters["isBugReportEnabled"] = true;
            return this;
        }

        /// <summary>
        /// The maximum recording time.The default and maximum value is 180 seconds (3 minutes).
        /// Setting values greater than this or less than zero will cause an exception. The minimum
        /// time resolution unit is one second.
        ///
        /// Since Appium 1.8.2 the time limit can be up to 1800 seconds (30 minutes).
        /// Appium will automatically try to merge the 3-minutes chunks recorded
        /// by the screenrecord utility, however, this requires FFMPEG utility
        /// to be installed and available in PATH on the server machine. If the utility is not
        /// present then the most recent screen recording chunk is going to be returned as the result.
        /// </summary>
        /// <param name="timeLimit">The actual time limit of the recorded video.</param>
        /// <returns>self instance for chaining.</returns>
        public new AndroidStartScreenRecordingOptions WithTimeLimit(TimeSpan timeLimit)
        {
            return base.WithTimeLimit(timeLimit);
        }
    }
}
