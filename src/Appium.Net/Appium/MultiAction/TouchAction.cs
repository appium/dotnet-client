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

using OpenQA.Selenium.Appium.Interfaces;
using System.Collections.Generic;
using System.Reflection;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Internal;

namespace OpenQA.Selenium.Appium.MultiTouch
{
    public class TouchAction : ITouchAction
    {
        internal class Step
        {
            private Dictionary<string, object> parameters = new Dictionary<string, object>();

            private string GetIdForElement(IWebElement el)
            {
                RemoteWebElement remoteWebElement = el as RemoteWebElement;
                if (remoteWebElement != null)
                    return (string) typeof(OpenQA.Selenium.Remote.RemoteWebElement).GetProperty("Id",
                        BindingFlags.NonPublic |
                        BindingFlags.Instance).GetValue(el, null);

                IWrapsElement elementWrapper = el as IWrapsElement;
                if (elementWrapper != null)
                    return GetIdForElement(elementWrapper.WrappedElement);

                return null;
            }

            public Step(string action)
            {
                parameters.Add("action", action);
            }

            public Step AddOpt(string name, object value)
            {
                if (value != null)
                {
                    if (!parameters.ContainsKey("options")) parameters.Add("options", new Dictionary<string, object>());
                    if (value is IWebElement)
                    {
                        string id = GetIdForElement((IWebElement) value);
                        ((Dictionary<string, object>) this.parameters["options"]).Add(name, id);
                    }
                    else if (value is double)
                    {
                        double doubleValue = (double) value;
                        if (doubleValue == (int) doubleValue)
                        {
                            ((Dictionary<string, object>) parameters["options"])
                                .Add(name, (int) doubleValue);
                        }
                        else
                        {
                            ((Dictionary<string, object>) parameters["options"])
                                .Add(name, doubleValue);
                        }
                    }
                    else
                    {
                        ((Dictionary<string, object>) parameters["options"]).Add(name, value);
                    }
                }
                return this;
            }

            public Dictionary<string, object> GetParameters()
            {
                return parameters;
            }
        }

        private IPerformsTouchActions TouchActionPerformer;
        private List<Step> steps = new List<Step>();


        public TouchAction(IPerformsTouchActions touchActionPerformer)
        {
            this.TouchActionPerformer = touchActionPerformer;
        }

        /// <summary>
        /// Press at the specified location in the element until the  context menu appears.
        /// </summary>
        /// <param name="element">The target element.</param>
        /// <param name=x>The x coordinate relative to the element.</param>
        /// <param name=y>The y coordinate relative to the element.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        public ITouchAction LongPress(IWebElement element, double? x = null, double? y = null)
        {
            Step longPressStep = new Step("longPress");
            longPressStep
                .AddOpt("element", element)
                .AddOpt("x", x)
                .AddOpt("y", y);
            steps.Add(longPressStep);
            return this;
        }

        /// <summary>
        /// Press at the specified location in the element until the  context menu appears.
        /// </summary>
        /// <param name="element">The target element.</param>
        /// <param name=x>The x coordinate relative to the element.</param>
        /// <param name=y>The y coordinate relative to the element.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        public ITouchAction LongPress(double x, double y)
        {
            Step longPressStep = new Step("longPress");
            longPressStep
                .AddOpt("x", x)
                .AddOpt("y", y);
            steps.Add(longPressStep);
            return this;
        }

        /// <summary>
        /// Move to the specified location in the element.
        /// </summary>
        /// <param name="element">The target element.</param>
        /// <param name=x>The x coordinate relative to the element.</param>
        /// <param name=y>The y coordinate relative to the element.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        public ITouchAction MoveTo(IWebElement element, double? x = null, double? y = null)
        {
            Step moveToStep = new Step("moveTo");
            moveToStep
                .AddOpt("element", element)
                .AddOpt("x", x)
                .AddOpt("y", y);
            steps.Add(moveToStep);
            return this;
        }

        /// <summary>
        /// Move to the specified location.
        /// </summary>
        /// <param name=x>The x coordinate.</param>
        /// <param name=y>The y coordinate.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        public ITouchAction MoveTo(double x, double y)
        {
            Step moveToStep = new Step("moveTo");
            moveToStep
                .AddOpt("x", x)
                .AddOpt("y", y);
            steps.Add(moveToStep);
            return this;
        }

        /// <summary>
        /// Press at the specified location in the element.
        /// </summary>
        /// <param name="element">The target element.</param>
        /// <param name=x>The x coordinate relative to the element.</param>
        /// <param name=y>The y coordinate relative to the element.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        public ITouchAction Press(IWebElement element, double? x = null, double? y = null)
        {
            Step pressStep = new Step("press");
            pressStep
                .AddOpt("element", element)
                .AddOpt("x", x)
                .AddOpt("y", y);
            steps.Add(pressStep);
            return this;
        }

        /// <summary>
        /// Press at the specified location.
        /// </summary>
        /// <param name=x>The x coordinate.</param>
        /// <param name=y>The y coordinate.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        public ITouchAction Press(double x, double y)
        {
            Step pressStep = new Step("press");
            pressStep
                .AddOpt("x", x)
                .AddOpt("y", y);
            steps.Add(pressStep);
            return this;
        }

        /// <summary>
        /// Release the pressure.
        /// </summary>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        public ITouchAction Release()
        {
            Step releaseStep = new Step("release");
            steps.Add(releaseStep);
            return this;
        }

        /// <summary>
        /// Tap at the specified location in the element.
        /// </summary>
        /// <param name="element">The target element.</param>
        /// <param name="x">The x coordinate relative to the element.</param>
        /// <param name="y">The y coordinate relative to the element.</param>
        /// <param name="count">The number of times to tap.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        public ITouchAction Tap(IWebElement element, double? x = null, double? y = null, long? count = null)
        {
            Step tapStep = new Step("tap");
            tapStep
                .AddOpt("element", element)
                .AddOpt("x", x)
                .AddOpt("y", y)
                .AddOpt("count", count);
            steps.Add(tapStep);
            return this;
        }

        /// <summary>
        /// Tap at the specified location.
        /// </summary>
        /// <param name="x">The x coordinate relative to the element.</param>
        /// <param name="y">The y coordinate relative to the element.</param>
        /// <param name="count">The number of times to tap.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        public ITouchAction Tap(double x, double y, long? count = null)
        {
            Step tapStep = new Step("tap");
            tapStep
                .AddOpt("x", x)
                .AddOpt("y", y)
                .AddOpt("count", count);
            steps.Add(tapStep);
            return this;
        }

        /// <summary>
        /// Wait for the given duration.
        /// </summary>
        /// <param name="ms">The amount of time to wait in milliseconds.</param>
        /// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
        public ITouchAction Wait(long? ms = null)
        {
            Step waitStep = new Step("wait");
            waitStep
                .AddOpt("ms", ms);
            steps.Add(waitStep);
            return this;
        }

        public List<Dictionary<string, object>> GetParameters()
        {
            List<Dictionary<string, object>> parameters = new List<Dictionary<string, object>>();
            for (int i = 0; i < this.steps.Count; i++)
            {
                parameters.Add(steps[i].GetParameters());
            }
            return parameters;
        }

        /// <summary>
        /// Cancels the Touch Action
        /// </summary>
        public void Cancel()
        {
            steps.Clear();
        }

        /// <summary>
        /// Executes the Touch Action
        /// </summary>
        public void Perform()
        {
            TouchActionPerformer.PerformTouchAction(this);
        }
    }
}