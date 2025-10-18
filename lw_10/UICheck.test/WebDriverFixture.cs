using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace UICheck.test;

public class WebDriverFixture : IDisposable
{
    public IWebDriver Driver { get; }

    public WebDriverFixture(string browser)
    {
        Uri gridUri = new Uri("http://localhost:4444/wd/hub");

        if (browser.ToLower() == "chrome")
        {
            ChromeOptions options = new ChromeOptions();
            Driver = new RemoteWebDriver(gridUri, options);
        }
        else if (browser.ToLower() == "firefox")
        {
            FirefoxOptions options = new FirefoxOptions();
            Driver = new RemoteWebDriver(gridUri, options);
        }
    }

    public void Dispose()
    {
        // Закрываем Chrome
        Driver.Quit();
    }
}
