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
    
    [Fact]
    public void IncrementProductCountTest()
    {
        HomePage homePage = new HomePage(_driver);
        homePage.Open();
        homePage.CloseCookieBannerIfExists();
        homePage.AddFirstProductToCart();
        homePage.OpenCart();
        Thread.Sleep(2000);
        int initialCount = int.Parse(homePage.ProductCount.Text);
        homePage.IncrementProductButton.Click();
        Thread.Sleep(2000);
        int newCount = int.Parse(homePage.ProductCount.Text);
        Assert.True(newCount > initialCount);
    }
    
    [Fact]
    public void DecrementProductCountTest()
    {
        HomePage homePage = new HomePage(_driver);
        homePage.Open();
        homePage.CloseCookieBannerIfExists();
        homePage.AddFirstProductToCart();
        homePage.OpenCart();
        Thread.Sleep(2000);
        homePage.IncrementProductButton.Click();
        Thread.Sleep(2000);
        int countAfterIncrement = int.Parse(homePage.ProductCount.Text);
        homePage.DecrementProductButton.Click();
        Thread.Sleep(2000);
        int countAfterDecrement = int.Parse(homePage.ProductCount.Text);
        Assert.True(countAfterDecrement < countAfterIncrement);
    }
    
    [Fact]
    public void RemoveProductFromCartTest()
    {
        HomePage homePage = new HomePage(_driver);
        homePage.Open();
        homePage.CloseCookieBannerIfExists();
        homePage.AddFirstProductToCart();
        homePage.OpenCart();
        Thread.Sleep(2000);
        homePage.CartModalProductDeleteButton.Click();
        Thread.Sleep(5000);
        Assert.True(homePage.IsCartEmpty());
    }
}