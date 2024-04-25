using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

class Program
{
    static void Main()
    {
        // Set up the driver
        IWebDriver driver = new ChromeDriver("./");
        
        // Navigate to the typing test website
        driver.Navigate().GoToUrl("https://humanbenchmark.com/tests/typing");

        // Give some time for the page to fully load
        driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
        driver.Manage().Window.Maximize(); // Maximizes the window to full screen
        #region
        IWebElement loginButton = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[3]/div/div[2]/a[2]"));
        loginButton.Click();

        IWebElement inputUser = WaitForElement(driver, By.CssSelector("#root > div > div.css-1f554t4.e19owgy74 > div > div > form > p:nth-child(1) > input[type=text]"), TimeSpan.FromSeconds(10));
        inputUser.SendKeys("Made by Layth Hammad");
        Thread.Sleep(1000);
        inputUser.Clear();
        inputUser.SendKeys("BOT49");

        IWebElement inputKey = WaitForElement(driver, By.CssSelector("#root > div > div.css-1f554t4.e19owgy74 > div > div > form > p:nth-child(2) > input[type=password]"), TimeSpan.FromSeconds(10));
        inputKey.SendKeys("Layth12+");

        IWebElement confirmButton = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[4]/div/div/form/p[3]/input"));
        confirmButton.Click();

        IWebElement typingButton = WaitForElement(driver, By.XPath("//*[@id=\"root\"]/div/div[4]/div/div/div[1]/div/div[5]"), TimeSpan.FromSeconds(10));
        typingButton.Click();

        IWebElement startButton = WaitForElement(driver, By.XPath("//*[@id=\"root\"]/div/div[4]/div/div/div[2]/div[2]/div/div[4]/a"), TimeSpan.FromSeconds(10));
        startButton.Click();

        IWebElement startTest = driver.FindElement(By.XPath("//*[@id=\"root\"]/div/div[4]/div[1]/div/div/div"));
        Thread.Sleep(1000);
        startTest.Click();
        #endregion

        // Get the page source
        string pageSource = driver.PageSource;

        // Use HtmlAgilityPack to parse the page source
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(pageSource);

        // Wait a second to make sure the webpage is focused
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
        wait.Until(driver => driver.FindElement(By.TagName("body")).Displayed);

        // Type the text
        IWebElement inputField = driver.FindElement(By.CssSelector("#root > div > div:nth-child(4) > div.e1q0za6r0.css-1c2t4mr.e19owgy77 > div > div:nth-child(2) > div"));
        string textToType = inputField.Text;
        inputField.SendKeys(textToType);

        Console.Clear();
        Console.WriteLine(textToType);

        static IWebElement WaitForElement(IWebDriver driver, By by, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }
    }
}
