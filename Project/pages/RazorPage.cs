using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;
using AventStack.ExtentReports;
using Utils;
using UiRaozr;


namespace Project.pages
{
    public class RazorPage
    {
        private readonly IWebDriver driver;
        private IJavaScriptExecutor js;
        private string cellValue = ""; 
        private readonly ExtentReports extentReport;
        private ExtentTest test;
        public RazorPage(IWebDriver driver, ExtentReports extentReport)
        {
            this.driver = driver;
            this.js = (IJavaScriptExecutor)driver;
            this.extentReport = extentReport; 
            test = extentReport.CreateTest("Test_SearchForRazor", "Test to search for a razor");


            ExcelReader excelReader = new ExcelReader();
            string excelFilePath = "/home/coder/project/workspace/Project/data/Datasheet.xlsx";
            string sheetName = "Sheet1";
            List<Dictionary<string, string>> excelData = excelReader.GetData(excelFilePath, sheetName);
            if (excelData.Count > 0) 
            {
                cellValue = excelData[0]["keys"] ?? "";
                Console.WriteLine($"Value of cell A2 (keys): {cellValue}");
            }
            else
            {
                Console.WriteLine("The Excel sheet does not have enough rows.");
            }
        }

        public void ClickSearchBox()
        {
            
            try
            {
                IWebElement clickSearchBox = driver.FindElement(RazorUI.SearchIcon);
                clickSearchBox.Click();
                var screenshotPath = Reporter.CaptureScreenshotAsBase64(driver, "clicked Search Bar successfully");
                test.AddScreenCaptureFromBase64String(screenshotPath, "Screenshot on Pass");
                test.Pass("clicked Search Bar successfully.");
            }
            catch (Exception ex)
            {
                var screenshotPath = Reporter.CaptureScreenshotAsBase64(driver, "Test_SearchForRazor_Failure");
                test.AddScreenCaptureFromBase64String(screenshotPath, "Screenshot on Failure");
                test.Fail($"clicking Search Ba failed with exception: {ex.Message}");
            }
        }

        public void SearchForRazor()
        {
            try
            {
                IWebElement searchBox = driver.FindElement(RazorUI.SearchBox);
                searchBox.SendKeys(cellValue);
                IWebElement selectRazor = driver.FindElement(RazorUI.SelectRazor);
                selectRazor.Click();
                string screenshotPath = Screenshot.CaptureScreenshot(driver);
                test.Pass("razor keys sent sucessfuly successfully.");
            }
            catch (Exception ex)
            {
                var screenshotPath = Reporter.CaptureScreenshotAsBase64(driver, "Test_SearchForRazor_Failure");
                test.AddScreenCaptureFromBase64String(screenshotPath, "Screenshot on Failure");
                test.Fail($"razor keys sentr failed with exception: {ex.Message}");
            }
        }
    }
}
