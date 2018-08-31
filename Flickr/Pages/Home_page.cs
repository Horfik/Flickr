using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Configuration;

namespace Report.Pages
{
    class Home_page : Base
    {

        public Home_page()
        {
            PageFactory.InitElements(Browser, this);
        }

        ///<summary>Root item</summary>
        [FindsBy(How = How.XPath, Using = ".//ul[@class='nav-menu desktop-only']/li/a")]
        public IList<IWebElement> RootItem { get; set; }

        ///<summary>Sub-menu item</summary>
        [FindsBy(How = How.CssSelector, Using = "ul.nav-menu ul a")]
        public IList<IWebElement> SubMenuItem { get; set; }

        public bool ElementsConteinLinc()
        {

            for (int i = 0; i < SubMenuItem.Count(); i++)
            {
                if (SubMenuItem[i].GetAttribute("href") != null)
                {
                    ;
                }
                else return false;
            }
            return true;
        }

        public int Item()
        {
            return RootItem.Count();
        }
    }
}
