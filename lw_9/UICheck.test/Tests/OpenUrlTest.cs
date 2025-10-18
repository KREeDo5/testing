using UICheck.test.Config;
namespace UICheck.test.Tests;
using Xunit;
using OpenQA.Selenium;
public class OpenUrlTest(WebDriverFixture fixture) : IClassFixture<WebDriverFixture>
{
    private readonly IWebDriver _driver = fixture.Driver;

    [Fact]
    public void OpenHomePage()
    {
        _driver.Navigate().GoToUrl(HomePageTestData.BaseUrl);
        Assert.Contains(HomePageTestData.DomainName, _driver.Url);
    }
}