using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Report.Pages
{
    class Alboms :Base
    {
        public Alboms()
        {
            PageFactory.InitElements(Browser, this);
        }

        ///<summary>Go Albom</summary>
        [FindsBy(How = How.CssSelector, Using = "#albums a")]
        public IWebElement GoAlbom { get; set; }

        ///<summary>Thumbnail</summary>
        [FindsBy(How = How.CssSelector, Using = "div.photo-list-view div.awake")]
        public IList<IWebElement> Thumbnail { get; set; }

        ///<summary>Linc</summary>
        [FindsBy(How = How.CssSelector, Using = "a.overlay")]
        public IList<IWebElement> Linc { get; set; }

        ///<summary>Albom Name</summary>
        [FindsBy(How = How.CssSelector, Using = "h4.album-title")]
        public IList<IWebElement> Name { get; set; }

        ///<summary>Numbers of photo</summary>
        [FindsBy(How = How.ClassName, Using = "album-photo-count")]
        public IList<IWebElement> Photo { get; set; }

        ///<summary>Numbers of views</summary>
        [FindsBy(How = How.ClassName, Using = "album-views-label")]
        public IList<IWebElement> View { get; set; }
    }


}
