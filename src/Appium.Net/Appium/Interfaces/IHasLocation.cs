/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * on-line resources
 */
namespace OpenQA.Selenium.Appium.Interfaces
{
    /// <summary>
    /// Describes a contract by which you can get or set a GPS location on a mobile device.
    /// </summary>
    public interface IHasLocation
    {
        /// <summary>
        /// Gets or sets the GPS location of this mobile device.
        /// </summary>
        Location Location { get; set; }
    }
}
