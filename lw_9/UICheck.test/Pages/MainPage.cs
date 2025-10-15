using System.Collections.ObjectModel;

namespace UICheck.test.Pages;

using OpenQA.Selenium;

public class MainPage
{
    private readonly IWebDriver driver;

    public MainPage(IWebDriver webDriver)
    {
        driver = webDriver;
    }

    public void Open()
    {
        driver.Navigate().GoToUrl("https://prostayaeda.ru/");
    }

    /// <summary>
    /// Найти первый элемент из новинок и добавить в корзину
    /// </summary>
    public void AddFirstProductToCart()
    {
        IWebElement addToCartButton = driver.FindElement(By.CssSelector(".b-basketBtn.b-basketBtn_descriptionCard"));
        addToCartButton.Click();
    }

    /// <summary>
    /// Открыть корзину по клику на иконку корзины в шапке сайта
    /// </summary>
    public void OpenCart()
    {
        IWebElement cartLink = driver.FindElement(By.CssSelector("#bx_basket_line_FKauiI .b-h__iconsItem_basket"));
        cartLink.Click();
    }

    /// <summary>
    /// Проверяет наличие хотя бы одного товара в окне корзины с количеством == 1.
    /// </summary>
    public bool CartHasItem()
    {
        // Находим все элементы товаров в корзине
        ReadOnlyCollection<IWebElement> items = driver.FindElements(By.CssSelector(".b-basket__item"));
        foreach (IWebElement item in items)
        {
            IWebElement counterValue = item.FindElement(By.CssSelector(".b-counter__value"));
            Console.WriteLine("counterValue.Text = '" + counterValue.Text + "'");
            if (int.TryParse(counterValue.Text, out int count) && count == 1)
            {
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// Закрыть баннер с куки, если он есть
    /// </summary>
    public void CloseCookieBannerIfExists()
    {
        try
        {
            IWebElement cookieButton = driver.FindElement(By.CssSelector(".gdpr-cookie-accept-btn-wrapper"));
            cookieButton.Click();
        }
        // Если баннера нет — ничего не делаем
        catch (NoSuchElementException)
        {
            Console.WriteLine("Баннер отсутствует");
        }
    }
}