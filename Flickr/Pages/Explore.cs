using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Report.Pages
{
    class Explore : Base
    {

        public Explore()
        {
            PageFactory.InitElements(Browser, this);
        }

        ///<summary>Explore</summary>
        [FindsBy(How = How.XPath, Using = "//*[contains(@class, 'gn-title explore')]")]
        public IWebElement ExploreButton { get; set; }

        ///<summary>Photo</summary>
        [FindsBy(How = How.CssSelector, Using = "a.overlay")]
        public IList<IWebElement> Photo { get; set; }

        ///<summary>Photo title</summary>
        [FindsBy(How = How.CssSelector, Using = "div.text  a.title")]
        public IList<IWebElement> PhotoTitle { get; set; }

        public void NavigateExplore()
        {
            ExploreButton.Click();
        }

        public bool Label()
        {
            for(int i = 0; i < Photo.Count(); i++)
            {
                if (Photo[i].GetAttribute("aria-label") != null)
                {
                    ;
                }
                else return false;
            }
            return true;
        }


    }
}
