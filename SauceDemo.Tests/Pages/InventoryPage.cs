using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.Tests.Pages;

public sealed class InventoryPage(IWebDriver driver) : BasePage(driver)
{
    public IReadOnlyCollection<string> ProductNames =>
        FindVisible(Locators.InventoryContainer)
            .FindElements(Locators.ProductNames)
            .Select(element => element.Text)
            .ToArray();

    public int CartItemCount =>
        Driver.FindElements(Locators.CartBadge).Count == 0
            ? 0
            : int.Parse(Driver.FindElement(Locators.CartBadge).Text);

    public void AddProduct(string productName) => Click(Locators.AddToCartButton(productName));

    public void RemoveProduct(string productName) => Click(Locators.RemoveFromCartButton(productName));

    public void SortBy(string option)
    {
        var select = new SelectElement(FindVisible(Locators.ProductSort));
        select.SelectByValue(option);
    }

    public CartPage OpenCart()
    {
        Click(Locators.CartLink);
        FindVisible(Locators.CartList);
        return new CartPage(Driver);
    }

    public void Logout()
    {
        Click(Locators.MenuButton);
        Click(Locators.LogoutLink);
        FindVisible(Locators.LoginButton);
    }
}
