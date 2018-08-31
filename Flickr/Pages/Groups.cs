using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Report.Pages
{
    class Groups : Base
    {
        public Groups()
        {
            PageFactory.InitElements(Browser, this);
        }

        ///<summary>Create groups</summary>
        [FindsBy(How = How.ClassName, Using = "create-group")]
        public IWebElement GreateGroups { get; set; }

        ///<summary>Join</summary>
        [FindsBy(How = How.CssSelector, Using = "div.recommended-group button")]
        public IList<IWebElement> Join { get; set; }

        ///<summary>Name</summary>
        [FindsBy(How = How.CssSelector, Using = "div.text-content div.title")]
        public IList<IWebElement> Name { get; set; }

        ///<summary>Numbers</summary>
        [FindsBy(How = How.CssSelector, Using = "div.links i")]
        public IList<IWebElement> Numbers { get; set; }
    }
}
