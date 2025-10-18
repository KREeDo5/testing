using System.Collections.ObjectModel;

namespace UICheck.test.Pages;

using OpenQA.Selenium;

public class AuthPage
{
    private readonly IWebDriver _driver;

    public AuthPage(IWebDriver webDriver)
    {
        _driver = webDriver;
    }

    /// <summary>
    /// Открыть страницу авторизации.
    /// </summary>
    public void Open()
    {
        _driver.Navigate().GoToUrl("https://prostayaeda.ru/login/?register=false");
    }

    /// <summary>
    /// Поле ввода телефона.
    /// </summary>
    public IWebElement PhoneInput => _driver.FindElement(By.Id("USER_LOGIN"));

    /// <summary>
    /// Поле ввода пароля.
    /// </summary>
    public IWebElement PasswordInput => _driver.FindElement(By.Id("USER_PASSWORD"));

    /// <summary>
    /// Кнопка "Войти".
    /// </summary>
    public IWebElement SubmitButton => _driver.FindElement(By.CssSelector("button[type='submit'].b-auth__btn"));

    /// <summary>
    /// Заголовок формы авторизации.
    /// </summary>
    public IWebElement Title => _driver.FindElement(By.XPath("//h4[contains(text(), 'Авторизация')]"));
    
    /// <summary>
    /// Проверяет, есть ли в модальных окнах текст (например, "Пользователь не найден" или "Неверный логин или пароль").
    /// </summary>
    public bool IsErrorModalVisible(string errorText)
    {
        try
        {
            // Ищет <h5> с нужным текстом во всех модальных окнах
            ReadOnlyCollection<IWebElement> elements = _driver.FindElements(By.XPath($"//h5[contains(@class,'b-order-confirm__title') and contains(translate(text(),'ABCDEFGHIJKLMNOPQRSTUVWXYZАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ','abcdefghijklmnopqrstuvwxyzабвгдеёжзийклмнопрстуфхцчшщъыьэюя'), '{errorText.ToLower()}')]"));
            foreach (IWebElement element in elements)
            {
                if (element.Displayed) return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Проверяет наличие локальной help-label (ошибки под полем авторизации).
    /// </summary>
    public bool IsLocalHelpLabelVisible()
    {
        try
        {
            ReadOnlyCollection<IWebElement> elements = _driver.FindElements(By.CssSelector(".help-label"));
            foreach (IWebElement element in elements)
            {
                if (element.Displayed && !string.IsNullOrWhiteSpace(element.Text)) return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}