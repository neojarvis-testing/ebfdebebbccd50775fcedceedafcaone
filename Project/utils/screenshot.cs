using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using ConfigurationFile;
using System.IO;

public class Screenshot
{
    public static string CaptureScreenshot(IWebDriver driver)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string screenshotDir = "/home/coder/project/workspace/Project/screenshot";
        if (!Directory.Exists(screenshotDir))
        {
            Directory.CreateDirectory(screenshotDir);
        }

        string screenshotFilename = $"screenshot_{timestamp}.png";
        string screenshotPath = Path.Combine(screenshotDir, screenshotFilename);
        string absoluteScreenshotPath = Path.GetFullPath(screenshotPath);

        try
        {
            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile(absoluteScreenshotPath);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save screenshot: {ex.Message}");
        }

        return absoluteScreenshotPath;
    }
}
