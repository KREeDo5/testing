using UICheck.test.Config;
using OpenQA.Selenium;
namespace UICheck.test.Tests;
using Pages;

public class AuthTests(WebDriverFixture fixture) : IClassFixture<WebDriverFixture>
{
    private readonly IWebDriver _driver = fixture.Driver;

    [Fact]
    public void AuthPage_ElementsPresent()
    {
        AuthPage page = new AuthPage(_driver);
        page.Open();
        Thread.Sleep(1000);
        Assert.True(page.Title.Displayed);
        Assert.True(page.PhoneInput.Displayed);
        Assert.True(page.PasswordInput.Displayed);
        Assert.True(page.SubmitButton.Displayed);
    }

    //Ошибка при вводе пустых полей
    [Fact]
    public void AuthPage_LoginErrorWithEmptyFields()
    {
        AuthPage page = new AuthPage(_driver);
        page.Open();
        Thread.Sleep(1000);
        page.SubmitButton.Click();
        Thread.Sleep(1000);
        Assert.True(page.IsLocalHelpLabelVisible());
    }

    //Ошибка при вводе некорректных данных
    [Theory]
    [MemberData(nameof(InvalidLoginData))]
    public void AuthPage_LoginErrorWithInvalidData(string login, string password)
    {
        AuthPage page = new AuthPage(_driver);
        page.Open();
        Thread.Sleep(1000);
        page.PhoneInput.SendKeys(login);
        page.PasswordInput.SendKeys(password);
        page.SubmitButton.Click();
        Thread.Sleep(1500);
        Assert.True(page.IsLocalHelpLabelVisible()
                    || page.IsErrorModalVisible("Пользователь не найден")
                    || page.IsErrorModalVisible("Неверный логин или пароль"));
    }

    public static IEnumerable<object[]> InvalidLoginData()
    {
        foreach ((string Login, string Password) pair in AuthPageTestData.InvalidLoginCredentials)
        {
            yield return [pair.Login, pair.Password];
        }
    }

    // Аккаунт не найден
    [Fact]
    public void AuthPage_ShowsUserNotFound()
    {
        AuthPage page = new AuthPage(_driver);
        page.Open();
        Thread.Sleep(1000);
        page.PhoneInput.SendKeys(AuthPageTestData.InvalidLogin);
        page.PasswordInput.SendKeys(AuthPageTestData.InvalidPassword);
        page.SubmitButton.Click();
        Thread.Sleep(1000);
        Assert.True(page.IsErrorModalVisible(AuthPageTestData.UserNotFound));
    }

    // Аккаунт найден, но неверный пароль
    [Fact]
    public void AuthPage_ShowsInvalidPasswordMessageForValidUser()
    {
        AuthPage page = new AuthPage(_driver);
        page.Open();
        Thread.Sleep(1000);
        page.PhoneInput.SendKeys(AuthPageTestData.ExistingLogin);
        page.PasswordInput.SendKeys(AuthPageTestData.InvalidPassword);
        page.SubmitButton.Click();
        Thread.Sleep(1000);
        Assert.True(page.IsErrorModalVisible(AuthPageTestData.InvalidPasswordError));
    }

    //Тест с реальными данными
    [Fact]
    public void AuthPage_LoginWithValidData()
    {
        AuthPage page = new AuthPage(_driver);
        page.Open();
        Thread.Sleep(1000);
        page.PhoneInput
            .SendKeys(AuthPageTestData.ExistingLogin);
        page.PasswordInput
            .SendKeys(AuthPageTestData.SecretPassword);
        page.SubmitButton.Click();
        Thread.Sleep(6000);
        Assert.True(page.DidRedirectToPersonalPage());
    }
}