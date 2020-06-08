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

using System.Text;
using OpenQA.Selenium.Appium.Interfaces;

namespace OpenQA.Selenium.Appium.Android.UiAutomator
{
    /// <summary>
    /// A convenience class to wrap Android UiSelector method calls.
    /// The output of this class can be passed to some methods to find an element using
    /// the Android UiSelector class.
    /// </summary>
    /// <remarks>
    /// Class docs: https://developer.android.com/reference/android/support/test/uiautomator/UiSelector.html
    /// </remarks>
    public class AndroidUiSelector : IUiAutomatorStatementBuilder
    {
        private readonly StringBuilder _builder;

        /// <summary>
        /// Creates a new UiSelector builder.
        /// </summary>
        public AndroidUiSelector()
        {
            _builder = new StringBuilder("new UiSelector()");
        }

        /// <summary>
        /// Creates a new UiSelector builder by copying all existing data
        /// from the given selector.
        /// </summary>
        /// <param name="selector">
        /// The <see cref="AndroidUiSelector"/> to copy into this new instance
        /// </param>
        public AndroidUiSelector(AndroidUiSelector selector)
        {
            _builder = new StringBuilder(selector.RequireNotNull(nameof(selector)).Build());
        }

        /// <summary>
        /// Set the search criteria to match widgets that are checkable. Typically, using this search criteria
        /// alone is not useful. You should also include additional criteria, such as text, content-description,
        /// or the class name for a widget. If no other search criteria is specified, and there is more than one
        /// matching widget, the first widget in the tree is selected.
        /// Maps to the UiSelector.checkable(boolean) method.
        /// </summary>
        /// <param name="value">
        /// When true, matches elements which are checkable. When false, matches elements which are not checkable.
        /// </param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#checkable</remarks>
        public AndroidUiSelector IsCheckable(bool value)
        {
            _builder.AppendFormat(".checkable({0})", value.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Set the search criteria to match widgets that are currently checked (usually for checkboxes). Typically,
        /// using this search criteria alone is not useful. You should also include additional criteria, such as text,
        /// content-description, or the class name for a widget. If no other search criteria is specified, and there is
        /// more than one matching widget, the first widget in the tree is selected.
        /// Maps to the UiSelector.checked(boolean) method.
        /// </summary>
        /// <param name="value">
        /// When true, matches elements which are checked. When false, matches elements which are not checked.
        /// </param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#checked</remarks>
        public AndroidUiSelector IsChecked(bool value)
        {
            _builder.AppendFormat(".checked({0})", value.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Adds a child UiSelector criteria to this selector. Use this selector to narrow the search scope to child
        /// widgets under a specific parent widget.
        /// Maps to the UiSelector.childSelector(UiSelector) method.
        /// </summary>
        /// <param name="selector"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#childselector</remarks>
        public AndroidUiSelector ChildSelector(AndroidUiSelector selector)
        {
            _builder.AppendFormat(".childSelector({0})", selector.RequireNotNull(nameof(selector)).Build());
            return this;
        }

        /// <summary>
        /// Set the search criteria to match the class property for a widget (for example, "android.widget.Button").
        /// Maps to the UiSelector.className(String) method.
        /// </summary>
        /// <param name="className"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#classname</remarks>
        public AndroidUiSelector ClassNameEquals(string className)
        {
            _builder.AppendFormat(".className(\"{0}\")", className.RequireNotNull(nameof(className)));
            return this;
        }

        /// <summary>
        /// Set the search criteria to match the class property for a widget (for example, "android.widget.Button").
        /// Maps to the UiSelector.classNameMatches(String) method.
        /// </summary>
        /// <param name="regex"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#classnamematches</remarks>
        public AndroidUiSelector ClassNameMatches(string regex)
        {
            _builder.AppendFormat(".classNameMatches(\"{0}\")", regex.RequireNotNull(nameof(regex)));
            return this;
        }

        /// <summary>
        /// Set the search criteria to match widgets that are clickable. Typically, using this search criteria alone is not
        /// useful. You should also include additional criteria, such as text, content-description, or the class name for a
        /// widget. If no other search criteria is specified, and there is more than one matching widget, the first widget
        /// in the tree is selected.
        /// Maps to the UiSelector.clickable(boolean) method.
        /// </summary>
        /// <param name="value">
        /// When true, matches elements which are clickable. When false, matches elements which are not clickable.
        /// </param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#clickable</remarks>
        public AndroidUiSelector IsClickable(bool value)
        {
            _builder.AppendFormat(".clickable({0})", value.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Set the search criteria to match the content-description property for a widget. The content-description
        /// is typically used by the Android Accessibility framework to provide an audio prompt for the widget when
        /// the widget is selected. The content-description for the widget must match exactly with the string in your
        /// input argument. Matching is case-sensitive.
        /// Maps to the UiSelector.description(String) method.
        /// </summary>
        /// <param name="description"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#description</remarks>
        public AndroidUiSelector DescriptionEquals(string description)
        {
            _builder.AppendFormat(".description(\"{0}\")", description.RequireNotNull(nameof(description)));
            return this;
        }

        /// <summary>
        /// Set the search criteria to match the content-description property for a widget. The content-description is
        /// typically used by the Android Accessibility framework to provide an audio prompt for the widget when the
        /// widget is selected. The content-description for the widget must contain the string in your input argument.
        /// Matching is case-insensitive.
        /// Maps to the UiSelector.descriptionContains(String) method.
        /// </summary>
        /// <param name="description"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#descriptioncontains</remarks>
        public AndroidUiSelector DescriptionContains(string description)
        {
            _builder.AppendFormat(".descriptionContains(\"{0}\")", description.RequireNotNull(nameof(description)));
            return this;
        }

        /// <summary>
        /// Set the search criteria to match the content-description property for a widget. The content-description is
        /// typically used by the Android Accessibility framework to provide an audio prompt for the widget when the
        /// widget is selected. The content-description for the widget must match exactly with the string in your input
        /// argument.
        /// Maps to the UiSelector.descriptionMatches(String) method.
        /// </summary>
        /// <param name="description"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#descriptionmatches</remarks>
        public AndroidUiSelector DescriptionMatches(string description)
        {
            _builder.AppendFormat(".descriptionMatches(\"{0}\")", description.RequireNotNull(nameof(description)));
            return this;
        }

        /// <summary>
        /// Set the search criteria to match the content-description property for a widget. The content-description is
        /// typically used by the Android Accessibility framework to provide an audio prompt for the widget when the
        /// widget is selected. The content-description for the widget must start with the string in your input argument.
        /// Matching is case-insensitive.
        /// Maps to the UiSelector.descriptionStartsWith(String) method.
        /// </summary>
        /// <param name="description"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#descriptionstartswith</remarks>
        public AndroidUiSelector DescriptionStartsWith(string description)
        {
            _builder.AppendFormat(".descriptionStartsWith(\"{0}\")", description.RequireNotNull(nameof(description)));
            return this;
        }

        /// <summary>
        /// Set the search criteria to match widgets that are enabled. Typically, using this search criteria alone is not
        /// useful. You should also include additional criteria, such as text, content-description, or the class name for
        /// a widget. If no other search criteria is specified, and there is more than one matching widget, the first
        /// widget in the tree is selected.
        /// Maps to the UiSelector.enabled(boolean) method.
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#enabled</remarks>
        public AndroidUiSelector IsEnabled(bool value)
        {
            _builder.AppendFormat(".enabled({0})", value.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Set the search criteria to match widgets that are focusable. Typically, using this search criteria alone is
        /// not useful. You should also include additional criteria, such as text, content-description, or the class
        /// name for a widget. If no other search criteria is specified, and there is more than one matching widget,
        /// the first widget in the tree is selected.
        /// Maps to the UiSelector.focusable(boolean) method.
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#focusable</remarks>
        public AndroidUiSelector IsFocusable(bool value)
        {
            _builder.AppendFormat(".focusable({0})", value.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Set the search criteria to match widgets that have focus. Typically, using this search criteria alone is
        /// not useful. You should also include additional criteria, such as text, content-description, or the class
        /// name for a widget. If no other search criteria is specified, and there is more than one matching widget,
        /// the first widget in the tree is selected.
        /// Maps to the UiSelector.focused(boolean) method.
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#focused</remarks>
        public AndroidUiSelector IsFocused(bool value)
        {
            _builder.AppendFormat(".focused({0})", value.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Adds a child UiSelector criteria to this selector which is used to start search from the parent widget.
        /// Use this selector to narrow the search scope to sibling widgets as well all child widgets under a parent.
        /// Maps to the UiSelector.fromParent(UiSelector) method.
        /// </summary>
        /// <param name="selector"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#fromparent</remarks>
        public AndroidUiSelector FromParent(AndroidUiSelector selector)
        {
            _builder.AppendFormat(".fromParent({0})", selector.RequireNotNull(nameof(selector)).Build());
            return this;
        }

        /// <summary>
        /// Set the search criteria to match the widget by its node index in the layout hierarchy. The index value must
        /// be 0 or greater. Using the index can be unreliable and should only be used as a last resort for matching.
        /// Instead, consider using the <see cref="Instance"/> method.
        /// Maps to the UiSelector.index(int) method.
        /// </summary>
        /// <param name="index"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#index</remarks>
        public AndroidUiSelector Index(int index)
        {
            _builder.AppendFormat(".index({0})", index.RequireIsPositive(nameof(index)));
            return this;
        }

        /// <summary>
        /// Set the search criteria to match the widget by its instance number. The instance value must
        /// be 0 or greater, where the first instance is 0. For example, to simulate a user click on the
        /// third image that is enabled in a UI screen, you could specify a a search criteria where the
        /// instance is 2, the className(String) matches the image widget class, and enabled(boolean) is
        /// true.
        /// Maps to the UiSelector.instance(int) method.
        /// </summary>
        /// <param name="instance">
        /// The 0-indexed instance to match on
        /// </param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#instance</remarks>
        public AndroidUiSelector Instance(int instance)
        {
            _builder.AppendFormat(".instance({0})", instance.RequireIsPositive(nameof(instance)));
            return this;
        }

        /// <summary>
        /// Set the search criteria to match widgets that are long-clickable. Typically, using this search
        /// criteria alone is not useful. You should also include additional criteria, such as text,
        /// content-description, or the class name for a widget. If no other search criteria is specified,
        /// and there is more than one matching widget, the first widget in the tree is selected.
        /// Maps to the UiSelector.longClickable(int) method.
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#longclickable</remarks>
        public AndroidUiSelector IsLongClickable(bool value)
        {
            _builder.AppendFormat(".longClickable({0})", value.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Set the search criteria to match the package name of the application that contains the widget.
        /// Maps to the UiSelector.packageName(String) method.
        /// </summary>
        /// <param name="packageName"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#packagename</remarks>
        public AndroidUiSelector PackageNameEquals(string packageName)
        {
            _builder.AppendFormat(".packageName(\"{0}\")", packageName.RequireNotNull(nameof(packageName)));
            return this;
        }

        /// <summary>
        /// Set the search criteria to match the package name of the application that contains the widget.
        /// Maps to the UiSelector.packageNameMatches(String) method.
        /// </summary>
        /// <param name="regex"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#packagenamematches</remarks>
        public AndroidUiSelector PackageNameMatches(string regex)
        {
            _builder.AppendFormat(".packageNameMatches(\"{0}\")", regex.RequireNotNull(nameof(regex)));
            return this;
        }

        /// <summary>
        /// Set the search criteria to match the given resource ID.
        /// Maps to the UiSelector.resourceId(String) method.
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#resourceid</remarks>
        public AndroidUiSelector ResourceIdEquals(string id)
        {
            _builder.AppendFormat(".resourceId(\"{0}\")", id.RequireNotNull(nameof(id)));
            return this;
        }

        /// <summary>
        /// Set the search criteria to match the resource ID of the widget, using a regular expression.
        /// Maps to the UiSelector.resourceIdMatches(String) method.
        /// </summary>
        /// <param name="regex"></param>
        /// <remarks>https://developer.android.com/reference/android/support/test/uiautomator/UiSelector.html#resourceidmatches</remarks>
        public AndroidUiSelector ResourceIdMatches(string regex)
        {
            _builder.AppendFormat(".resourceIdMatches(\"{0}\")", regex.RequireNotNull(nameof(regex)));
            return this;
        }

        /// <summary>
        /// Set the search criteria to match widgets that are scrollable. Typically, using this
        /// search criteria alone is not useful. You should also include additional criteria, such
        /// as text, content-description, or the class name for a widget. If no other search criteria
        /// is specified, and there is more than one matching widget, the first widget in the tree
        /// is selected.
        /// Maps to the UiSelector.scrollable(boolean) method.
        /// </summary>
        /// <param name="value">
        /// When true, matches elements which are scrollable. When false, matches elements which are not scrollable.
        /// </param>
        /// <remarks>https://developer.android.com/reference/android/support/test/uiautomator/UiSelector.html#scrollable</remarks>
        public AndroidUiSelector IsScrollable(bool value)
        {
            _builder.AppendFormat(".scrollable({0})", value.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Set the search criteria to match widgets that are currently selected. Typically, using this
        /// search criteria alone is not useful. You should also include additional criteria, such as text,
        /// content-description, or the class name for a widget. If no other search criteria is specified, and
        /// there is more than one matching widget, the first widget in the tree is selected.
        /// Maps to the UiSelector.selected(boolean) method.
        /// </summary>
        /// <param name="value">
        /// When true, matches elements which are scrollable. When false, matches elements which are not scrollable.
        /// </param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#selected</remarks>
        public AndroidUiSelector IsSelected(bool value)
        {
            _builder.AppendFormat(".selected({0})", value.ToString().ToLowerInvariant());
            return this;
        }

        /// <summary>
        /// Includes elements whose accessibility-id attribute is equal to the given text.
        /// Maps to the UiSelector.text(String) method.
        /// </summary>
        /// <param name="text"></param>
        /// <remarks>https://developer.android.com/reference/android/support/test/uiautomator/UiSelector.html#text</remarks>
        public AndroidUiSelector AccessibilityIdEquals(string text)
        {
            // these are the same method. this version is only included to prevent confusion
            // since its kinda weird that it handles both text and accessibility-id
            return TextEquals(text.RequireNotNull(nameof(text)));
        }

        /// <summary>
        /// Includes elements whose text attribute is equal to the given text.
        /// Maps to the UiSelector.text(String) method.
        /// </summary>
        /// <param name="text"></param>
        /// <remarks>https://developer.android.com/reference/android/support/test/uiautomator/UiSelector.html#text</remarks>
        public AndroidUiSelector TextEquals(string text)
        {
            _builder.AppendFormat(".text(\"{0}\")", text.RequireNotNull(nameof(text)));
            return this;
        }

        /// <summary>
        /// Set the search criteria to match the visible text in a widget where the visible text must contain the
        /// string in your input argument. The matching is case-sensitive.
        /// Maps to the UiSelector.textContains(String) method.
        /// </summary>
        /// <param name="text"></param>
        /// <remarks>https://developer.android.com/reference/android/support/test/uiautomator/UiSelector.html#textcontains</remarks>
        public AndroidUiSelector TextContains(string text)
        {
            _builder.AppendFormat(".textContains(\"{0}\")", text.RequireNotNull(nameof(text)));
            return this;
        }

        /// <summary>
        /// Set the search criteria to match the visible text displayed in a layout element, using a regular expression.
        /// The text in the widget must match exactly with the string in your input argument.
        /// Maps to the UiSelector.textMatches(String) method.
        /// </summary>
        /// <param name="regex">
        /// A regular expression
        /// </param>
        /// <remarks>https://developer.android.com/reference/androidx/test/uiautomator/UiSelector#textmatches</remarks>
        public AndroidUiSelector TextMatches(string regex)
        {
            _builder.AppendFormat(".textMatches(\"{0}\")", regex.RequireNotNull(nameof(regex)));
            return this;
        }

        /// <summary>
        /// Set the search criteria to match visible text in a widget that is prefixed by the text parameter.
        /// The matching is case-insensitive.
        /// Maps to the UiSelector.textStartsWith(String) method.
        /// </summary>
        /// <param name="text"></param>
        /// <remarks>https://developer.android.com/reference/android/support/test/uiautomator/UiSelector#textstartswith</remarks>
        public AndroidUiSelector TextStartsWith(string text)
        {
            _builder.AppendFormat(".textStartsWith(\"{0}\")", text.RequireNotNull(nameof(text)));
            return this;
        }

        /// <summary>
        /// Append raw text to this <see cref="AndroidUiSelector"/> instance. The target language is Java.
        /// Text entered here will not be checked for validity. Use this at your own risk.
        /// </summary>
        /// <param name="text">
        /// Text to be appended to the UiSelector command builder.
        /// </param>
        public AndroidUiSelector AddRawText(string text)
        {
            _builder.Append(text.RequireNotNull(nameof(text)));
            return this;
        }

        /// <summary>
        /// Compiles the current UiSelector statements that have
        /// been added to this instance.
        /// </summary>
        /// <returns></returns>
        public string Build()
        {
            return _builder.ToString();
        }
    }
}
