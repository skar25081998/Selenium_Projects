using OpenQA.Selenium;

namespace SauceDemo.Tests.Pages;

public sealed class CartPage(IWebDriver driver) : BasePage(driver)
{
    public int ItemCount => Driver.FindElements(Locators.CartItems).Count;

    public IReadOnlyCollection<string> ProductNames =>
        Driver.FindElements(Locators.CartProductNames)
            .Select(element => element.Text)
            .ToArray();

    public void RemoveProduct(string productName) => Click(Locators.RemoveCartItemButton(productName));

    public CheckoutPage StartCheckout()
    {
        Click(Locators.CheckoutButton);
        FindVisible(Locators.FirstName);
        return new CheckoutPage(Driver);
    }
}
