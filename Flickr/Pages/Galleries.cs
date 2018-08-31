using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Report.Pages
{
    class Galleries : Base
    {
        public Galleries()
        {
            PageFactory.InitElements(Browser, this);
        }

        ///<summary>Galleries name</summary>
        [FindsBy(How = How.CssSelector, Using = "h4 a.Seta")]
        public IList<IWebElement> Name { get; set; }

        ///<summary>Galleries numbers</summary>
        [FindsBy(How = How.CssSelector, Using = "div.gallery-case-meta p")]
        public IList<IWebElement> Numbers { get; set; }
    }
}
