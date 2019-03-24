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

using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenQA.Selenium.Appium.Interactions
{
    /// <summary>
    /// Specifies the button used during a pointer down or up action.
    /// </summary>
    public enum PointerButton
    {
        /// <summary>
        /// Neither buttons nor touch/pen contact changed since last event
        /// </summary>
        None = -1,

        /// <summary>
        /// Mouse left button
        /// </summary>
        LeftMouse = 0,

        /// <summary>
        /// Default touch contact
        /// </summary>
        TouchContact = 0,

        /// <summary>
        /// The pen tip
        /// </summary>
        PenContact = 0,

        /// <summary>
        /// Mouse middle button
        /// </summary>
        MiddleMouse = 1,

        /// <summary>
        /// Mouse right button
        /// </summary>
        RightMouse = 2,

        /// <summary>
        /// Pen barrel button
        /// </summary>
        PenBarrel = 2,

        /// <summary>
        /// Mouse X1 (back) button
        /// </summary>
        X1Mouse = 3,

        /// <summary>
        /// Mouse X1 (forward) button
        /// </summary>
        X2Mouse = 4,

        /// <summary>
        /// The pen eraser button
        /// </summary>
        PenEraser = 5
    }

    /// <summary>
    /// Represents a pointer input device such as pen, touch, and mouse. This class extends
    /// OpenQA.Selenium.Interactions.PointerUInputDevice to add optional interaction values
    /// such as pressure, rotation, tilt angle, etc.
    /// </summary>
    public class PointerInputDevice : Selenium.Interactions.PointerInputDevice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointerInputDevice"/> class.
        /// </summary>
        /// <param name="pointerKind">The kind of pointer represented by this input device.</param>
        public PointerInputDevice(PointerKind pointerKind)
            : base(pointerKind)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PointerInputDevice"/> class.
        /// </summary>
        /// <param name="pointerKind">The kind of pointer represented by this input device.</param>
        /// <param name="deviceName">The unique name for this input device.</param>
        public PointerInputDevice(PointerKind pointerKind, string deviceName)
            : base(pointerKind, deviceName)
        {
        }

        /// <summary>
        /// Creates a pointer down action.
        /// </summary>
        /// <param name="button">The button of the pointer that should be pressed.</param>
        /// <returns>The action representing the pointer down gesture.</returns>
        public Interaction CreatePointerDown(PointerButton button)
        {
            return CreatePointerDown((MouseButton)button);
        }

        /// <summary>
        /// Creates a pointer up action.
        /// </summary>
        /// <param name="button">The button of the pointer that should be released.</param>
        /// <returns>The action representing the pointer up gesture.</returns>
        public Interaction CreatePointerUp(PointerButton button)
        {

            return CreatePointerUp((MouseButton)button);
        }

        /// <summary>
        /// Creates a pointer down action.
        /// </summary>
        /// <param name="button">The button of the pointer that should be pressed.</param>
        /// <param name="pointerExtraAttributes">Additional pointer attributes.</param>
        /// <returns>The action representing the pointer down gesture.</returns>
        public Interaction CreatePointerDown(PointerButton button, IInteractionInfo pointerExtraAttributes)
        {
            return new ExtendedPointerInteraction(CreatePointerDown((MouseButton)button), pointerExtraAttributes);
        }

        /// <summary>
        /// Creates a pointer up action.
        /// </summary>
        /// <param name="button">The button of the pointer that should be released.</param>
        /// <param name="pointerExtraAttributes">Additional pointer attributes.</param>
        /// <returns>The action representing the pointer up gesture.</returns>
        public Interaction CreatePointerUp(PointerButton button, IInteractionInfo pointerExtraAttributes)
        {
            return new ExtendedPointerInteraction(CreatePointerUp((MouseButton)button), pointerExtraAttributes);
        }

        /// <summary>
        /// Creates a pointer move action to a specific element.
        /// </summary>
        /// <param name="target">The <see cref="IWebElement"/> used as the target for the move.</param>
        /// <param name="xOffset">The horizontal offset from the origin of the move.</param>
        /// <param name="yOffset">The vertical offset from the origin of the move.</param>
        /// <param name="duration">The length of time the move gesture takes to complete.</param>
        /// <param name="pointerExtraAttributes">Additional pointer attributes.</param>
        /// <returns>The action representing the pointer move gesture.</returns>
        public Interaction CreatePointerMove(IWebElement target, int xOffset, int yOffset, TimeSpan duration, IInteractionInfo pointerExtraAttributes)
        {
            return new ExtendedPointerInteraction(CreatePointerMove(target, xOffset, yOffset, duration), pointerExtraAttributes);
        }

        /// <summary>
        /// Creates a pointer move action to an absolute coordinate.
        /// </summary>
        /// <param name="origin">The origin of coordinates for the move. Values can be relative to
        /// the view port origin, or the most recent pointer position.</param>
        /// <param name="xOffset">The horizontal offset from the origin of the move.</param>
        /// <param name="yOffset">The vertical offset from the origin of the move.</param>
        /// <param name="duration">The length of time the move gesture takes to complete.</param>
        /// <param name="pointerExtraAttributes">Additional pointer attributes.</param>
        /// <returns>The action representing the pointer move gesture.</returns>
        /// <exception cref="ArgumentException">Thrown when passing CoordinateOrigin.Element into origin.
        /// Users should use the other CreatePointerMove overload to move to a specific element.</exception>
        public Interaction CreatePointerMove(CoordinateOrigin origin, int xOffset, int yOffset, TimeSpan duration, IInteractionInfo pointerExtraAttributes)
        {
            return new ExtendedPointerInteraction(CreatePointerMove(origin, xOffset, yOffset, duration), pointerExtraAttributes);
        }

        // This class appended the original Interaction defined in OpenQA.Selenium.Interaction with additional
        // extra attributes of the pointer interaction. This can be removed when the standard interaction objects
        // in OpenQA.Selenium.Interaction namespace are enhanced to support these additional attributes.
        private class ExtendedPointerInteraction : Interaction
        {
            private Interaction wrappedInteraction;
            private Dictionary<string, object> pointerInputExtraAttributes;

            public ExtendedPointerInteraction(Interaction pointerInteraction, IInteractionInfo pointerAttributes)
                : base(pointerInteraction.SourceDevice)
            {
                if (pointerInteraction == null)
                {
                    throw new ArgumentException("Pointer interaction provided is invalid");
                }

                if (pointerAttributes == null)
                {
                    throw new ArgumentException("Pointer attributes provided is invalid");
                }

                wrappedInteraction = pointerInteraction;
                pointerInputExtraAttributes = pointerAttributes.ToDictionary();
            }

            public override Dictionary<string, object> ToDictionary()
            {
                // Get the original payload from the wrapped Interaction
                Dictionary<string, object> toReturn = wrappedInteraction.ToDictionary();

                // Append the original payload with the given pointer input extra attributes
                pointerInputExtraAttributes.ToList().ForEach(x => toReturn[x.Key] = x.Value);

                return toReturn;
            }
        }
    }
}