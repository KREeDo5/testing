namespace UICheck.test.Tests;
using Xunit;
using OpenQA.Selenium;
using Pages;
using OpenQA.Selenium.Support.UI;

public class CartTests(WebDriverFixture fixture) : IClassFixture<WebDriverFixture>
{
    private readonly IWebDriver _driver = fixture.Driver;

    [Fact]
    public void AddProductToCartTest()
    {
        MainPage mainPage = new MainPage(_driver);

        mainPage.Open();
        mainPage.CloseCookieBannerIfExists();
        mainPage.AddFirstProductToCart();
        mainPage.OpenCart();
        Thread.Sleep(2000);
        bool cartHasItem = mainPage.CartHasItem();
        Assert.True(cartHasItem);
    }
}