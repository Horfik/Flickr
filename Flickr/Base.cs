using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace Report
{
    public class Base
    {
        protected static IWebDriver Browser;
        public static WebDriverWait Wait;
        public static string iTestNumCurrent = "";
        public static bool iExecTestGood = false;
        public static string iWoekDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static string iFolderResultTest = iWoekDir + @"\Report\";
        public static string iFolderScreen = iFolderResultTest + @"\Screen";
        public static string iPathReportFile = iFolderResultTest + @"Report.html";
        public static int iTestCountGood = 0;
        public static int iTestCountFail = 0;
    }
}
