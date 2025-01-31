using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using System;
using System.IO;

namespace Utils
{
    public static class Reporter
    {
        private static ExtentReports? _extentReport;
        private static string? _reportFilePath;

        public static ExtentReports GenerateExtentReport(string reportName = null)
        {
            if (_extentReport == null)
            {
                _extentReport = CreateExtentReport(reportName);
            }
            return _extentReport;
        }

        private static ExtentReports CreateExtentReport(string reportName)
        {
            var extentReport = new ExtentReports();

            // Define the report file path with the timestamp and provided report name
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            var reportFilePath = Path.Combine(Environment.CurrentDirectory, "Reports");
            _reportFilePath = Path.Combine(reportFilePath, $"{reportName}_{timestamp}.html");

            var sparkReporter = new ExtentSparkReporter(_reportFilePath);

            extentReport.AttachReporter(sparkReporter);

            extentReport.AddSystemInfo("Operating System", Environment.OSVersion.ToString());
            extentReport.AddSystemInfo("Username", Environment.UserName);
            extentReport.AddSystemInfo("Framework Version", Environment.Version.ToString());

            return extentReport;
        }

        public static string CaptureScreenshotAsBase64(IWebDriver driver, string screenshotName)
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            var screenshotDriver = (ITakesScreenshot)driver;
            var screenshot = screenshotDriver.GetScreenshot();

            var base64Screenshot = screenshot.AsBase64EncodedString;

            // Save the screenshot to a file for reference
            SaveScreenshotToFile(screenshot.AsByteArray, $"{screenshotName}_{timestamp}.png");

            return base64Screenshot;
        }

        private static void SaveScreenshotToFile(byte[] screenshotBytes, string fileName)
        {
            var screenshotsDirPath = Path.Combine(Environment.CurrentDirectory, "Reports", "ErrorScreenshots");
            Directory.CreateDirectory(screenshotsDirPath);

            var destinationScreenshotPath = Path.Combine(screenshotsDirPath, fileName);
            File.WriteAllBytes(destinationScreenshotPath, screenshotBytes);
        }
    }
}
