using OpenQA.Selenium;

namespace SauceDemo.Tests.Pages;

public sealed class CheckoutPage(IWebDriver driver) : BasePage(driver)
{
    public void EnterInformation(string firstName, string lastName, string postalCode)
    {
        EnterText(Locators.FirstName, firstName);
        EnterText(Locators.LastName, lastName);
        EnterText(Locators.PostalCode, postalCode);
        Click(Locators.ContinueButton);
        FindVisible(Locators.FinishButton);
    }

    public void FinishOrder() => Click(Locators.FinishButton);

    public string ConfirmationMessage => FindVisible(Locators.CompleteHeader).Text;
}
