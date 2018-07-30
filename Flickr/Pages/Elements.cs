using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Report.Pages
{
    class Elements
    {
        ///<summary>Кнопка Flickr</summary>
        [FindsBy(How = How.CssSelector, Using = "a.main-logo")]
        public IWebElement Flickr { get; set; }

        ///<summary>Кнопка You</summary>
        [FindsBy(How = How.ClassName, Using = "you")]
        public IWebElement you { get; set; }

        ///<summary>Кнопка Explore</summary>
        [FindsBy(How = How.CssSelector, Using = "li a.explore")]
        public IWebElement explore { get; set; }

        ///<summary>Поле поиска</summary>
        [FindsBy(How = How.Id, Using = "search-field")]
        public IWebElement Search { get; set; }
    }
}
