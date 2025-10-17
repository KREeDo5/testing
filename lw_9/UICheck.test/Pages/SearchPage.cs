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
    /// Открыть модальное окно поиска (клик по иконке).
    /// </summary>
    public void OpenSearchModal()
    {
        IWebElement searchIcon = driver.FindElement(By.CssSelector(".b-h__iconsItem_search"));
        searchIcon.Click();
    }

    /// <summary>
    /// Ввести текст в поле поиска.
    /// </summary>
    public void EnterSearchText(string query)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
        IWebElement input = wait.Until(d => d.FindElement(By.CssSelector(".b-search__input")));
        input.Clear();
        input.SendKeys(query);
    }

    /// <summary>
    /// Наличие результатов после поиска
    /// </summary>
    public bool HasSearchResults()
    {   
        // Появился ли popup
        ReadOnlyCollection<IWebElement> popups = driver.FindElements(By.CssSelector(".b-search__popup"));
        if (popups.Count == 0)
        {
            return false;
        }

        // Появился ли блок с результатами
        IWebElement popup = popups[0];
        ReadOnlyCollection<IWebElement> searchResults = popup.FindElements(By.CssSelector(".b-search__result"));
        if (searchResults.Count == 0)
        {
            return false;
        }

        // Есть ли хотя бы 1 результат
        foreach (IWebElement resultBlock in searchResults)
        {
            ReadOnlyCollection<IWebElement> products = resultBlock.FindElements(By.CssSelector(".b-productCard.b-productCard_search"));
            if (products.Count > 0)
            {
                return true;
            }
        }
        return false;
    }
}