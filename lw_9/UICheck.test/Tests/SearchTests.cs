using UICheck.test.Config;

namespace UICheck.test.Tests;

using OpenQA.Selenium;
using Pages;

/// <summary>
/// Тесты - Поиск товара в каталоге
/// </summary>
public class SearchTests(WebDriverFixture fixture) : IClassFixture<WebDriverFixture>
{
    private readonly IWebDriver _driver = fixture.Driver;
    
    //Поиск возвращает товары
    [Fact]
    public void SearchItemAppearsInResults()
    {
        HomePage homePage = new HomePage(_driver);
        homePage.Open();
        homePage.CloseCookieBannerIfExists();

        SearchPage searchPage = new SearchPage(_driver);
        searchPage.OpenSearchModal();
        Thread.Sleep(2000);
        searchPage.EnterSearchText(SearchPageTestData.ValidSearchQuery);
        
        Thread.Sleep(2000);
        Assert.True(searchPage.HasSearchResults());
    }
    
    //Проверка поиска по некорректному (несуществующему) товару
    [Theory]
    [MemberData(nameof(InvalidProductQueries))]
    public void SearchUnknownProductTitleQuery(string invalidQuery)
    {
        HomePage homePage = new HomePage(_driver);
        homePage.Open();
        homePage.CloseCookieBannerIfExists();
        SearchPage searchPage = new SearchPage(_driver);
        searchPage.OpenSearchModal();
        Thread.Sleep(2000);
        searchPage.EnterSearchText(invalidQuery);
        Thread.Sleep(2000);
        Assert.False(searchPage.HasSearchResults());
    }
    
    public static IEnumerable<object[]> InvalidProductQueries()
    {
        foreach (string query in SearchPageTestData.InvalidSearchQueries)
        {
            yield return [query];
        }
    }
    
    //Проверка корректности поиска (наличие поискового запроса в заголовках результатов)
    [Theory]
    [MemberData(nameof(ValidProductQueries))]
    public void SearchResultsContainQueryInTitle(string query)
    {
        HomePage homePage = new HomePage(_driver);
        homePage.Open();
        homePage.CloseCookieBannerIfExists();
        SearchPage searchPage = new SearchPage(_driver);
        searchPage.OpenSearchModal();
        Thread.Sleep(2000);
        searchPage.EnterSearchText(query);
        Thread.Sleep(2000);
        Assert.True(searchPage.AllProductTitlesContain(query));
    }
    
    public static IEnumerable<object[]> ValidProductQueries()
    {
        foreach (string query in SearchPageTestData.ValidSearchQueries)
        {
            yield return [query];
        }
    }
}