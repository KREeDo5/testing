using System.Text.Json;

using ShopProducts.Entities;

namespace ShopProducts.TestData;

public class ProductTestData
{
    private static List<Product> All =>
        JsonSerializer.Deserialize<List<Product>>(File.ReadAllText("TestData/products.json")) ?? [];

    public static IEnumerable<object[]> AllProducts()
    {
        foreach (Product p in All) yield return [p];
    }

    /// <summary>
    /// Товары, соответствующие требованиям
    /// </summary>
    public static IEnumerable<object[]> ValidProducts()
    {
        foreach (Product p in All)
            if (p.category_id is >= 1 and <= 15 && !string.IsNullOrWhiteSpace(p.title) && p.price > 0)
                yield return [p];
    }
    
    /// <summary>
    /// Товары с некорректной категорией (меньше 1 или больше 15 или null).
    /// </summary>
    public static IEnumerable<object[]> InvalidCategoryProducts()
    {
        foreach (Product p in All)
            if (p.category_id is null || p.category_id < 1 || p.category_id > 15)
                yield return [p];
    }

    /// <summary>
    /// Товары с пустым или состоящим только из пробелов названием (title).
    /// </summary>
    public static IEnumerable<object[]> EmptyOrSpaceFieldsProducts()
    {
        foreach (Product p in All)
            if (string.IsNullOrWhiteSpace(p.title))
                yield return [p];
    }

    /// <summary>
    /// Товары, название которых содержит специальные символы (не буквы/цифры/пробел).
    /// Проверяет граничные кейсы на работу с title/alias.
    /// </summary>
    public static IEnumerable<object[]> SpecialCharProducts()
    {
        foreach (Product p in All)
            if (!string.IsNullOrEmpty(p.title) && p.title.Any(ch => !char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch)))
                yield return [p];
    }

    /// <summary>
    /// Товар с очень длинным названием.
    /// </summary>
    public static IEnumerable<object[]> LongTitleProducts()
    {
        foreach (Product p in All)
            if (p.title != null && p.title.Length > 255)
                yield return [p];
    }

    /// <summary>
    /// Товары с нулевой ценой и отсутствующей (price == 0 // null).
    /// </summary>
    public static IEnumerable<object[]> ZeroPriceProducts()
    {
        foreach (Product p in All)
            if (p.price == 0 || p.price is null)
                yield return [p];
    }

    /// <summary>
    /// Товары с одинаковым названием — Дубликаты.
    /// </summary>
    public static IEnumerable<object[]> SameTitleProducts()
    {
        IEnumerable<IGrouping<string?, Product>> dups = All.GroupBy(p => p.title)
                      .Where(g => !string.IsNullOrWhiteSpace(g.Key) && g.Count() > 1);
        foreach (IGrouping<string, Product> group in dups)
            foreach (Product p in group)
                yield return [p];
    }
}