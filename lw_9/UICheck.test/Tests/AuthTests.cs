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
    [Fact]
    public void AuthPage_LoginErrorWithInvalidData()
    {
        AuthPage page = new AuthPage(_driver);
        page.Open();
        Thread.Sleep(1000);
        page.PhoneInput.SendKeys("1111111111"); //TODO: Все входные данные и проверяемые значения должны быть вынесены в конфигурационный файл с тестовым набором данных (имя товара, логин и тп).
        page.PasswordInput.SendKeys("wrongpass");  //TODO: Все входные данные и проверяемые значения должны быть вынесены в конфигурационный файл с тестовым набором данных (имя товара, логин и тп).
        page.SubmitButton.Click();
        Thread.Sleep(1500);
        Assert.True(page.IsLocalHelpLabelVisible());
    }
    
    // Аккаунт не найден
    [Fact]
    public void AuthPage_ShowsUserNotFound()
    {
        AuthPage page = new AuthPage(_driver);
        page.Open();
        Thread.Sleep(1000);
        page.PhoneInput.SendKeys("79999999933");   //TODO: Все входные данные и проверяемые значения должны быть вынесены в конфигурационный файл с тестовым набором данных (имя товара, логин и тп).
        page.PasswordInput.SendKeys("wrongpass123");  //TODO: Все входные данные и проверяемые значения должны быть вынесены в конфигурационный файл с тестовым набором данных (имя товара, логин и тп).
        page.SubmitButton.Click();
        Thread.Sleep(1000);
        Assert.True(page.IsErrorModalVisible("Пользователь не найден"));  //TODO: Все входные данные и проверяемые значения должны быть вынесены в конфигурационный файл с тестовым набором данных (имя товара, логин и тп).
    }
    
    // Аккаунт найден, но неверный пароль
    [Fact]
    public void AuthPage_ShowsInvalidPasswordMessageForValidUser()
    {
        AuthPage page = new AuthPage(_driver);
        page.Open();
        Thread.Sleep(1000);
        page.PhoneInput.SendKeys("79961163172");   //TODO: Все входные данные и проверяемые значения должны быть вынесены в конфигурационный файл с тестовым набором данных (имя товара, логин и тп).
        page.PasswordInput.SendKeys("wrongpass123");  //TODO: Все входные данные и проверяемые значения должны быть вынесены в конфигурационный файл с тестовым набором данных (имя товара, логин и тп).
        page.SubmitButton.Click();
        Thread.Sleep(1000);
        Assert.True(page.IsErrorModalVisible("Неверный логин или пароль"));  //TODO: Все входные данные и проверяемые значения должны быть вынесены в конфигурационный файл с тестовым набором данных (имя товара, логин и тп).
    }

    //Тест с реальными данными
    [Fact]
    public void AuthPage_LoginWithValidData()
    {
        AuthPage page = new AuthPage(_driver);
        page.Open();
        Thread.Sleep(1000);
        page.PhoneInput.SendKeys("79961163172");  //TODO: Все входные данные и проверяемые значения должны быть вынесены в конфигурационный файл с тестовым набором данных (имя товара, логин и тп).
        page.PasswordInput.SendKeys("secretPassword");  //TODO: Все входные данные и проверяемые значения должны быть вынесены в конфигурационный файл с тестовым набором данных (имя товара, логин и тп).
        page.SubmitButton.Click();
        Thread.Sleep(2000);
        //TODO проверка на корректность далее
    }
}