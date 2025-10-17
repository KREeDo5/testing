namespace UICheck.test.Tests;

using OpenQA.Selenium;
using Pages;

public class SearchTests(WebDriverFixture fixture) : IClassFixture<WebDriverFixture>
{
    private readonly IWebDriver _driver = fixture.Driver;

    [Fact]
    public void SearchItemAppearsInResults()
    {
        HomePage homePage = new HomePage(_driver);
        homePage.Open();
        homePage.CloseCookieBannerIfExists();

        SearchPage searchPage = new SearchPage(_driver);
        searchPage.OpenSearchModal();
        searchPage.EnterSearchText("ролл");
        
        Thread.Sleep(2000);
        Assert.True(searchPage.HasSearchResults());
    }
    
    //TODO: Поиск конкретного товара
}