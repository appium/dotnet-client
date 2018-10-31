using System.Drawing;
using OpenQA.Selenium.Appium.Enums;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface IHasClipboard
    {
        #region base methods

        /// <summary>
        /// Sets the content to the clipboard
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="base64Content"></param>
        void SetClipboard(ClipboardContentType contentType, string base64Content);
    
        /// <summary>
        /// Get the content of the clipboard.
        /// </summary>
        /// <param name="contentType"></param>
        /// <remarks>Android supports plaintext only</remarks>
        /// <returns>The content of the clipboard as base64-encoded string or an empty string if the clipboard is empty</returns>
        string GetClipboard(ClipboardContentType contentType);

        #endregion

        /// <summary>
        /// Sets text to the clipboard
        /// </summary>
        /// <param name="textContent"></param>
        /// <param name="label">For Android only - A user visible label for the clipboard content.</param>
        void SetClipboardText(string textContent, string label);

        /// <summary>
        /// Get the plaintext content of the clipboard.
        /// </summary>
        /// <remarks>Android supports plaintext only</remarks>
        /// <returns>The string content of the clipboard or an empty string if the clipboard is empty</returns>
        string GetClipboardText();

        /// <summary>
        /// Sets the url string to the clipboard
        /// </summary>
        /// <param name="url"></param>
        void SetClipboardUrl(string url);

        /// <summary>
        /// Gets the url string from the clipboard
        /// </summary>
        /// <returns>The url string content of the clipboard or an empty string if the clipboard is empty</returns>
        string GetClipboardUrl();

        /// <summary>
        /// Sets the image to the clipboard
        /// </summary>
        /// <param name="image"></param>
        void SetClipboardImage(Image image);

        /// <summary>
        /// Gets the image from the clipboard
        /// </summary>
        /// <returns>The image content of the clipboard as base64-encoded string or an empty string the clipboard is empty</returns>
        Image GetClipboardImage();



    }
}
