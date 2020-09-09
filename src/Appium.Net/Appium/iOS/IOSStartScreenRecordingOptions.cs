using System;
using OpenQA.Selenium.Appium.ScreenRecording;

namespace OpenQA.Selenium.Appium.iOS
{
    public class IOSStartScreenRecordingOptions : BaseStartScreenRecordingOptions<IOSStartScreenRecordingOptions>
    {
        public static IOSStartScreenRecordingOptions GetIosStartScreenRecordingOptions()
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
            Parameters["videoType"] = videoType.ToString().ToLower();
            return this;
        }

        /// <summary>
        /// The format of the screen capture to be recorded.
        /// Available formats: Execute <code>ffmpeg -codecs</code> in the terminal to see the list of supported video codecs.
        /// ‘mjpeg’ by default. (Since Appium 1.10.0)
        /// </summary>
        /// <param name="videoType">one of available format names.</param>
        /// <returns>self instance for chaining.</returns>
        public IOSStartScreenRecordingOptions WithVideoType(string videoType)
        {
            Parameters["videoType"] = videoType;
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
            Parameters["videoQuality"] = videoQuality.ToString().ToLower();
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

        /// <summary>
        /// The Frames Per Second rate of the recorded video.
        /// Change this value if the resulting video is too slow or too fast.
        /// Defaults to 10. This can decrease the resulting file size.
        /// </summary>
        /// <param name="videoFps"></param>
        /// <returns>self instance for chaining.</returns>
        public IOSStartScreenRecordingOptions WithVideoFps(string videoFps)
        {
            Parameters["videoFps"] = videoFps;
            return this;
        }

        /// <summary>
        /// The scaling value to apply.
        /// Read <see href="https://trac.ffmpeg.org/wiki/Scaling">here</see> for possible values.
        /// Example value of 720p scaling is '1280:720'. This can decrease/increase the resulting file size.
        /// No scale is applied by default.
        /// </summary>
        /// <param name="videoScale"></param>
        /// <returns>self instance for chaining.</returns>
        public IOSStartScreenRecordingOptions WithVideoScale(string videoScale)
        {
            Parameters["videoScale"] = videoScale;
            return this;
        }

        /// <summary>
        /// Output pixel format.
        /// Run <code>ffmpeg -pix_fmts</code> to list possible values.
        /// For QuickTime compatibility, set to “yuv420p” along with videoType: “libx264”.
        /// Supported since Appium 1.12.0)
        /// </summary>
        /// <param name="pixelFormat"></param>
        /// <returns></returns>
        public IOSStartScreenRecordingOptions WithPixelFormat(string pixelFormat)
        {
            Parameters["pixelFormat"] = pixelFormat;
            return this;
        }

        /// <summary>
        /// The ffmpeg video filters to apply.
        /// These filters allow to scale, flip, rotate and do many other useful transformations on the source video stream.
        /// The format of the property must comply with listed <see href="https://ffmpeg.org/ffmpeg-filters.html">here</see>
        /// Supported since Appium 1.15)
        /// </summary>
        /// <param name="videoFilters"></param>
        /// <returns></returns>
        public IOSStartScreenRecordingOptions WithVideoFilter(string videoFilters)
        {
            Parameters["videoFilters"] = videoFilters;
            return this;
        }
    }
}
