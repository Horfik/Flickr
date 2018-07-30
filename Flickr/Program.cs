using System;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Reflection;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Compatibility;
using Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.UI;
using Report.Pages;
using OpenQA.Selenium.Support.PageObjects;



namespace Report
{
    [TestFixture]
    class Test
    {
        public static void Main(string[] args)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            NUnit.ConsoleRunner.Program.Main(new[] { path });
        }

        public static IWebDriver Browser;
        public static string iTestNumCurrent = "";
        public static bool iExecTestGood = false;
        public static string iWoekDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static string iFolderResultTest = iWoekDir + @"\Report\";
        public static string iFolderScreen = iFolderResultTest + @"\Screen";
        public static string iPathReportFile = iFolderResultTest + @"Report.html";
        public static int iTestCountGood = 0;
        public static int iTestCountFail = 0;
        public static Elements elements = new Elements();

        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            GenerateReport(0, "0", true);
            int i = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Browser"));
            if (i == 1)
            {
                ChromeOptions option = new ChromeOptions();
                //FirefoxOptions option = new FirefoxOptions();
                option.AddArguments("--ignore-certificate-errors");
                option.AddArguments("--ignore-ssl-errors");
                Browser = new ChromeDriver(iWoekDir, option);
                //Browser = new FirefoxDriver(iWoekDir, option);
            }
            else
            {
                FirefoxOptions option = new FirefoxOptions();
                option.AddArguments("--ignore-certificate-errors");
                option.AddArguments("--ignore-ssl-errors");
                Browser = new FirefoxDriver(iWoekDir, option);
            }
            Browser.Manage().Window.Maximize();
            Browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Browser.Navigate().GoToUrl("https://www.flickr.com/");
            PageFactory.InitElements(Browser, elements);

        }

        [OneTimeTearDown]
        public void TestFixrureTearDown()
        {
            GenerateReport(9, "0", true);
            Browser.Quit();
        }

        [TestCase]
        public void Test_1_login()
        {
            iTestNumCurrent = "1-Login";
            try
            {
                IWebElement log_in = Browser.FindElement(By.ClassName("gn-title"));
                log_in.Click();
                IWebElement email = Browser.FindElement(By.Name("username"));
                string Useremail = ConfigurationManager.AppSettings.Get("email");
                string UserPassword = ConfigurationManager.AppSettings.Get("password");
                email.SendKeys(Useremail + OpenQA.Selenium.Keys.Enter);
                IWebElement Password = Browser.FindElement(By.Name("password"));
                Password.SendKeys(UserPassword + OpenQA.Selenium.Keys.Enter);
                IWebElement element = Browser.FindElement(By.CssSelector("div.avatar"));
                element.Click();
                string Name;
                string NameAccount = ConfigurationManager.AppSettings.Get("NameAccount");
                if (Browser.Url == "https://www.flickr.com/")
                {                 
                    Name = Browser.FindElement(By.CssSelector("div.title h4")).Text;
                    Browser.FindElement(By.ClassName("fluid-droparound-overlay")).Click();
                }
                else
                {
                    Name = Browser.FindElement(By.ClassName("email")).Text;
                }
                StringAssert.Contains(NameAccount, Name);
                GenerateReport(1, iTestNumCurrent, true);
                iExecTestGood = true;
            }
            catch (Exception ex)
            {
                GenerateReport(1, iTestNumCurrent, false, ex.ToString());
                iExecTestGood = false;
            }
            Assert.True(iExecTestGood);
        }

        [TestCase]
        public void Test_2_Title()
        {

            iTestNumCurrent = "2-Title";
            try
            {
                elements.Flickr.Click();
                System.Threading.Thread.Sleep(3000);
                Assert.AreEqual("Home | Flickr", Browser.Title);
                GenerateReport(1, iTestNumCurrent, true);
                iExecTestGood = true;
            }
            catch (Exception ex)
            {
                GenerateReport(1, iTestNumCurrent, false, ex.ToString());
                iExecTestGood = false;
            }
            Assert.True(iExecTestGood);
        }

        [TestCase]
        public void Test_3_RootItem()
        {
            iTestNumCurrent = "3-RootItem";
            try
            {
                List<IWebElement> items = Browser.FindElements(By.CssSelector("ul.nav-menu > li > a")).ToList();
                Assert.AreEqual(3, items.Count());
                GenerateReport(1, iTestNumCurrent, true);
                iExecTestGood = true;
            }
            catch (Exception ex)
            {
                GenerateReport(1, iTestNumCurrent, false, ex.ToString());
                iExecTestGood = false;
            }
            Assert.True(iExecTestGood);
        }

        [TestCase]
        public void Test_4_Linc()
        {
            iTestNumCurrent = "4-Linc";
            try
            {
                System.Threading.Thread.Sleep(2000);
                Assert.True(ElementContainsLinc("ul.nav-menu ul a"));
                GenerateReport(1, iTestNumCurrent, true);
                iExecTestGood = true;
            }
            catch (Exception ex)
            {
                GenerateReport(1, iTestNumCurrent, false, ex.ToString());
                iExecTestGood = false;
            }
            Assert.True(iExecTestGood);
        }

        [TestCase]
        public void Test_5_Name()
        {
            iTestNumCurrent = "5-Name";
            elements.you.Click();
            try
            {
                string name = ConfigurationManager.AppSettings.Get("UserName");
                Assert.AreEqual(name, Browser.FindElement(By.ClassName("truncate")).Text);
                GenerateReport(1, iTestNumCurrent, true);
                iExecTestGood = true;
            }
            catch (Exception ex)
            {
                GenerateReport(1, iTestNumCurrent, false, ex.ToString());
                iExecTestGood = false;
            }
            Assert.True(iExecTestGood);
        }

        [TestCase]
        public void Test_6_Label()
        {
            elements.explore.Click();
            iTestNumCurrent = "6-Label";
            try
            {
                List<IWebElement> Label = Browser.FindElements(By.CssSelector("a.overlay")).ToList();
                bool l = false;
                for (int i = 0; i < Label.Count(); i++)
                {
                    if (Label[i].GetAttribute("aria-label") != null)
                        l = true;
                    else
                    {
                        l = false;
                        break;
                    }
                }
                Assert.True(l);
                GenerateReport(1, iTestNumCurrent, true);
                iExecTestGood = true;
            }
            catch (Exception ex)
            {
                GenerateReport(1, iTestNumCurrent, false, ex.ToString());
                iExecTestGood = false;
            }
            Assert.True(iExecTestGood);
        }

        [TestCase]
        public void Test_7_PhotoTitle()
        {
            iTestNumCurrent = "7-PhotoTitle";
            try
            {
                List<IWebElement> name = Browser.FindElements(By.CssSelector("a.overlay")).ToList();
                string namep = name[0].GetAttribute("aria-label");
                //name[0].Click();
                Browser.Navigate().GoToUrl(name[0].GetAttribute("href"));
                string NamePhoto = Browser.FindElement(By.ClassName("photo-title")).Text;
                Assert.True(namep.Contains(NamePhoto));
                GenerateReport(1, iTestNumCurrent, true);
                iExecTestGood = true;
            }
            catch (Exception ex)
            {
                GenerateReport(1, iTestNumCurrent, false, ex.ToString());
                iExecTestGood = false;
            }
            Assert.True(iExecTestGood);
        }

        [TestCase]
        public void Test_8_PhotoDetails()
        {
            iTestNumCurrent = "8-PhotoDetails";
            string data = Browser.FindElement(By.ClassName("date-taken-label")).Text;
            DateTime data2 = Convert.ToDateTime(data.Substring(9));
            try
            {
                int k = 0;
                if (Browser.FindElement(By.ClassName("photo-title")) != null
                    & Browser.Title.Contains(Browser.FindElement(By.ClassName("photo-title")).Text)
                    & Browser.FindElement(By.XPath(".//div[@class='attribution-info']/a")) != null)
                    k += 1;

                if (Browser.FindElement(By.XPath(".//button[@class='unfluid follow ui-button ui-button-icon']")) != null)
                    k += 1;

                if (Browser.FindElement(By.ClassName("sub-photo-right-stats-view")) != null)
                    k += 1;

                //if (data.Contains( Convert.ToString(String.Format("{0:MMMM dd, yyyy}", data2))))
                if (Regex.IsMatch(data, "Taken on " + String.Format("{0:MMMM dd, yyyy}", data2)))
                    k += 1;

                if (Browser.FindElement(By.ClassName("photo-license-url")).GetAttribute("href") != null)
                    k += 1;

                if (Browser.FindElement(By.ClassName("photo-charm-exif-scrappy-view")) != null)
                    k += 1;

                if (ElementContainsLinc("ul.tags-list a"))
                    k += 1;
                Assert.AreEqual(7, k);
                GenerateReport(1, iTestNumCurrent, true);
                iExecTestGood = true;
            }
            catch (Exception ex)
            {
                GenerateReport(1, iTestNumCurrent, false, ex.ToString());
                iExecTestGood = false;
            }
            Assert.True(iExecTestGood);
        }

        [TestCase]
        public void Test_9_Authors()
        {
            iTestNumCurrent = "9-Authors";
            try
            {
                string NameInPhoto = Browser.FindElement(By.CssSelector("div.clear-float a.owner-name")).Text;
                System.Threading.Thread.Sleep(10000);
                string a = Browser.FindElement(By.ClassName("owner-name-with-by")).GetAttribute("href");
                Browser.Navigate().GoToUrl(a);
                Assert.True(Browser.Title.Contains(Browser.FindElement(By.CssSelector("h1.truncate")).Text));
                Assert.AreEqual(NameInPhoto, Browser.FindElement(By.CssSelector("h1.truncate")).Text);
                GenerateReport(1, iTestNumCurrent, true);
                iExecTestGood = true;
            }
            catch (Exception ex)
            {
                GenerateReport(1, iTestNumCurrent, false, ex.ToString());
                iExecTestGood = false;
            }
            Assert.True(iExecTestGood);
        }

        [TestCase]
        public void Test10_Alboms()
        {
            iTestNumCurrent = "10-Alboms";
            try
            {
                Browser.FindElement(By.CssSelector("#albums a")).Click();
                System.Threading.Thread.Sleep(3000);
                List<IWebElement> Stile = Browser.FindElements(By.CssSelector("div.photo-list-view div.awake")).ToList();
                List<IWebElement> Linc = Browser.FindElements(By.CssSelector("a.overlay")).ToList();
                List<IWebElement> Name = Browser.FindElements(By.CssSelector("h4.album-title")).ToList();
                List<IWebElement> Photo = Browser.FindElements(By.ClassName("album-photo-count")).ToList();
                List<IWebElement> Views = Browser.FindElements(By.ClassName("album-views-label")).ToList();
                bool l = false;
                for (int i = 0; i < Stile.Count(); i++)
                {
                    if (Stile[i].GetAttribute("style") != null)
                        l = true;
                    else
                    {
                        l = false;
                        break;
                    }

                    if (Linc[i].GetAttribute("href") != null)
                        l = true;
                    else
                    {
                        l = false;
                        break;
                    }

                    if ((Name.Count & Photo.Count & Views.Count) == Stile.Count)
                        l = true;
                    else
                    {
                        l = false;
                        break;
                    }
                }
                Assert.True(l);
                GenerateReport(1, iTestNumCurrent, true);
                iExecTestGood = true;
            }
            catch (Exception ex)
            {
                GenerateReport(1, iTestNumCurrent, false, ex.ToString());
                iExecTestGood = false;
            }
            Assert.True(iExecTestGood);
        }

        [TestCase]
        public void Test11_Groups()
        {
            iTestNumCurrent = "11-Groups";
            elements.you.Click();
            System.Threading.Thread.Sleep(3000);
            try
            {
                Browser.FindElement(By.CssSelector("#groups a")).Click();
                System.Threading.Thread.Sleep(3000);
                List<IWebElement> Groups = new List<IWebElement>();
                Groups.Add(Browser.FindElement(By.ClassName("create-group")));
                Groups.Add(Browser.FindElement(By.ClassName("next-page")));
                Assert.AreEqual(2, Groups.Count());
                List<IWebElement> JoinButton = Browser.FindElements(By.CssSelector("div.recommended-group button")).ToList();
                Assert.AreEqual(6, JoinButton.Count());
                List<IWebElement> NameGroups = Browser.FindElements(By.CssSelector("div.text-content div.title")).ToList();
                Assert.AreEqual(6, NameGroups.Count());
                List<IWebElement> Numbers = Browser.FindElements(By.CssSelector("div.links i")).ToList();            
                Assert.AreEqual(18, Numbers.Count());
                GenerateReport(1, iTestNumCurrent, true);
                iExecTestGood = true;
            }
            catch (Exception ex)
            {
                GenerateReport(1, iTestNumCurrent, false, ex.ToString());
                iExecTestGood = false;
            }
            Assert.True(iExecTestGood);
        }

        [TestCase]
        public void Test13_Galleries()
        {
            iTestNumCurrent = "13-Galleries";
            Browser.Navigate().GoToUrl("https://www.flickr.com/photos/flickr/galleries");
            try
            {
                List<IWebElement> Name = Browser.FindElements(By.CssSelector("h4 a.Seta")).ToList();
                List<IWebElement> Numbers = Browser.FindElements(By.CssSelector("div.gallery-case-meta p")).ToList();
                Assert.AreEqual(36, Numbers.Count());
                bool l = false;
                for (int i=0; i<Name.Count(); i++)
                {
                    if (Name[i].GetAttribute("title") != null)
                        l = true;
                    else { l = false; break; }
                }
                Assert.True(l);
                GenerateReport(1, iTestNumCurrent, true);
                iExecTestGood = true;
            }
            catch (Exception ex)
            {
                GenerateReport(1, iTestNumCurrent, false, ex.ToString());
                iExecTestGood = false;
            }
            Assert.True(iExecTestGood);
            
        }

        [TestCase]
        public void Test12_Search()
        {
            iTestNumCurrent = "12-Search";
            string KeyWord = ConfigurationManager.AppSettings.Get("WordForSearch");
            try
            {
                elements.Search.SendKeys(KeyWord + OpenQA.Selenium.Keys.Enter);
                List<IWebElement> ResultSearch = Browser.FindElements(By.CssSelector("div.photo-list-photo-view div.interaction-bar")).ToList();
                int k = 0;
                for (int i = 0; i < ResultSearch.Count(); i++)
                {
                    if (ResultSearch[i].GetAttribute("title").Contains(KeyWord))
                        k += 1;
                }
                Assert.True(k > 0);
                GenerateReport(1, iTestNumCurrent, true);
                iExecTestGood = true;
            }
            catch (Exception ex)
            {
                GenerateReport(1, iTestNumCurrent, false, ex.ToString());
                iExecTestGood = false;
            }
            Assert.True(iExecTestGood);
        }

        public bool ElementContainsLinc(string CssS)
        {
            List<IWebElement> Lincs = Browser.FindElements(By.CssSelector(CssS)).ToList();
            bool l = false;
            for (int i = 0; i < Lincs.Count(); i++)
            {
                if (Lincs[i].GetAttribute("href") != null)
                    l = true;
                else
                {
                    l = false;
                    break;
                }
            }
            return l;
        }

        public static void GenerateReport(int iStep, string iTestNum, bool iResult, string iMessage = "-")
        {
            iMessage = iMessage.Replace("<", "&lt;").Replace(">", "&lt");
            iMessage = iMessage.Replace("\n", "/br");
            string iTime = String.Format("{0:HH:mm:ss}", DateTime.Now);
            FileStream fs = new FileStream(iPathReportFile, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            if (iStep == 0)
            {
                iTestCountGood = 0;
                iTestCountFail = 0;
                sw.WriteLine(@"<!DOCTYPE html>" + "\n" +
                    @"<html lang='ru-RU'>" + "\n" +
                    @"<head>" + "\n" +
                    @"<meta http-equiv='Content-Type' content='text/html; charset=UTF-8' />" + "\n" +
                    @"<title>Отчёт о тестировании</title>" + "\n" +
                    @"</head>" + "\n" +
                    @"<body>" + "\n" +
                    @"<div style='font-size:22px;' align='center'><strong>Тест начат: " + String.Format("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now) + @"</strong></div></br>" + "\n" +
                    @"<table border='1' align='center' cellpadding='5' cellspacing='0' width='100%'>" + "\n" +
                    @"<tr style='text-align:center;'>" + "\n" +
                    @"<td width='100px'><strong>Тест</strong></td>" + "\n" +
                    @"<td width='70px'><strong>Результат</strong></td>" + "\n" +
                    @"<td width='70px'><strong>Время</strong></td>" + "\n" +
                    @"<td width='70px'><strong>Снимок</strong></td>" + "\n" +
                    @"<td><strong>Сообщение</strong></td>" + "\n" +
                    @"</tr>");
            }
            if (iResult == true & iStep == 1)
            {
                iTestCountGood += 1;
                sw.WriteLine(@"<tr style='color: green;'>" + "\n" +
                    @"<td>" + iTestNum + @"</td>" + "\n" +
                    @"<td>Успех</td>" + "\n" +
                    @"<td>" + iTime + @"</td>" + "\n" +
                    @"<td>-</td>" + "\n" +
                    @"<td>" + iMessage + @"</td>" + "\n" +
                    @"</tr>" + "\n");
            }
            if (iResult == false & iStep == 1)
            {
                iTestCountFail += 1;
                ITakesScreenshot screenshootdrivers = Test.Browser as ITakesScreenshot;
                Screenshot screenshot = screenshootdrivers.GetScreenshot();
                //string iNameScreen = iTestNum + "_" + String.Format("{0:yyyy:-MM-dd_HH--mm-ss}", DateTime.Now);
                //screenshot.SaveAsFile("1213.png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                //screenshot.SaveAsFile("d:\\page.png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                screenshot.SaveAsFile(iFolderScreen + @"\" + iTestNum + ".png", OpenQA.Selenium.ScreenshotImageFormat.Png);
                sw.WriteLine(@"<tr style='color: red;'>" + "\n" + 
                    @"<td>" + iTestNum + @"</td>" + "\n" +
                    @"<td>Провал</td>" + "\n" +
                    @"<td>" + iTime + @"</td>" + "\n" +
                    @"<td><center><a target='_blank' href='screen/" + iTestNum + ".png" + @"'>скриншот</a><br/><br/><a href='" + Browser.Url + @"' target='_blank'>URL</a></center></td>" + "\n" +
                    @"<td>" + iMessage + @"</td>" + "\n" +
                    @"</tr>" + "\n");
            }
            if (iStep == 9)
            {
                Decimal iProcent = 0;
                if (iTestCountGood > 0 || iTestCountFail > 0)
                {
                    iProcent = ((100 * iTestCountGood) / (iTestCountGood + iTestCountFail));
                }
                sw.WriteLine(@"<tr style='text-align:center;'>" + "\n" +
                    @"<td colspan='5'>&nbsp;</center></td>" + "\n" +
                    @"</tr>" + "\n" +
                    @"<tr style='text-align:center;'>" + "\n" +
                    @"<td colspan='5'>Всего тестов запущено: " + (iTestCountGood + iTestCountFail) + " || <span style='color: green;'>Успешно: " + iTestCountGood +
                             "</span> || <span style='color: red;'>Провалено: " + iTestCountFail + "</span> || Процент пройденных: " + iProcent + "% || Тест завершён: " + String.Format("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now) + "</td>" + "\n" +
                    @"</tr>" + "\n" +
                    @"</table>" + "\n" +
                    @"</body>" + "\n" +
                    @"</html>");
            }
            sw.Close();
        }
    }
}
