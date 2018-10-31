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

using System;
using System.Collections.Generic;
using OpenQA.Selenium.Appium.Interfaces;

namespace OpenQA.Selenium.Appium.MultiTouch
{
    public class MultiAction : IMultiAction
    {
        private IList<ITouchAction> actions = new List<ITouchAction>();

        private IPerformsTouchActions TouchActionPerformer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiTouchAction"/> class.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver"/> the driver to be used.</param>
        public MultiAction(IPerformsTouchActions touchActionPerformer)
        {
            this.TouchActionPerformer = touchActionPerformer;
        }


        /// <summary>
        /// Add touch actions to be performed
        /// </summary>
        /// <param name="touchAction"></param>
        public IMultiAction Add(ITouchAction touchAction)
        {
            if (null == touchAction)
            {
                throw new ArgumentNullException("touchAction");
            }

            actions.Add(touchAction);
            return this;
        }

        /// <summary>
        /// Gets the actions parameter dictionary for this multi touch action
        /// </summary>
        /// <returns>empty dictionary if no actions found, else dictionary of actions</returns>
        public Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("actions", new List<object>());

            for (int i = 0; i < actions.Count; i++)
            {
                ((List<object>) parameters["actions"])
                    .Add(((TouchAction) actions[i]).GetParameters());
            }
            return parameters;
        }

        /// <summary>
        /// Cancels the Multi Action
        /// </summary>
        public void Cancel()
        {
            actions.Clear();
        }

        /// <summary>
        /// Executes the Multi Action
        /// </summary>
        public void Perform()
        {
            if (actions.Count == 1)
            {
                TouchActionPerformer.PerformTouchAction(actions[0]);
            }
            if (actions.Count > 1)
            {
                TouchActionPerformer.PerformMultiAction(this);
            }
            if (actions.Count == 0)
            {
                throw new ArgumentException(
                    "Multi action must have at least one TouchAction added before it can be performed");
            }
        }
    }
}