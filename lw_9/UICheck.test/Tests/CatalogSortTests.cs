using System.Collections.ObjectModel;
using OpenQA.Selenium;
using UICheck.test.Config;

namespace UICheck.test.Tests;
using Pages;

public class CatalogSortTests(WebDriverFixture fixture) : IClassFixture<WebDriverFixture>
{
    private readonly IWebDriver _driver = fixture.Driver;

    [Fact]
    public void FilterPanelAndSortPanelExist()
    {
        CatalogPage page = new CatalogPage(_driver);
        page.Open();
        Thread.Sleep(1000);
        Assert.True(page.FilterPanel.Displayed);
        Assert.True(page.SortPanel.Displayed);
    }

    [Fact]
    public void CanOpenSortDropdownAndSeeOptions()
    {
        CatalogPage page = new CatalogPage(_driver);
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

    [Theory]
    [MemberData(nameof(SortOptionsData))]
    public void CanSelectSortOption(string label)
    {
        CatalogPage page = new CatalogPage(_driver);
        page.Open();
        Thread.Sleep(1000);
        page.OpenSortDropdown();
        Thread.Sleep(1000);
        ReadOnlyCollection<IWebElement> options = page.SortOptions;
        IWebElement? opt = options.FirstOrDefault(o => o.Text.Contains(label));
        Assert.NotNull(opt);
        opt.Click();
        Thread.Sleep(1000);
        Assert.Equal(label, page.CurrentSortOption);
    }

    public static IEnumerable<object[]> SortOptionsData()
    {
        foreach (string label in CatalogPageTestData.SortOptions)
            yield return new object[] { label };
    }
}