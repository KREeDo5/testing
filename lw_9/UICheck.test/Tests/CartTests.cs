using OpenQA.Selenium;
namespace UICheck.test.Tests;
using Pages;

/// <summary>
/// Тесты - Добавление товара в корзину и работа с модальным окном корзины
/// </summary>
public class CartTests(WebDriverFixture fixture) : IClassFixture<WebDriverFixture>
{
    private readonly IWebDriver _driver = fixture.Driver;
    
    [Fact]
    public void CartModalOpens()
    {
        HomePage homePage = new HomePage(_driver);
        homePage.Open();
        homePage.CloseCookieBannerIfExists();
        homePage.AddFirstProductToCart();
        homePage.OpenCart();
        Thread.Sleep(2000);
        Assert.True(homePage.CartModal.Displayed);
    }
    
  [Fact]
    public void CanCloseCartModalWindow()
    {
        HomePage homePage = new HomePage(_driver);
        homePage.Open();
        homePage.CloseCookieBannerIfExists();
        homePage.AddFirstProductToCart();
        homePage.OpenCart();
        Thread.Sleep(2000);
        homePage.CartModalCloseButton.Click();
        Thread.Sleep(2000);
        // Проверяем по NoSuchElementException, что окно исчезло
        bool isClosed;
        try
        {
            IWebElement modal = homePage.CartModal;
            isClosed = !modal.Displayed;
        }
        catch (NoSuchElementException)
        {
            isClosed = true;
        }
        Assert.True(isClosed);
    }
    
    // Проверка цены товара в модальном окне корзины
    [Fact]
    public void CartModalProductHasCorrectPrice()
    {
        HomePage homePage = new HomePage(_driver);
        homePage.Open();
        homePage.CloseCookieBannerIfExists();
        homePage.AddFirstProductToCart();
        homePage.OpenCart();
        Thread.Sleep(2000);
        string productPriceText = homePage.CartModalProductTotalPrice.Text;
        Assert.False(string.IsNullOrEmpty(productPriceText));
        Assert.True(int.TryParse(productPriceText.Replace(" ", ""), out int productPrice) && productPrice > 0);
    }
    
    // Проверка суммы корзины
    [Fact]
    public void CartModalTotalCostIsCorrect()
    {
        HomePage homePage = new HomePage(_driver);
        homePage.Open();
        homePage.CloseCookieBannerIfExists();
        homePage.AddFirstProductToCart();
        homePage.OpenCart();
        Thread.Sleep(2000);
        string costText = homePage.CartModalTotalCost.Text;
        Assert.False(string.IsNullOrEmpty(costText));
        // Можно проверить, что это число и больше нуля
        Assert.True(int.TryParse(costText.Replace(" ", ""), out int cost) && cost > 0);
    }
    
    [Fact]
    public void CartIsEmptyWhenNoProductAdded()
    {
        HomePage homePage = new HomePage(_driver);
        homePage.Open();
        homePage.CloseCookieBannerIfExists();
        homePage.OpenCart();
        Thread.Sleep(2000);
        Assert.True(homePage.IsCartEmpty());
    }
    
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