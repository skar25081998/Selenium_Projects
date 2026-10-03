using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemo.Tests.Pages;

namespace SauceDemo.Tests.Tests;

[TestFixture]
public sealed class SauceDemoScenarios
{
    private const string Username = "standard_user";
    private const string Password = "secret_sauce";
    private IWebDriver _driver = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless=new");
        options.AddArgument("--window-size=1920,1080");
        _driver = new ChromeDriver(options);
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
    }

    [TearDown]
    public void TearDown()
    {
        _driver?.Quit();
        _driver?.Dispose();
    }

    [Test]
    public void ValidUserCanLogInAndSeeAllProducts()
    {
        var inventory = Login();

        Assert.That(inventory.ProductNames, Has.Count.EqualTo(6));
    }

    [TestCase("az", "Sauce Labs Backpack", "Test.allTheThings() T-Shirt (Red)", TestName = "Products_sort_by_name_ascending")]
    [TestCase("za", "Test.allTheThings() T-Shirt (Red)", "Sauce Labs Backpack", TestName = "Products_sort_by_name_descending")]
    [TestCase("lohi", "Sauce Labs Onesie", "Sauce Labs Fleece Jacket", TestName = "Products_sort_by_price_ascending")]
    [TestCase("hilo", "Sauce Labs Fleece Jacket", "Sauce Labs Onesie", TestName = "Products_sort_by_price_descending")]
    public void InventoryCanBeSorted(string sortOption, string expectedFirst, string expectedLast)
    {
        var inventory = Login();

        inventory.SortBy(sortOption);

        Assert.That(inventory.ProductNames.First(), Is.EqualTo(expectedFirst));
        Assert.That(inventory.ProductNames.Last(), Is.EqualTo(expectedLast));
    }

    [Test]
    public void UserCanAddMultipleProductsToCart()
    {
        var inventory = Login();
        inventory.AddProduct("Sauce Labs Backpack");
        inventory.AddProduct("Sauce Labs Bike Light");

        Assert.That(inventory.CartItemCount, Is.EqualTo(2));
    }

    [Test]
    public void UserCanRemoveProductFromInventory()
    {
        var inventory = Login();
        inventory.AddProduct("Sauce Labs Backpack");
        inventory.RemoveProduct("Sauce Labs Backpack");

        Assert.That(inventory.CartItemCount, Is.Zero);
    }

    [Test]
    public void UserCanReviewAndRemoveProductFromCart()
    {
        var inventory = Login();
        inventory.AddProduct("Sauce Labs Backpack");
        var cart = inventory.OpenCart();

        Assert.That(cart.ProductNames, Is.EqualTo(new[] { "Sauce Labs Backpack" }));
        cart.RemoveProduct("Sauce Labs Backpack");
        Assert.That(cart.ItemCount, Is.Zero);
    }

    [Test]
    public void UserCanCompleteCheckout()
    {
        var inventory = Login();
        inventory.AddProduct("Sauce Labs Backpack");
        var checkout = inventory.OpenCart().StartCheckout();

        checkout.EnterInformation("Test", "User", "12345");
        checkout.FinishOrder();

        Assert.That(checkout.ConfirmationMessage, Is.EqualTo("Thank you for your order!"));
    }

    [Test]
    public void UserCanLogOut()
    {
        var inventory = Login();

        inventory.Logout();

        Assert.That(_driver.Url, Is.EqualTo("https://www.saucedemo.com/"));
    }

    private InventoryPage Login()
    {
        var login = new LoginPage(_driver);
        login.Open();
        return login.Login(Username, Password);
    }
}
