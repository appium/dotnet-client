using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Interactions;

namespace OpenQA.Selenium.Appium
{
    /// <summary>
    /// Extensiosn class for the touch actions
    /// </summary>
    public static class TouchExtensions
    {
        #region Private member Variables
        private static readonly Lazy<Assembly> _SeleniumAssembly = new Lazy<Assembly>(() => Assembly.Load("WebDriver"));

        #region Class Types
        private static readonly Lazy<Type> _ScreenPressActionType = new Lazy<Type>(() => _GetClassType("ScreenPressAction"));
        //private static readonly Lazy<Type> _SingleTapActionType = new Lazy<Type>(() => _GetClassType("SingleTapAction"));
        private static readonly Lazy<Type> _ScreenReleaseActionType = new Lazy<Type>(() => _GetClassType("ScreenReleaseAction"));
        private static readonly Lazy<Type> _ScreenMoveActionType = new Lazy<Type>(() => _GetClassType("ScreenMoveAction"));
        //private static readonly Lazy<Type> _ScrollActionType = new Lazy<Type>(() => _GetClassType("ScrollAction"));
        //private static readonly Lazy<Type> _DoubleTapActionType = new Lazy<Type>(() => _GetClassType("DoubleTapAction"));
        //private static readonly Lazy<Type> _LongPressActionType = new Lazy<Type>(() => _GetClassType("LongPressAction"));
        //private static readonly Lazy<Type> _FlickActionType = new Lazy<Type>(() => _GetClassType("FlickAction"));
        //private static readonly Lazy<Type> _TouchActionType = new Lazy<Type>(() => _GetClassType("Internal.WebDriverAction"));
        #endregion Class Types

        #region Field Info List
        //private static readonly Lazy<List<FieldInfo>> _TouchFieldInfoList = new Lazy<List<FieldInfo>>(() => _GetFieldInfoList(_TouchActionType.Value));
        private static readonly Lazy<List<FieldInfo>> _ScreenPressFieldInfoList = new Lazy<List<FieldInfo>>(() => _GetFieldInfoList(_ScreenPressActionType.Value));
        //private static readonly Lazy<List<FieldInfo>> _SingleTapFieldInfoList = new Lazy<List<FieldInfo>>(() => _GetFieldInfoList(_SingleTapActionType.Value));
        //private static readonly Lazy<List<FieldInfo>> _ScreenReleaseFieldInfoList = new Lazy<List<FieldInfo>>(() => _GetFieldInfoList(_ScreenReleaseActionType.Value));
        private static readonly Lazy<List<FieldInfo>> _ScreenMoveFieldInfoList = new Lazy<List<FieldInfo>>(() => _GetFieldInfoList(_ScreenMoveActionType.Value));
        //private static readonly Lazy<List<FieldInfo>> _ScrollFieldInfoList = new Lazy<List<FieldInfo>>(() => _GetFieldInfoList(_ScrollActionType.Value));
        //private static readonly Lazy<List<FieldInfo>> _DoubleTapFieldInfoList = new Lazy<List<FieldInfo>>(() => _GetFieldInfoList(_DoubleTapActionType.Value));
        //private static readonly Lazy<List<FieldInfo>> _LongPressFieldInfoList = new Lazy<List<FieldInfo>>(() => _GetFieldInfoList(_LongPressActionType.Value));
        //private static readonly Lazy<List<FieldInfo>> _FlickFieldInfoList = new Lazy<List<FieldInfo>>(() => _GetFieldInfoList(_FlickActionType.Value));
        #endregion Field Info List

        #region Field Info
        private static readonly Lazy<FieldInfo> _ActionsListFieldInfo = new Lazy<FieldInfo>(() => _GetFieldInfo("CompositeAction", "actionsList"));
        #endregion Field Info

        #endregion Private member Variables

        #region Public Methods
        /// <summary>
        /// Gets the list of actions for the touch action to be used in MultiTouchAction 
        /// </summary>
        /// <param name="touchAction">touch action</param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> GetActionsList(this TouchActions touchAction)
        {
            if (null == touchAction)
            {
                throw new ArgumentNullException("touchAction");
            }

            var actionsList = new List<Dictionary<string, object>>();
            foreach (var iaction in touchAction.GetActions())
            {
                Dictionary<string, object> yy = null;
                if (null == iaction)
                {
                    // do nothing
                }
                else if (null == (yy = iaction._GetParameters()))
                {
                    // do nothing
                }
                else
                {
                    actionsList.Add(yy);
                }
            }

            return actionsList;
        }

