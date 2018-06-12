using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface ILockDevice : IExecuteMethod
    {
        /// <summary>
        /// This method locks a device. It will return silently if the device is already locked.
        /// Lock the device (bring it to the lock screen) for a given number of seconds
        /// or forever (until the command for unlocking is called). 
        /// The call is ignored if the device has been already locked.
        /// </summary>
        /// <param name="duration"> for how long to lock the screen. Minimum time resolution is one second. 
        /// A negative/zero value will lock the device and return immediately.</param>
        void LockDevice(int duration);

        /// <summary>
        /// Unlock the device if it is locked. 
        /// This method will return silently if the device is locked
        /// </summary>
        void UnlockDevice();

        /// <summary>
        /// Check if the device is locked.
        /// </summary>
        /// <returns>true if the device is locked or false</returns>
        bool IsDeviceLocked();
    }
}
