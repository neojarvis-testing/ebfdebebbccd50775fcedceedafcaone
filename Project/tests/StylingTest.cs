using NUnit.Framework;
using OpenQA.Selenium;
using Project.pages;
using System;
using AventStack.ExtentReports;
using Utils;
using ConfigurationFile;

namespace stylingTest
{
    [TestFixture]
    public class StylingTest
    {
        private IWebDriver driver;
        private ExtentReports extentReport;
        private ExtentTest test;
        private StylingPage stylingPage;
        private ConfigReader configReader;
        private string url;

        public StylingTest(IWebDriver driver, ExtentReports extentReport) // Corrected constructor name
        {
            this.driver = driver;
            this.extentReport = extentReport;
            this.stylingPage = new StylingPage(driver, extentReport); 
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
            driver.Navigate().GoToUrl(url); 
        }

        [Test]
        public void TestMethod1()
        {
            stylingPage.clickStyles();
            stylingPage.ClickExplore();
        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}
