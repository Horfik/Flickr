using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Report.Pages;

namespace Report
{
    [SetUpFixture]
    class FixtureUp : Base
    {
        public static GenerateReport generate = new GenerateReport();
        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            
            generate.GenerateReports(0, "0", true);
            int i = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Browser"));
            if (i == 1)
            {
                ChromeOptions option = new ChromeOptions();
                option.AddArguments("--ignore-certificate-errors");
                option.AddArguments("--ignore-ssl-errors");
                Browser = new ChromeDriver(iWoekDir, option);
            }
            else
            {
                FirefoxOptions option = new FirefoxOptions();
                option.AddArguments("--ignore-certificate-errors");
                option.AddArguments("--ignore-ssl-errors");
                Browser = new FirefoxDriver(iWoekDir, option);
            }
            Browser.Manage().Window.Maximize();
            Wait = new WebDriverWait(Browser, TimeSpan.FromSeconds(30));
            Browser.Navigate().GoToUrl("https://www.flickr.com/");
            Login login = new Login();
            login.LoginFlickr();

        }

        [OneTimeTearDown]
        public void TestFixrureTearDown()
        {
            generate.GenerateReports(9, "0", true);
            Browser.Quit();
        }
    }
}
