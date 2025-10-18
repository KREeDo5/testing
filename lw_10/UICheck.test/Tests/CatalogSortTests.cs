using System.Collections.ObjectModel;
using OpenQA.Selenium;
using UICheck.test.Config;

namespace UICheck.test.Tests;
using Pages;

public class CatalogSortTests
{
    public static IEnumerable<object[]> Browsers() =>
    [
        ["chrome"],
        ["firefox"]
    ];
    
    [Theory]
    [MemberData(nameof(Browsers))]
    public void FilterPanelAndSortPanelExist(string browser)
    {
        using var fixture = new WebDriverFixture(browser);
        var driver = fixture.Driver;
        CatalogPage page = new CatalogPage(driver);
        page.Open();
        Thread.Sleep(1000);
        Assert.True(page.FilterPanel.Displayed);
        Assert.True(page.SortPanel.Displayed);
    }

    [Theory]
    [MemberData(nameof(Browsers))]
    public void CanOpenSortDropdownAndSeeOptions(string browser)
    {
        using var fixture = new WebDriverFixture(browser);
        var driver = fixture.Driver;
        CatalogPage page = new CatalogPage(driver);
        page.Open();
        Thread.Sleep(1000);
        page.OpenSortDropdown();
        Thread.Sleep(1000);
        ReadOnlyCollection<IWebElement> options = page.SortOptions;
        Assert.True(options.Count >= CatalogPageTestData.SortOptions.Length);
        foreach (string expected in CatalogPageTestData.SortOptions)
        {
            Assert.Contains(options, opt => opt.Text.Contains(expected));
        }
    }
}