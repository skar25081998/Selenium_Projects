using OpenQA.Selenium;

namespace SauceDemo.Tests.Pages;

public static class Locators
{
    public static readonly By Username = By.Id("user-name");
    public static readonly By Password = By.Id("password");
    public static readonly By LoginButton = By.Id("login-button");
    public static readonly By LoginError = By.CssSelector("[data-test='error']");
    public static readonly By InventoryContainer = By.Id("inventory_container");
    public static readonly By ProductNames = By.CssSelector(".inventory_item_name");
    public static readonly By ProductSort = By.CssSelector("[data-test='product-sort-container']");
    public static readonly By CartLink = By.CssSelector(".shopping_cart_link");
    public static readonly By CartBadge = By.CssSelector(".shopping_cart_badge");
    public static readonly By CartList = By.CssSelector(".cart_list");
    public static readonly By CartItems = By.CssSelector(".cart_item");
    public static readonly By CartProductNames = By.CssSelector(".cart_item .inventory_item_name");
    public static readonly By FirstName = By.Id("first-name");
    public static readonly By LastName = By.Id("last-name");
    public static readonly By PostalCode = By.Id("postal-code");
    public static readonly By CheckoutButton = By.Id("checkout");
    public static readonly By ContinueButton = By.Id("continue");
    public static readonly By FinishButton = By.Id("finish");
    public static readonly By CompleteHeader = By.CssSelector(".complete-header");
    public static readonly By MenuButton = By.Id("react-burger-menu-btn");
    public static readonly By LogoutLink = By.Id("logout_sidebar_link");

    public static By ProductNameInCard(string productName) =>
        By.XPath($".//div[@class='inventory_item_name' and normalize-space()='{productName}']");

    public static By AddToCartButton(string productName) =>
        By.CssSelector($"[data-test='add-to-cart-{ProductId(productName)}']");

    public static By RemoveFromCartButton(string productName) =>
        By.CssSelector($"[data-test='remove-{ProductId(productName)}']");

    public static By RemoveCartItemButton(string productName) =>
        By.XPath($"//div[contains(@class,'cart_item')][.//div[@class='inventory_item_name' and normalize-space()='{productName}']]//button");

    public static By CartItemByName(string productName) =>
        By.XPath($"//div[contains(@class,'cart_item')][.//div[@class='inventory_item_name' and normalize-space()='{productName}']]");

    private static string ProductId(string productName) =>
        productName.ToLowerInvariant().Replace(' ', '-');
}
