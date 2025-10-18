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
        searchPage.EnterSearchText("ролл"); //TODO: Все входные данные и проверяемые значения должны быть вынесены в конфигурационный файл с тестовым набором данных (имя товара, логин и тп).
        
        Thread.Sleep(2000);
        Assert.True(searchPage.HasSearchResults());
    }
    
    //Проверка поиска по некорректному (несуществующему) товару
    [Fact]
    public void SearchUnknownProductTitleQuery()
    {
        HomePage homePage = new HomePage(_driver);
        homePage.Open();
        homePage.CloseCookieBannerIfExists();
        SearchPage searchPage = new SearchPage(_driver);
        searchPage.OpenSearchModal();
        Thread.Sleep(2000);
        searchPage.EnterSearchText("Наушники"); //TODO: Все входные данные и проверяемые значения должны быть вынесены в конфигурационный файл с тестовым набором данных (имя товара, логин и тп).
        Thread.Sleep(2000);
        Assert.False(searchPage.HasSearchResults());
    }
    
    //Проверка корректности поиска (наличие поискового запроса в заголовках результатов)
    [Fact]
    public void SearchResultsContainQueryInTitle()
    {
        HomePage homePage = new HomePage(_driver);
        homePage.Open();
        homePage.CloseCookieBannerIfExists();
        SearchPage searchPage = new SearchPage(_driver);
        searchPage.OpenSearchModal();
        Thread.Sleep(2000);
        string query = "Ролл Филадельфия Лайт с огурцом"; //TODO: Все входные данные и проверяемые значения должны быть вынесены в конфигурационный файл с тестовым набором данных (имя товара, логин и тп).
        searchPage.EnterSearchText(query);
        Thread.Sleep(2000);
        Assert.True(searchPage.AllProductTitlesContain(query));
    }
}