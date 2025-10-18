using UICheck.test.Config;
namespace UICheck.test.Pages;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

public class CatalogPage
{
    private readonly IWebDriver driver;

    public CatalogPage(IWebDriver webDriver)
    {
        driver = webDriver;
    }

    public void Open()
    {
        driver.Navigate().GoToUrl(CatalogPageTestData.CatalogUrl);
    }

    /// <summary>
    /// Блок фильтрации
    /// </summary>
    public IWebElement FilterPanel => driver.FindElement(By.CssSelector(".b-catalog__filterPanel"));

    /// <summary>
    /// Блок сортировки
    /// </summary>
    public IWebElement SortPanel => driver.FindElement(By.CssSelector(".b-catalog__sorting"));

    /// <summary>
    /// Кнопка открытия выпадающего меню сортировки
    /// </summary>
    public IWebElement SortDropdownToggle => SortPanel.FindElement(By.CssSelector(".vs__dropdown-toggle"));

    /// <summary>
    /// Выпадающий список опций сортировки
    /// </summary>
    public ReadOnlyCollection<IWebElement> SortOptions => driver.FindElements(By.CssSelector(".vs__dropdown-option"));

    /// <summary>
    /// Текущая выбранная сортировка
    /// </summary>
    public string CurrentSortOption 
    {
        get
        {
            IWebElement selectedSpan = SortPanel.FindElement(By.CssSelector(".vs__selected"));
            return selectedSpan.Text.Trim();
        }
    }

    /// <summary>
    /// Открыть выпадающее меню сортировки
    /// </summary>
    public void OpenSortDropdown()
    {
        SortDropdownToggle.Click();
    }
}