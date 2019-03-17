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

namespace OpenQA.Selenium.Appium.Interactions
{
    /// <summary>
    /// Provides a method by which optional attributes can be added to an Interactions.
    /// </summary>
    public interface IInteractionInfo
    {
        /// <summary>
        /// Returns optional values that are set to extend the default interaction values.
        /// </summary>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> representing the values that are set.</returns>
        Dictionary<string, object> ToDictionary();
    }

    /// <summary>
    /// Represents a collection of optional attributes for pen pointer interaction.
    /// </summary>
    public class PenInfo : IInteractionInfo
    {
        /// <summary>
        /// The normalized pressure of the pointer input in the range of [0,1]
        /// </summary>
        public float? Pressure { get; set; }

        /// <summary>
        /// The clockwise rotation (in degrees, in the range of [0,359]) of a transducer (e.g. pen stylus) around its own major axis
        /// </summary>
        public int? Twist { get; set; }

        /// <summary>
        /// The plane angle (in degrees, in the range of [-90,90]) between the Y-Z plane and the plane containing both the transducer (e.g. pen stylus) axis and the Y axis
        /// </summary>
        public int? TiltX { get; set; }

        /// <summary>
        /// The plane angle (in degrees, in the range of [-90,90]) between the X-Z plane and the plane containing both the transducer (e.g. pen stylus) axis and the X axis
        /// </summary>
        public int? TiltY { get; set; }

        /// <summary>
        /// Returns optional values that are set to extend the default pen pointer interaction values.
        /// </summary>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> representing the values that are set.</returns>
        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();

            if (Pressure.HasValue)
            {
                toReturn["pressure"] = Pressure;
            }
            if (Twist.HasValue)
            {
                toReturn["twist"] = Twist;
            }
            if (TiltX.HasValue)
            {
                toReturn["tiltX"] = TiltX;
            }
            if (TiltY.HasValue)
            {
                toReturn["tiltY"] = TiltY;
            }

            return toReturn;
        }
    }

    /// <summary>
    /// Represents a collection of optional attributes for touch pointer interaction.
    /// </summary>
    public class TouchInfo : IInteractionInfo
    {
        /// <summary>
        /// The normalized pressure of the pointer input in the range of [0,1]
        /// </summary>
        public float? Pressure { get; set; }

        /// <summary>
        /// The clockwise rotation (in degrees, in the range of [0,359]) of a transducer (e.g. pen stylus) around its own major axis
        /// </summary>
        public int? Twist { get; set; }

        /// <summary>
        /// The width (magnitude on the X axis), in pixels of the contact geometry of the pointer
        /// </summary>
        public double? Width { get; set; }

        /// <summary>
        /// The height (magnitude on the Y axis), in pixels of the contact geometry of the pointer
        /// </summary>
        public double? Height { get; set; }

        /// <summary>
        /// Returns optional values that are set to extend the default touch pointer interaction values.
        /// </summary>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> representing the values that are set.</returns>
        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();

            if (Pressure.HasValue)
            {
                toReturn["pressure"] = Pressure;
            }
            if (Twist.HasValue)
            {
                toReturn["twist"] = Twist;
            }
            if (Width.HasValue)
            {
                toReturn["width"] = Width;
            }
            if (Height.HasValue)
            {
                toReturn["height"] = Height;
            }

            return toReturn;
        }
    }
}