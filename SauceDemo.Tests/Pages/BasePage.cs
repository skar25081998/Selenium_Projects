using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Tests.Pages;

public abstract class BasePage(IWebDriver driver)
{
    protected IWebDriver Driver { get; } = driver;
    protected WebDriverWait Wait { get; } = new(driver, TimeSpan.FromSeconds(10));

    protected IWebElement FindVisible(By locator) =>
        Wait.Until(d =>
        {
            var element = d.FindElement(locator);
            return element.Displayed ? element : null;
        })!;

    protected void WaitForPageLoad() =>
        Wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState")?.ToString() == "complete");

    protected void Click(By locator) => FindVisible(locator).Click();

    protected void EnterText(By locator, string value)
    {
        var element = FindVisible(locator);
        element.Clear();
        element.SendKeys(value);
    }
}
