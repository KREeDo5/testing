namespace UICheck.test.Tests;

using Xunit;
using OpenQA.Selenium;
public class OpenUrlTest(WebDriverFixture fixture) : IClassFixture<WebDriverFixture>
{
    private readonly IWebDriver _driver = fixture.Driver;

    [Fact]
    public void OpenMainPage()
    {
        _driver.Navigate().GoToUrl("https://prostayaeda.ru/");
        Assert.Contains("prostayaeda", _driver.Url);
    }
}