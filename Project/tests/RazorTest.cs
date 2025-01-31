using NUnit.Framework;
using OpenQA.Selenium;
using Project.pages;
using System;
using AventStack.ExtentReports;
using Utils;
using ConfigurationFile;

namespace razorTest
{
    [TestFixture]
    public class RazorTest
    {
        private IWebDriver driver;
        private ExtentReports extentReport;
        private ExtentTest test;
        private RazorPage razorPage;
        private ConfigReader configReader;
        private string url;

        public RazorTest(IWebDriver driver, ExtentReports extentReport)
        {
            this.driver = driver;
            this.extentReport = extentReport;
            this.razorPage = new RazorPage(driver, extentReport);
            this.configReader = new ConfigReader(); 
            this.url = configReader.GetUrl(); 
        }

        public void RunTests()
        {
            SetUp();
            TestMethod1();
            TearDown();
        }

        [SetUp]
        public void SetUp()
        {
            test = extentReport.CreateTest(TestContext.CurrentContext.Test.Name);
            driver.Navigate().GoToUrl(url); 
        }

        [Test]
        public void TestMethod1()
        {
           razorPage.ClickSearchBox();
           razorPage.SearchForRazor();
        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}
