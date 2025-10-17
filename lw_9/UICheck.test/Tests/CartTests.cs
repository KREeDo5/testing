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
        HomePage homePage = new HomePage(_driver);

        homePage.Open();
        homePage.CloseCookieBannerIfExists();
        homePage.AddFirstProductToCart();
        homePage.OpenCart();
        Thread.Sleep(2000);
        bool cartHasItem = homePage.CartHasItem();
        Assert.True(cartHasItem);
    }
}