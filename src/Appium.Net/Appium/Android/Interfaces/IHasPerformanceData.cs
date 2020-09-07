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
using OpenQA.Selenium.Appium.Android.Enums;

namespace OpenQA.Selenium.Appium.Android.Interfaces
{
    public interface IHasPerformanceData
    {
        /// <summary>
        /// Returns the information of the system state which is supported to read e.g. cpu, memory, network
        /// </summary>
        /// <param name="packageName">The package name of the application</param>
        /// <param name="performanceDataType">The type of system state which you want to read, use <see cref="PerformanceDataType"/>
        /// or call <see cref="GetPerformanceDataTypes"/> to find a list of the supported performance data types.</param>
        /// <returns>
        /// A table like list of the performance data, The first index of the table represents the type of data.
        /// The remaining index represent the values of the data.
        ///        in case of battery info : [[power], [23]]
        ///        in case of memory info :
        ///             [[totalPrivateDirty, nativePrivateDirty, dalvikPrivateDirty, eglPrivateDirty, glPrivateDirty,
        ///                       totalPss, nativePss, dalvikPss, eglPss, glPss, nativeHeapAllocatedSize, nativeHeapSize],
        ///                      [18360, 8296, 6132, null, null, 42588, 8406, 7024, null, null, 26519, 10344]]
        ///        in case of network info :
        ///            [[bucketStart, activeTime, rxBytes, rxPackets, txBytes, txPackets, operations, bucketDuration,],
        ///                      [1478091600000, null, 1099075, 610947, 928, 114362, 769, 0, 3600000],
        ///                      [1478095200000, null, 1306300, 405997, 509, 46359, 370, 0, 3600000]]
        ///        in case of network info :
        ///            [[st, activeTime, rb, rp, tb, tp, op, bucketDuration],
        ///                       [1478088000, null, null, 32115296, 34291, 2956805, 25705, 0, 3600],
        ///                      [1478091600, null, null, 2714683, 11821, 1420564, 12650, 0, 3600],
        ///                      [1478095200, null, null, 10079213, 19962, 2487705, 20015, 0, 3600],
        ///                      [1478098800, null, null, 4444433, 10227, 1430356, 10493, 0, 3600]]
        ///        in case of cpu info : [[user, kernel], [0.9, 1.3]]
        /// </returns>
        IList<object> GetPerformanceData(string packageName, string performanceDataType);


        /// <summary>
        /// Returns the information of the system state which is supported to read e.g. cpu, memory, network
        /// </summary>
        /// <param name="packageName">The package name of the application</param>
        /// <param name="performanceDataType">The type of system state which you want to read, use <see cref="PerformanceDataType"/>
        /// or call <see cref="GetPerformanceDataTypes"/> to find a list of the supported performance data types.</param>
        /// <param name="dataReadAttempts">The number of attempts to read data in the event of a data read failure (optional). Value must be greater than 0.</param>
        /// <returns>
        /// A table like list of the performance data, The first index of the table represents the type of data.
        /// The remaining index represent the values of the data.
        ///        in case of battery info : [[power], [23]]
        ///        in case of memory info :
        ///             [[totalPrivateDirty, nativePrivateDirty, dalvikPrivateDirty, eglPrivateDirty, glPrivateDirty,
        ///                       totalPss, nativePss, dalvikPss, eglPss, glPss, nativeHeapAllocatedSize, nativeHeapSize],
        ///                      [18360, 8296, 6132, null, null, 42588, 8406, 7024, null, null, 26519, 10344]]
        ///        in case of network info :
        ///            [[bucketStart, activeTime, rxBytes, rxPackets, txBytes, txPackets, operations, bucketDuration,],
        ///                      [1478091600000, null, 1099075, 610947, 928, 114362, 769, 0, 3600000],
        ///                      [1478095200000, null, 1306300, 405997, 509, 46359, 370, 0, 3600000]]
        ///        in case of network info :
        ///            [[st, activeTime, rb, rp, tb, tp, op, bucketDuration],
        ///                       [1478088000, null, null, 32115296, 34291, 2956805, 25705, 0, 3600],
        ///                      [1478091600, null, null, 2714683, 11821, 1420564, 12650, 0, 3600],
        ///                      [1478095200, null, null, 10079213, 19962, 2487705, 20015, 0, 3600],
        ///                      [1478098800, null, null, 4444433, 10227, 1430356, 10493, 0, 3600]]
        ///        in case of cpu info : [[user, kernel], [0.9, 1.3]]
        /// </returns>
        IList<object> GetPerformanceData(string packageName, string performanceDataType, int dataReadAttempts);
        
        /// <summary>
        /// Returns the information types of the system state which is supported to read as like cpu, memory, network traffic, and battery
        /// </summary>
        IList<string> GetPerformanceDataTypes();
    }
}
