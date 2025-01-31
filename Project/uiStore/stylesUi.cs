using OpenQA.Selenium;

namespace UiStyles
{
    public class StylesUI
    {
        public static By Products = By.XPath("//span[contains(text(),'Products')]");
        public static By Styling = By.XPath("//*[@id=\"headerMenu\"]/div[2]/nav/div/div[2]/div/div/div/div/div[1]/div[1]/a[4]");
        public static By Explore = By.XPath("//body/div[@id='__next']/div[1]/main[1]/div[1]/div[2]/div[1]/div[2]/div[1]/a[1]");
    }
}
