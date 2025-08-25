using NUnit.Framework;      // Imports the NUnit framework for testing attributes like [Test], [SetUp], etc.
using OpenQA.Selenium;      // Imports core Selenium components like IWebDriver and By.
using OpenQA.Selenium.Chrome; // Imports the ChromeDriver class to control the Chrome browser.
using OpenQA.Selenium.Support.UI; // Imports support classes, including WebDriverWait for explicit waits.
using System;                 // Imports basic .NET system functionality, used here for TimeSpan.

// Define a namespace to organize the code and prevent naming conflicts.
namespace SeleniumTestProject
{
    // Define the main test class where all the tests will reside.
    public class Tests
    {
        // Declare a private variable for the WebDriver instance.
        // It's of type IWebDriver to allow for flexibility (e.g., switching to FirefoxDriver later).
        private IWebDriver driver;

        // This method is marked with [SetUp]. NUnit will automatically run this method
        // before each and every [Test] method in this class.
        [SetUp]
        public void Setup()
        {
            // Initialize the 'driver' variable with a new instance of ChromeDriver.
            // This action opens a new Google Chrome browser window.
            driver = new ChromeDriver();

            // Access the browser window's properties and maximize it to full screen.
            driver.Manage().Window.Maximize();
        }


        // This method is marked with [Test], indicating it is an automated test case
        // that NUnit should execute.
        [Test]
        public void Search_OnDuckDuckGo_ShouldReturnResults()
        {
            // Command the browser to navigate to the specified URL.
            driver.Navigate().GoToUrl("https://duckduckgo.com");

            // Find the search input element on the page.
            // We use By.Name("q") because the search input field has an HTML attribute 'name="q"'.
            var searchBox = driver.FindElement(By.Name("q"));

            // Type the string "Selenium C#" into the located search box element.
            searchBox.SendKeys("Selenium C#");

            // Submit the form containing the search box. This is equivalent to pressing the Enter key.
            searchBox.Submit();

            // Create a WebDriverWait instance. This is an "explicit wait" mechanism.
            // It tells Selenium to wait for a certain condition to be met for a maximum of 10 seconds.
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Use the 'wait' object to pause execution until a specific condition is true.
            // The condition here is to find at least one element that matches the CSS selector "a.result__a".
            // This selector looks for an anchor tag <a> with the class "result__a", which is used for search result links on DuckDuckGo.
            // If the element doesn't appear within 10 seconds, a TimeoutException will be thrown.
            wait.Until(d => d.FindElement(By.CssSelector("a.result__a")));

            // After confirming at least one result exists, find ALL elements matching the selector.
            // FindElements (plural) returns a list of all matching web elements.
            var results = driver.FindElements(By.CssSelector("a.result__a"));

            // Use NUnit's Assert class to verify the test outcome.
            // Assert.IsTrue checks if the provided condition is true.
            // The condition is 'results.Count > 0', which checks if the list of results contains at least one item.
            // If the condition is false, the test will fail and display the message "Không tìm thấy kết quả nào!".
            Assert.IsTrue(results.Count > 0, "Không tìm thấy kết quả nào!");
        }

        // This method is marked with [TearDown]. NUnit will automatically run this method
        // after each and every [Test] method has finished, regardless of whether it passed or failed.
        [TearDown]
        public void Teardown()
        {
            // Close all browser windows that were opened by this WebDriver instance
            // and safely ends the browser driver process.
            driver.Quit();
        }
    }
}
