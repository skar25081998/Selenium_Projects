using OpenQA.Selenium;

namespace SauceDemo.Tests.Pages;

public sealed class LoginPage(IWebDriver driver) : BasePage(driver)
{
    public void Open() => Driver.Navigate().GoToUrl("https://www.saucedemo.com/");

    public InventoryPage Login(string username, string password)
    {
        EnterText(Locators.Username, username);
        EnterText(Locators.Password, password);
        Click(Locators.LoginButton);
        WaitForPageLoad();
        FindVisible(Locators.InventoryContainer);
        return new InventoryPage(Driver);
    }
}
