using System.Globalization;

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
    
    // Блок с карточками товаров
    public ReadOnlyCollection<IWebElement> ProductCards => driver.FindElements(By.CssSelector(".b-productCard"));
    
    /// <summary>
    /// Получить цены всех товаров на странице
    /// </summary>
    public List<decimal> GetProductPrices()
    {
        List<decimal> prices = new List<decimal>();
        foreach (IWebElement card in ProductCards)
        {
            IWebElement priceElement = card.FindElement(By.CssSelector(".b-price__item.b-price__item_current span"));
            string raw = priceElement.Text.Trim();
            raw = raw.Replace(',', '.');
            raw = raw.Replace(" ", "");
            if (decimal.TryParse(raw, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price))
            {
                prices.Add(price);
            }
        }
        return prices;
    }
}