using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Events;
using System;
using Utils;
using razorTest;
using stylingTest;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            var extentReport = Reporter.GenerateExtentReport("RazorTestReport");

            Uri seleniumHubUrl = new Uri("https://4444-badabeeddccbcfeefcabbdcdefafeaf.project.examly.io/");
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--no-sandbox");

            IWebDriver baseDriver = new RemoteWebDriver(seleniumHubUrl, options.ToCapabilities(), TimeSpan.FromSeconds(10));
            baseDriver.Manage().Window.Maximize();

            RazorTest razorTest = new RazorTest(baseDriver, extentReport);
            razorTest.RunTests();

            StylingTest stylingTest = new StylingTest(baseDriver, extentReport);
            stylingTest.RunTests();

            extentReport.Flush();
            baseDriver.Quit();
        }
    }
}
