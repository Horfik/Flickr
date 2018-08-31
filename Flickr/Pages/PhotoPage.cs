using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Pages
{
    class PhotoPage : Base
    {
        public PhotoPage()
        {
            PageFactory.InitElements(Browser, this);
        }
        
        ///<summary>Author</summary>
        [FindsBy(How = How.CssSelector, Using = "div.clear-float a.owner-name")]
        public IWebElement Author { get; set; }

        ///<summary>Photo Name</summary>
        [FindsBy(How = How.CssSelector, Using = "h1.photo-title")]
        public IWebElement PhotoName { get; set; }

        ///<summary>Follow button</summary>
        [FindsBy(How = How.CssSelector, Using = "h1.photo-title")]
        public IWebElement Follow { get; set; }

        ///<summary>Numbers</summary>
        [FindsBy(How = How.CssSelector, Using = ".sub-photo-right-stats-view")]
        public IWebElement Numbers { get; set; }

        ///<summary>Right linc</summary>
        [FindsBy(How = How.ClassName, Using = "photo-license-url")]
        public IWebElement Rightlinc { get; set; }

        ///<summary>Tags</summary>
        [FindsBy(How = How.CssSelector, Using = "ul.tags-list > li > a:nth-child(2)")]
        public IList<IWebElement> Tags { get; set; }

        ///<summary>AutoTags</summary>
        [FindsBy(How = How.CssSelector, Using = "ul.tags-list li.autotag a")]
        public IList<IWebElement> AutoTags { get; set; }

        ///<summary>Camera details</summary>
        [FindsBy(How = How.CssSelector, Using = ".photo-charm-exif-scrappy-view")]
        public IList<IWebElement> Camera { get; set; }

        public bool ElementsConteinLinc()
        {

            for (int i = 0; i < Tags.Count(); i++)
            {
                if (Tags[i].GetAttribute("href") != null)
                {
                    ;
                }
                else return false;
            }
            return true;
        }

        public bool ElementsConteinLinc2()
        {

            for (int i = 0; i < AutoTags.Count(); i++)
            {
                if (AutoTags[i].GetAttribute("href") != null)
                {
                    ;
                }
                else return false;
            }
            return true;
        }
    }
}
