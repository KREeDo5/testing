using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UICheck.test;

public class WebDriverFixture : IDisposable
{
    public IWebDriver Driver { get; private set; }

    public WebDriverFixture()
    {
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        Driver = new ChromeDriver(options);
    }

    public void Dispose()
    {
        // Закрываем Chrome
        Driver.Quit();
    }
}
