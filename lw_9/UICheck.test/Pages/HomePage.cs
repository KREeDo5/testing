using System.Collections.ObjectModel;

using UICheck.test.Config;

namespace UICheck.test.Pages;

using OpenQA.Selenium;

public class HomePage
{
    private readonly IWebDriver driver;

    public HomePage(IWebDriver webDriver)
    {
        driver = webDriver;
    }
    
    /// <summary>
    /// Кнопка "Добавить в корзину" в карточке товара в списке товаров.
    /// </summary>
    public IWebElement AddToCartButton => driver.FindElement(By.CssSelector(".b-basketBtn.b-basketBtn_descriptionCard")); //b-basketBtn__link b-basketBtn__link_toBasket
    
    /// <summary>
    /// Кнопка уменьшения количества товара. Используется в карточке товара в списке товаров и в карточке товара в корзине.
    /// </summary>
    public IWebElement DecrementProductButton => driver.FindElement(By.CssSelector(".b-counter__icon.b-counter__icon_minus")); //icon icon--minus b-counter__icon b-counter__icon_minus
    
    /// <summary>
    /// Кнопка увеличения количества товара. Используется в карточке товара в списке товаров и в карточке товара в корзине.
    /// </summary>
    public IWebElement IncrementProductButton => driver.FindElement(By.CssSelector(".b-counter__icon.b-counter__icon_plus")); //icon icon--plus b-counter__icon b-counter__icon_plus
    
    /// <summary>
    /// Количество товара. Используется в карточке товара в списке товаров и в карточке товара в корзине.
    /// </summary>
    public IWebElement ProductCount => driver.FindElement(By.CssSelector(".b-counter__value"));
    
    /// <summary>
    /// Кнопка корзины в шапке сайта.
    /// </summary>
    public IWebElement CartButton => driver.FindElement(By.CssSelector("#bx_basket_line_FKauiI .b-h__iconsItem_basket"));
    
    /// <summary>
    /// Окно корзины, которое появляется при нажатии на иконку корзины в шапке сайта.
    /// </summary>
    public IWebElement CartModal => driver.FindElement(By.CssSelector(".b-basket-popup.b-basket-popup_popup.click-popup.b-basket-popup_opened"));
    
    /// <summary>
    /// Сумма корзины в модальном окне корзины.
    /// </summary>
    public IWebElement CartModalTotalCost => driver.FindElement(By.CssSelector(".b-price__item.b-price__item_current"));
    
    /// <summary>
    /// Кнопка закрытия модального окна корзины.
    /// </summary>
    public IWebElement CartModalCloseButton => driver.FindElement(By.ClassName("b-basket__close")); //icon icon--close b-basket__close
    
    /// <summary>
    /// Общая цена товара в карточке товара в модальном окне корзины.
    /// </summary>
    public IWebElement CartModalProductTotalPrice => driver.FindElement(By.CssSelector("b-price__item_current"));   //b-price__item b-price__item_current
    
    /// <summary>
    /// Кнопка удаления товара из корзины в модальном окне корзины.
    /// </summary>
    public IWebElement CartModalProductDeleteButton => driver.FindElement(By.CssSelector(".b-basket__itemRemove"));
    
    /// <summary>
    /// Все товары в модальном окне корзины.
    /// </summary>
    public ReadOnlyCollection<IWebElement> CartModalProducts => driver.FindElements(By.CssSelector(".b-basket__item"));
    
    /// <summary>
    /// Текст, отображаемый в модальном окне корзины, когда корзина пуста.
    /// </summary>
    public IWebElement CartEmptyText => driver.FindElement(By.CssSelector(".b-basket__empty_text"));
    
    /// <summary>
    /// Кнопка принятия куки.
    /// </summary>
    private IWebElement AcceptCookieButton => driver.FindElement(By.CssSelector(".gdpr-cookie-accept-btn-wrapper"));

    public void Open()
    {
        driver.Navigate().GoToUrl(HomePageTestData.BaseUrl);
    }

    /// <summary>
    /// Найти первый элемент из новинок и добавить в корзину
    /// </summary>
    public void AddFirstProductToCart()
    {
        AddToCartButton.Click();
    }

    /// <summary>
    /// Открыть корзину по клику на иконку корзины в шапке сайта
    /// </summary>
    public void OpenCart()
    {
        CartButton.Click();
    }

    /// <summary>
    /// Проверяет наличие хотя бы одного товара в окне корзины с количеством == 1.
    /// </summary>
    public bool CartHasItem()
    {
        // Находим все элементы товаров в корзине
        foreach (IWebElement product in CartModalProducts)
        {
            IWebElement counterValue = product.FindElement(By.CssSelector(".b-counter__value"));
            if (int.TryParse(counterValue.Text, out int count) && count > 0)
            {
                return true;
            }
        }
        return false;
    }
    
    /// <summary>
    /// Проверяет, что в корзине нет товаров.
    /// </summary>
    public bool IsCartEmpty()
    {
        try
        {
            // Проверяем наличие хедера корзины и текста пустой корзины
            IWebElement emptyText = CartEmptyText;
            bool result = emptyText.Displayed;
            return result;
        }
        catch (NoSuchElementException)
        {
            // Корзина не пустая
            return false;
        }
    }

    /// <summary>
    /// Закрыть баннер с куки, если он есть
    /// </summary>
    public void CloseCookieBannerIfExists()
    {
        try
        {
            AcceptCookieButton.Click();
        }
        // Если баннера нет — ничего не делаем
        catch (NoSuchElementException)
        {
            Console.WriteLine("Баннер отсутствует");
        }
    }
}