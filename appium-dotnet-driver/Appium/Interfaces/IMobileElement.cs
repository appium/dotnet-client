using Appium.Interfaces.Generic.SearchContext;
using OpenQA.Selenium.Appium.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        IGenericFindsByPartialLinkText<W>, IGenericFindsByTagName<W>, IGenericFindsByXPath<W>, IWebElement 
        where W: IWebElement
    {
    }
}
