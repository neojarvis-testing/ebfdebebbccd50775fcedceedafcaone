using OpenQA.Selenium;
using OpenQA.Selenium.Support.Events;
using System;
using System.IO;

namespace Utils
{
    public class WebDriverEventHandler
    {
        private readonly string _logFilePath = "/home/coder/project/workspace/log.log";
        private EventFiringWebDriver _driver;

        public WebDriverEventHandler(EventFiringWebDriver driver)
        {
            _driver = driver;
            // Register events
            _driver.ElementClicked += OnElementClicked;
            _driver.ElementClicking += OnElementClicking;
            // Add more event subscriptions here...
        }

        private void Log(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging message: {ex.Message}");
            }
        }

        private string GetElementInfo(IWebElement element)
        {
            string tagName = element.TagName;
            string textValue = element.Text;
            string attributeValue = string.Join(" ",  element.GetAttribute("name"));

            return $"{tagName}: {textValue} ({attributeValue})";
        }

        public void OnElementClicked(object sender, WebElementEventArgs e)
        {
            string elementInfo = GetElementInfo(e.Element);
            Log($"Clicked on element1111: {elementInfo}");
        }

        public void OnElementClicking(object sender, WebElementEventArgs e)
        {
            string elementInfo = GetElementInfo(e.Element);
            Log($"Clicking on element22222: {elementInfo}");
        }

        public void AfterClickOn(IWebElement element, IWebDriver driver)
        {
            Log($"After clicking on33333: {element.TagName}");
        }

        // Destructor to clean up event subscriptions
        ~WebDriverEventHandler()
        {
            if (_driver != null)
            {
                // Unregister events to avoid memory leaks
                _driver.ElementClicked -= OnElementClicked;
                _driver.ElementClicking -= OnElementClicking;
                // Unsubscribe from more events as needed...
            }
        }
    }
}