        /// <summary>
        /// Pause execution of action chain for the given number of milliseconds.
        /// Defaults to zero so that it can be used for synchronizing actions
        /// </summary>
        /// <param name="touchAction">touch action chain to pause</param>
        /// <param name="ms">number of milliseconds to pause the action chain for</param>
        /// <returns></returns>
        public static TouchActions Wait(this TouchActions touchAction, int ms=0)
        {
            if (null == touchAction)
            {
                throw new ArgumentNullException("touchAction");
            }

            // use reflection to find the AddAction() to add the wait action into the action chain
            var mi = typeof(TouchActions).GetMethod("AddAction", BindingFlags.NonPublic | BindingFlags.Instance);

            if (null != mi)
            {
                mi.Invoke(touchAction, new object[] { new WaitAction(ms)} ); 
            }

            return touchAction;
        }

        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Returns the parameter list for the specified action
        /// </summary>
        /// <param name="action">action to get the parameters for</param>
        /// <returns>Dictionary of parameters if possible, else throws</returns>
        private static Dictionary<string, object> _GetParameters(this IAction action)
        {
            if (null == action)
            {
                throw new ArgumentNullException("action can not be null");
            }

            var actionType = action.GetType();
            ActionParameter actionParameter = null;

            if (actionType == _ScreenPressActionType.Value)
            {
                var x = _ScreenPressFieldInfoList.Value.Find(s => s.Name == "x").GetValue(action);
                var y = _ScreenPressFieldInfoList.Value.Find(s => s.Name == "y").GetValue(action);

                actionParameter = new ActionParameter("press");
                actionParameter.AddParameter("x", x);
                actionParameter.AddParameter("y", y);
            }
            else if (actionType == _ScreenReleaseActionType.Value)
            {
                actionParameter = new ActionParameter("release");
            }
            else if (actionType == _ScreenMoveActionType.Value)
            {
                var x = _ScreenMoveFieldInfoList.Value.Find(s => s.Name == "x").GetValue(action);
                var y = _ScreenMoveFieldInfoList.Value.Find(s => s.Name == "y").GetValue(action);

                actionParameter = new ActionParameter("moveTo");
                actionParameter.AddParameter("x", x);
                actionParameter.AddParameter("y", y);
            }
            else if (actionType == typeof(WaitAction))
            {
                actionParameter = new ActionParameter("wait");
                actionParameter.AddParameter("ms", ((WaitAction)action).Duration);
            }

            if (null == actionParameter)
            {
                throw new NotImplementedException(string.Format("Multi Touch does not support this feature {0}", action.GetType()));
            }

            return actionParameter.GetDictionary();
        }

        /// <summary>
        /// Returns a list of actions 
        /// </summary>
        /// <param name="actions"></param>
        /// <returns></returns>
        private static IEnumerable<IAction> GetActions(this TouchActions actions)
        {
            List<IAction> retVal = null;

            if (null == actions)
            {
                return null;
            }

            try
            {
                //var compositeActionObj = _ActionFieldInfo.Value.GetValue(actions);
                retVal = _ActionsListFieldInfo.Value.GetValue(actions.Build()) as List<IAction>;
            }
            catch
            {
                // unable to get the field
            }

            return retVal;
        }

        /// <summary>
        /// Get the Internal Action type 
        /// </summary>
        /// <param name="typeName">class type name to find via reflection</param>
        /// <returns>type of object if found, else throws</returns>
        private static Type _GetClassType(string typeName)
        {
            return _SeleniumAssembly.Value.GetType(string.Format("OpenQA.Selenium.Interactions.{0}", typeName));
        }

        /// <summary>
        /// Get the field info for the given class type 
        /// </summary>
        /// <param name="classTypeName">name of the class type</param>
        /// <param name="privateFieldName">Private field name one wishes to access</param>
        /// <returns>The FieldInfo for given private field</returns>
        private static FieldInfo _GetFieldInfo(string classTypeName, string privateFieldName)
        {
            var classType = _GetClassType(classTypeName);
            return classType.GetField(privateFieldName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField);
        }

        /// <summary>
        /// Returns a list of field info for the given class
        /// </summary>
        /// <param name="classType">Type of the class</param>
        /// <returns>List of field info objects for the given class (Public | Non Public | Instance)</returns>
        private static List<FieldInfo> _GetFieldInfoList(Type classType)
        {
            return classType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToList();
        }
        #endregion Private Methods

        #region Private class
        /// <summary>
        /// Class used to keep track of the parameters for the action
        /// </summary>
        private class ActionParameter
        {
            #region Private Member Variables
            /// <summary>
            /// Action to perform
            /// </summary>
            private readonly string _ActionName;

            /// <summary>
            /// Dictionary of options
            /// </summary>
            private readonly Dictionary<string, object> _Options = new Dictionary<string, object>();
            #endregion Private Member Variables

            #region Constructor
            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="actionName">action to perform</param>
            public ActionParameter(string actionName)
            {
                _ActionName = actionName;
            }
            #endregion Constructor

            #region Public Methods
            /// <summary>
            /// Add new parameter for the action
            /// </summary>
            /// <param name="key">parameter key</param>
            /// <param name="value">parameter value</param>
            public void AddParameter(string key, object value)
            {
                _Options[key] = value;
            }

            /// <summary>
            /// Returns the dictionary for this object
            /// </summary>
            /// <returns></returns>
            public Dictionary<string, object> GetDictionary()
            {
                var retVal = new Dictionary<string, object> { { "action", _ActionName }, { "options", _Options } };
                return retVal;
            }
            #endregion Public Methods
        }
        #endregion Private class

    }
}
