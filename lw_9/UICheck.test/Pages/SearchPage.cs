using System.Collections.ObjectModel;

namespace UICheck.test.Pages;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using System;

public class SearchPage
{
    private readonly IWebDriver driver;

    public SearchPage(IWebDriver webDriver)
    {
        driver = webDriver;
    }
    
    /// <summary>
    /// Кнопка поиска.
    /// </summary>
    public IWebElement SearchIcon => driver.FindElement(By.CssSelector(".b-h__iconsItem_search"));
    
    /// <summary>
    /// Форма ввода текста для поиска товаров.
    /// </summary>
    public IWebElement SearchInput => driver.FindElement(By.CssSelector(".b-search__input"));
    
    /// <summary>
    /// Список всех открытых модальных окон поиска на странице
    /// </summary>
    public ReadOnlyCollection<IWebElement> SearchPopups => driver.FindElements(By.CssSelector(".b-search__popup"));

    /// <summary>
    /// Список блоков с результатами поиска внутри первого открытого окна поиска. Каждый элемент SearchResultsBlocks — отдельный DIV с классом .b-search__result.
    /// </summary>
    public ReadOnlyCollection<IWebElement> SearchResultsBlocks
    {
        get
        {
            if (SearchPopups.Count == 0)
            {
                return new List<IWebElement>().AsReadOnly();
            }

            return SearchPopups[0].FindElements(By.CssSelector(".b-search__result"));
        }
    }
    
    /// <summary>
    /// Список карточек товаров, найденных по поиску.
    /// </summary>
    public ReadOnlyCollection<IWebElement> ProductCards
    {
        get
        {
            List<IWebElement> products = new List<IWebElement>();
            foreach (IWebElement resultsBlock in SearchResultsBlocks)
            {
                products.AddRange(resultsBlock.FindElements(By.CssSelector(".b-productCard.b-productCard_search")));
            }

            return products.AsReadOnly();
        }
    }

    /// <summary>
    /// Открыть модальное окно поиска (клик по иконке).
    /// </summary>
    public void OpenSearchModal()
    {
        SearchIcon.Click();
    }

    /// <summary>
    /// Ввести текст в поле поиска.
    /// </summary>
    public void EnterSearchText(string query)
    {
        SearchInput.Clear();
        SearchInput.SendKeys(query);
    }

    /// <summary>
    /// Наличие результатов после поиска
    /// </summary>
    public bool HasSearchResults()
    {
        return ProductCards.Count > 0;
    }
}