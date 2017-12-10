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

using Appium.Interfaces.Generic.SearchContext;

namespace OpenQA.Selenium.Appium.Interfaces
{
    /// <summary>
    /// This interface extends IWebElement and defines specific behavior
    /// for mobile.
    /// </summary>
    public interface IMobileElement<W> : IFindByAccessibilityId<W>, IGenericSearchContext<W>,
        IGenericFindsByClassName<W>,
        IGenericFindsById<W>, IGenericFindsByCssSelector<W>, IGenericFindsByLinkText<W>,
        IGenericFindsByName<W>,
        IGenericFindsByPartialLinkText<W>, IGenericFindsByTagName<W>, IGenericFindsByXPath<W>, IWebElement,
        IExecuteMethod
        where W : IWebElement
    {
    }
}