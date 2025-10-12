using System.Text.Json;

namespace ShopProducts.TestData;

public class ProductTestData
{
    public static IEnumerable<object[]> LoadProducts()
    {
        string json = File.ReadAllText("TestData/products.json");
        List<Product>? list = JsonSerializer.Deserialize<List<Product>>(json);
        foreach (Product p in list)
            yield return new object[] { p };
    }
}