using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.Appium.Interfaces;
using System.Reflection;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Internal;

namespace OpenQA.Selenium.Appium.MultiTouch
{
	public class MultiAction : IMultiAction 
    {
		private IList<ITouchAction> actions = new List<ITouchAction>();

		private IPerformsTouchActions TouchActionPerformer;
		private IWebElement element;

		private string GetIdForElement(IWebElement el) {
            RemoteWebElement remoteWebElement = el as RemoteWebElement;
            if (remoteWebElement != null)
                return (string)typeof(OpenQA.Selenium.Remote.RemoteWebElement).GetField("elementId",
                    BindingFlags.NonPublic | BindingFlags.Instance).GetValue(el);

            IWrapsElement elementWrapper = el as IWrapsElement;
            if (elementWrapper != null)
                return GetIdForElement(elementWrapper.WrappedElement);

            return null;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="MultiTouchAction"/> class.
		/// </summary>
		/// <param name="driver">The <see cref="IWebDriver"/> the driver to be used.</param>
		/// <param name="element">The <see cref="IWebDriver"/> the element on which the actions built will be performed.</param>
        public MultiAction(IPerformsTouchActions touchActionPerformer, IWebElement element)
            :this(touchActionPerformer)
		{
			this.element = element;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MultiTouchAction"/> class.
		/// </summary>
		/// <param name="driver">The <see cref="IWebDriver"/> the driver to be used.</param>
        public MultiAction(IPerformsTouchActions touchActionPerformer)
		{
            this.TouchActionPerformer = touchActionPerformer;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MultiTouchAction"/> class.
		/// </summary>
		/// <param name="element">The <see cref="IWebDriver"/> the element on which the actions built will be performed.</param>
		public MultiAction(IWebElement element)
		{
			this.element = element;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MultiTouchAction"/> class.
		/// </summary>
		public MultiAction()
		{
		}
			

		/// <summary>
		/// Sets the element.
		/// </summary>
		/// <param name="element">The <see cref="IWebDriver"/> the element on which the actions built will be performed.</param>
		public void setElement (IWebElement element) {
			this.element = element;
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
			Dictionary<string, object> parameters = new Dictionary<string, object> ();
			if (this.element != null) {
				parameters.Add ("elementId", GetIdForElement(this.element));
			}
			for (int i = 0; i < actions.Count; i++) {
				if (i == 0)
					parameters.Add ("actions", new List<object> ());
				((List<object>) parameters ["actions"])
					.Add (((TouchAction) actions[i]).GetParameters());
			}
            return parameters;
        }
		/// <summary>
		/// Cancels the Multi Action
		/// </summary>
		public void Cancel()
		{
			actions.Clear ();
		}

        /// <summary>
        /// Executes the Multi Action
        /// </summary>
        public void Perform()
        {
			TouchActionPerformer.PerformMultiAction (this);
        }
    }
}
