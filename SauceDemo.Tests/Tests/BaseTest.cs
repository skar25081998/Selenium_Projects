using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SauceDemo.Tests.Pages;

namespace SauceDemo.Tests.Tests;

public abstract class BaseTest
{
    private const string Username = "standard_user";
    private const string Password = "secret_sauce";

    protected IWebDriver Driver { get; private set; } = null!;
    protected InventoryPage Inventory { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        var options = new ChromeOptions();
        options.AddArgument("--window-size=1920,1080");
        options.AddUserProfilePreference("credentials_enable_service", false);
        options.AddUserProfilePreference("profile.password_manager_enabled", false);
        options.AddUserProfilePreference("profile.password_manager_leak_detection", false);
        Driver = new ChromeDriver(options);
        Driver.Manage().Window.Maximize();
        Driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
        Inventory = Login();
    }

    private InventoryPage Login()
    {
        var login = new LoginPage(Driver);
        login.Open();
        return login.Login(Username, Password);
    }

    [TearDown]
    public void TearDown()
    {
        Driver?.Quit();
        Driver?.Dispose();
    }
}