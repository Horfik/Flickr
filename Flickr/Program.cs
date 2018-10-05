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
using NUnit.Framework.Interfaces;
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
    class Test : Base
    {
        public static void Main(string[] args)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            NUnit.ConsoleRunner.Program.Main(new[] { path });
        }

        public static Explore explore;
        public static PhotoPage photo;
        public static Author author;
        public static Alboms alboms;
        public static Check check;
        public static Groups group;
        public static Galleries galleries;
        public static GenerateReport generate = new GenerateReport();
        public static string exception = "";

        [OneTimeSetUp]
        public void Init()
        {
            check = new Check();
            photo = new PhotoPage();
            explore = new Explore();
            author = new Author();
            alboms = new Alboms();
        }

        [SetUp]
        public void Setup()
        {
            Browser.Navigate().GoToUrl("https://www.flickr.com/");
        }

        [TearDown]
        public void gen()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Error ||
                TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                string exception = TestContext.CurrentContext.Result.Message + "\n" + TestContext.CurrentContext.Result.StackTrace;
                generate.GenerateReports(1, iTestNumCurrent, false, exception);
            }
            else generate.GenerateReports(1, iTestNumCurrent, true);

        }

        [TestCase]
        public void Test_1_login()
        {
            iTestNumCurrent = "1-Login";             
            Browser.FindElement(By.CssSelector("div.avatar")).Click();
            string Name;
            string NameAccount = ConfigurationManager.AppSettings.Get("NameAccount");
            if (Browser.Url == "https://www.flickr.com/")
            {                 
                Name = Browser.FindElement(By.CssSelector("div.title h4")).Text;
            }
            else
            {
                Name = Browser.FindElement(By.ClassName("email")).Text;
            }
            StringAssert.Contains(NameAccount, Name);       
        }

        [TestCase]
        public void Test_2_Title()
        {
            iTestNumCurrent = "2-Title";           
            System.Threading.Thread.Sleep(1000);
            Assert.AreEqual(ConfigurationManager.AppSettings.Get("HomePageName"), Browser.Title);              
        }
        
        [TestCase]
        public void Test_3_RootItem()
        {
            iTestNumCurrent = "3-RootItem";            
            Home_page home = new Home_page();
            Assert.AreEqual(4, home.Item());      
        }
        
        [TestCase]
        public void Test_4_Linc()
        {
            iTestNumCurrent = "4-Linc";            
            Check check = new Check();
            Home_page home = new Home_page();
            System.Threading.Thread.Sleep(2000);
            Assert.True(home.ElementsConteinLinc());              
        }
        
        [TestCase]
        public void Test_5_Name()
        {
            iTestNumCurrent = "5-Name";
            YouPage youPage = new YouPage();
            youPage.NavigateYouPage();       
            string name = ConfigurationManager.AppSettings.Get("UserName");
            Assert.AreEqual(name, youPage.UserName.Text);              
        }
        
        [TestCase]
        public void Test_6_Label()
        {
            
            iTestNumCurrent = "6-Label";           
            explore.NavigateExplore();               
            Assert.True(explore.Label());              
        }
        
        [TestCase]
        public void Test_7_Photo()
        {
            iTestNumCurrent = "7-PhotoTitle";
            explore.NavigateExplore();           
            System.Threading.Thread.Sleep(10000);
            explore.Photo[0].Click();
            string Url1 = Browser.Url;
            explore.NavigateExplore();
            System.Threading.Thread.Sleep(3000);
            explore.PhotoTitle[0].Click();
            string Url2 = Browser.Url;
            Assert.True(Url1 == Url2);                
        }
        
        [TestFixture]
        public class PhotoDetails
        {
            [OneTimeSetUp]
            public void Init()
            {
                check = new Check();
                photo = new PhotoPage();
                explore = new Explore();
                Browser.Navigate().GoToUrl("https://www.flickr.com/explore");
                Browser.FindElement(By.CssSelector("a.overlay")).Click();
                Wait.Until(ExpectedConditions.ElementToBeClickable(explore.Photo[0]));
            }

            [TearDown]
            public void gen()
            {
                if (TestContext.CurrentContext.Result.Outcome == ResultState.Error ||
                    TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
                {
                    string exception = TestContext.CurrentContext.Result.Message + "\n" + TestContext.CurrentContext.Result.StackTrace;
                    generate.GenerateReports(1, iTestNumCurrent, false, exception);
                }
                else generate.GenerateReports(1, iTestNumCurrent, true);
            }

            [TestCase]
            public void FormatData()
            {
                string regex = @"\w*Taken on|Uploaded on\s*\w*July|Juny|October\[1-9]{1,2}\,\s\d{4}";
                Assert.True(Regex.IsMatch(Browser.FindElement(By.ClassName("date-taken-label")).Text, regex));
            }

            [TestCase]
            public void Name()
            {
                iTestNumCurrent = "Name"; 
                Assert.True(photo.PhotoName != null);
                Assert.True(photo.Author != null);
                Assert.True(Browser.Title.Contains(photo.PhotoName.Text));                
            }

            [TestCase]
            public void FollowButton()
            {
                iTestNumCurrent = "FollowButton";         
                Assert.True(photo.Follow != null);              
            }

            [TestCase]
            public void Number()
            {
                iTestNumCurrent = "Number";
                Assert.True(photo.Numbers != null);              
            }

            [TestCase]
            public void RIghtsLinc()
            {
                iTestNumCurrent = "Rights";
                Assert.True(photo.Rightlinc.GetAttribute("href") != null);               
            }

            [TestCase]
            public void Tags()
            {
                iTestNumCurrent = "Tags";
                Assert.True(photo.ElementsConteinLinc());
                Assert.True(photo.ElementsConteinLinc2());               
            }

            [TestCase]
            public void Camera()
            {
                iTestNumCurrent = "Camera details";         
                Assert.True(photo.Camera != null);              
            }

        }
        
        [TestCase]
        public void Test_9_Authors()
        {
            iTestNumCurrent = "9-Authors";
            Browser.Navigate().GoToUrl("https://www.flickr.com/explore");
            Browser.FindElement(By.CssSelector("a.overlay")).Click();      
            string Name = photo.Author.Text;
            photo.Author.Click();
            System.Threading.Thread.Sleep(5000);
            Assert.True(Browser.Title.Contains(author.AuthorPhoto.Text));
            Assert.AreEqual(Name, author.AuthorPhoto.Text);           
        }

        [TestFixture]
        public class TestAlboms
        {
            [OneTimeSetUp]
            public void Init()
            {
                explore = new Explore();
                photo = new PhotoPage();
                alboms = new Alboms();
                check = new Check();
                Browser.Navigate().GoToUrl("https://www.flickr.com/explore");
                Browser.FindElement(By.CssSelector("a.overlay")).Click();
                Wait.Until(ExpectedConditions.ElementToBeClickable(explore.Photo[0]));
                photo.Author.Click();
                alboms.GoAlbom.Click();
                System.Threading.Thread.Sleep(5000);
            }

            [TearDown]
            public void gen()
            {
                if (TestContext.CurrentContext.Result.Outcome == ResultState.Error ||
                    TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
                {
                    string exception = TestContext.CurrentContext.Result.Message + "\n" + TestContext.CurrentContext.Result.StackTrace;
                    generate.GenerateReports(1, iTestNumCurrent, false, exception);
                }
                else generate.GenerateReports(1, iTestNumCurrent, true);
            }

            [TestCase]
            public void Thumbnail()
            {
                iTestNumCurrent = "Thumnail";
                Assert.True(check.ElementsConteinAttribute(alboms.Thumbnail, "style"));          
            }

            [TestCase]
            public void AlbomLinc()
            {
                iTestNumCurrent = "AlbumLinc";
                Assert.True(check.ElementsConteinAttribute(alboms.Linc, "href"));              
            }

            [TestCase]
            public void AlbomNumber()
            {
                iTestNumCurrent = "Album number";        
                Assert.AreEqual(alboms.Name.Count(), alboms.Photo.Count());
                Assert.AreEqual(alboms.Name.Count(), alboms.View.Count());
            }

            [TestCase]
            public void AlbomName()
            {
                iTestNumCurrent = "Album title";
                Assert.True(check.Elements(alboms.Name));
            }
        }

        [TestFixture]
        public class TestGroups
        {
            [OneTimeSetUp]
            public void Init()
            {
                Browser.Navigate().GoToUrl("https://www.flickr.com/groups");
                group = new Groups();
                check = new Check();
            }

            [TearDown]
            public void gen()
            {
                if (TestContext.CurrentContext.Result.Outcome == ResultState.Error ||
                    TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
                {
                    string exception = TestContext.CurrentContext.Result.Message + "\n" + TestContext.CurrentContext.Result.StackTrace;
                    generate.GenerateReports(1, iTestNumCurrent, false, exception);
                }
                else generate.GenerateReports(1, iTestNumCurrent, true);
            }

            [TestCase]
            public void CreateGroups()
            {
                iTestNumCurrent = "Greate groups";
                Assert.True(group.GreateGroups != null);              
            }

            [TestCase]
            public void Join()
            {
                iTestNumCurrent = "Join";
                Assert.True(check.Elements(group.Join));               
            }

            [TestCase]
            public void NameGroups()
            {
                iTestNumCurrent = "Name groups";
                Assert.True(check.Elements(group.Name));              
            }

            [TestCase]
            public void NumbersGrups()
            {
                iTestNumCurrent = "Numbers groups";
                Assert.True(check.Elements(group.Numbers));               
            }
        }

        [TestFixture]
        public class TestGalleries
        {
            [OneTimeSetUp]
            public void Init()
            {
                Browser.Navigate().GoToUrl("https://www.flickr.com/photos/flickr/galleries");
                galleries = new Galleries();
                check = new Check();
            }

            [TearDown]
            public void gen()
            {
                if (TestContext.CurrentContext.Result.Outcome == ResultState.Error ||
                    TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
                {
                    string exception = TestContext.CurrentContext.Result.Message + "\n" + TestContext.CurrentContext.Result.StackTrace;
                    generate.GenerateReports(1, iTestNumCurrent, false, exception);
                }
                else generate.GenerateReports(1, iTestNumCurrent, true);
            }

            [TestCase]
            public void GalleriesName()
            {
                iTestNumCurrent = "Name galleries";
                Assert.True(check.ElementsConteinAttribute(galleries.Name, "title"));                
            }

            [TestCase]
            public void GalleriesNumbers()
            {
                iTestNumCurrent = "NUmbers of galleries";
                Assert.True(check.Elements(galleries.Numbers));              
            }

        }

        [TestCase]
        public void Test12_Search()
        {
            iTestNumCurrent = "12-Search";
            Search search = new Search();
            search.SearchFlickr();
            Assert.True(search.Contein() > 0);        
        }
    }
}
