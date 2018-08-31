using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Report.Pages
{
    class YouPage : Base
    {
        public YouPage()
        {
            PageFactory.InitElements(Browser, this);
        }

        ///<summary>UserName</summary>
        [FindsBy(How = How.CssSelector, Using = "div.title-block-content h1")]
        public IWebElement UserName { get; set; }

        ///<summary>You</summary>
        [FindsBy(How = How.CssSelector, Using = "a.you")]
        public IWebElement You { get; set; }

        public void NavigateYouPage()
        {
            You.Click();
        }
    }
}
