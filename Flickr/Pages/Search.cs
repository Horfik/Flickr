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
    class Search : Base
    {
        public Search()
        {
            PageFactory.InitElements(Browser, this);
        }

        ///<summary>Search field</summary>
        [FindsBy(How = How.Id, Using = "search-field")]
        public IWebElement FieldSearch { get; set; }

        ///<summary>Result rearch</summary>
        [FindsBy(How = How.CssSelector, Using = "div.photo-list-photo-view div.interaction-bar")]
        public IList<IWebElement> Result { get; set; }

        public void SearchFlickr()
        {
            FieldSearch.SendKeys(ConfigurationManager.AppSettings.Get("WordForSearch") + OpenQA.Selenium.Keys.Enter);
        }

        public int Contein()
        {
            int k = 0;
            for (int i = 0; i < Result.Count(); i++)
            {
                if (Result[i].GetAttribute("title").Contains(ConfigurationManager.AppSettings.Get("WordForSearch")))
                    k += 1;              
            }
            return k;
        }
    }
}
