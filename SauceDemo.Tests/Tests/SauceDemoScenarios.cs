namespace SauceDemo.Tests.Tests;

[TestFixture]
public sealed class SauceDemoScenarios : BaseTest
{
    [Test]
    public void ValidUserCanLogInAndSeeAllProducts()
    {
        Assert.That(Inventory.ProductNames, Has.Count.EqualTo(6));
    }

    [TestCase("az", "Sauce Labs Backpack", "Test.allTheThings() T-Shirt (Red)", TestName = "Products_sort_by_name_ascending")]
    [TestCase("za", "Test.allTheThings() T-Shirt (Red)", "Sauce Labs Backpack", TestName = "Products_sort_by_name_descending")]
    [TestCase("lohi", "Sauce Labs Onesie", "Sauce Labs Fleece Jacket", TestName = "Products_sort_by_price_ascending")]
    [TestCase("hilo", "Sauce Labs Fleece Jacket", "Sauce Labs Onesie", TestName = "Products_sort_by_price_descending")]
    public void InventoryCanBeSorted(string sortOption, string expectedFirst, string expectedLast)
    {
        Inventory.SortBy(sortOption);
        var productNames = Inventory.ProductNames;

        Assert.Multiple(() =>
        {
            Assert.That(productNames.First(), Is.EqualTo(expectedFirst));
            Assert.That(productNames.Last(), Is.EqualTo(expectedLast));
        });
    }

    [Test]
    public void UserCanAddMultipleProductsToCart()
    {
        Inventory.AddProduct("Sauce Labs Backpack");
        Inventory.AddProduct("Sauce Labs Bike Light");

        Assert.That(Inventory.CartItemCount, Is.EqualTo(2));
    }

    [Test]
    public void UserCanRemoveProductFromInventory()
    {
        Inventory.AddProduct("Sauce Labs Backpack");
        Inventory.RemoveProduct("Sauce Labs Backpack");

        Assert.That(Inventory.CartItemCount, Is.Zero);
    }

    [Test]
    public void UserCanReviewAndRemoveProductFromCart()
    {
        Inventory.AddProduct("Sauce Labs Backpack");
        var cart = Inventory.OpenCart();
        var productNames = cart.ProductNames;

        cart.RemoveProduct("Sauce Labs Backpack");
        var itemCount = cart.ItemCount;

        Assert.Multiple(() =>
        {
            Assert.That(productNames, Is.EqualTo(new[] { "Sauce Labs Backpack" }));
            Assert.That(itemCount, Is.Zero);
        });
    }

    [Test]
    public void UserCanCompleteCheckout()
    {
        Inventory.AddProduct("Sauce Labs Backpack");
        var checkout = Inventory.OpenCart().StartCheckout();

        checkout.EnterInformation("Test", "User", "12345");
        checkout.FinishOrder();

        Assert.That(checkout.ConfirmationMessage, Is.EqualTo("Thank you for your order!"));
    }

    [Test]
    public void UserCanLogOut()
    {
        Inventory.Logout();

        Assert.That(Driver.Url, Is.EqualTo("https://www.saucedemo.com/"));
    }

}
