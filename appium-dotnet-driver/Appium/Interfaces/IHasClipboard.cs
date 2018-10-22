using OpenQA.Selenium.Appium.Enums;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface IHasClipboard
    {
        /// <summary>
        /// Sets the content to the clipboard
        /// </summary>
        /// <param name="base64Content"></param>
        /// <param name="contentType"></param>
        /// <param name="label">For Android only - A user visible label for the clipboard content.</param>
        void SetClipboard(byte[] base64Content, ClipboardContentType contentType, string label);

        /// <summary>
        /// Sets text to the clipboard
        /// </summary>
        /// <param name="textContent"></param>
        void SetClipboardText(string textContent);

        /// <summary>
        /// Get the content of the clipboard.
        /// </summary>
        /// <param name="contentType"></param>
        /// <remarks>Android supports plaintext only</remarks>
        /// <returns>The actual content of the clipboard as base64-encoded string or an empty string if the clipboard is empty</returns>
        string GetClipboard(ClipboardContentType contentType);

    }
}
