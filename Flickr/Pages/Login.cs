using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Configuration;
using OpenQA.Selenium.Support.UI;

namespace Report.Pages
{
    public class Login : Base
    {

        public Login()
        {
            Browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            PageFactory.InitElements(Browser, this);
        }

        ///<summary>Log in</summary>
        [FindsBy(How = How.CssSelector, Using = "li.gn-signin a.gn-title")]
        public IWebElement Log_in { get; set; }

        ///<summary>Email field</summary>
        [FindsBy(How = How.ClassName, Using = "phone-no")]
        public IWebElement Email { get; set; }

        ///<summary>Password field</summary>
        [FindsBy(How = How.Id, Using = "login-passwd")]
        public IWebElement Password { get; set; }

        public void LoginFlickr()
        {
            this.Log_in.Click();
            this.Email.SendKeys(ConfigurationManager.AppSettings.Get("email") + OpenQA.Selenium.Keys.Enter);
            
            this.Password.SendKeys(ConfigurationManager.AppSettings.Get("password") + OpenQA.Selenium.Keys.Enter);
        }
    }

}
