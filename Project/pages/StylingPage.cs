using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;
using AventStack.ExtentReports;
using Utils;
using UiStyles;

namespace Project.pages
{
    public class StylingPage
    {
        private readonly IWebDriver driver;
        private readonly ExtentReports extentReport;
        private ExtentTest test;

        public StylingPage(IWebDriver driver, ExtentReports extentReport)
        {
            this.driver = driver;
            this.extentReport = extentReport; 
            test = extentReport.CreateTest("Test_StylingPage", "Test to StylingPage");
        }

        public void clickStyles()
        {
            try
            {
                IWebElement ele1 = driver.FindElement(StylesUI.Products);
                IWebElement ele2 = driver.FindElement(StylesUI.Styling);
                // Create an instance of Actions class
                Actions actions = new Actions(driver);
                actions.MoveToElement(ele1).Perform(); 
                actions.MoveToElement(ele2).Click().Perform(); 
                test.Pass("clicked Styles Button successfully.");
            }
            catch (Exception ex)
            {
                var screenshotPath = Reporter.CaptureScreenshotAsBase64(driver, "Test_SearchForRazor_Failure");
                test.AddScreenCaptureFromBase64String(screenshotPath, "Screenshot on Failure");
                test.Fail($"clicking Styles Button failed with exception: {ex.Message}");
            }
        }
    

       public void ClickExplore()
        {
            try
            {
                IWebElement clickButon = driver.FindElement(StylesUI.Explore);
                clickButon.Click();
                test.Pass("clicke Explore button successfully.");
            }
            catch (Exception ex)
            {
                var screenshotPath = Reporter.CaptureScreenshotAsBase64(driver, "Test_SearchForRazor_Failure");
                test.AddScreenCaptureFromBase64String(screenshotPath, "Screenshot on Failure");
                test.Fail($"clicke Explore button failed with exception: {ex.Message}");
            }
            
        }
    }
}
