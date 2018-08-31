using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Report.Pages
{
    class Author : Base
    {
        public Author()
        {
            PageFactory.InitElements(Browser, this);
        }

        ///<summary>Author</summary>
        [FindsBy(How = How.CssSelector, Using = "h1.truncate")]
        public IWebElement AuthorPhoto { get; set; }


    }
}
