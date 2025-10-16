namespace ShopProducts.test;

using ShopProducts.Entities;

public class GetProductTests
{
    /// <summary>
    /// Метод получения списка товаров возвращает не пустой список.
    /// </summary>
    [Fact]
    public async Task Test_GetProductsWithNotEmptyArray()
    {
        ProductModel model = new ProductModel();
        List<Product>? products = await model.GetProducts();
        Assert.NotNull(products);
        Assert.NotEmpty(products);
    }
    
    /// <summary>
    /// Проверка соответствия сериализации ожидаемой схеме JSON.
    /// </summary>
    [Fact]
    public async Task Test_GetProductsWithAppropriateJsonSchema()
    {
        ProductModel model = new ProductModel();
        List<Product>? products = await model.GetProducts();
        Assert.NotNull(products);

        foreach (Product p in products)
        {
            Assert.True(p.id is not null, "id is null");
            Assert.True(p.category_id is not null, "category_id is null");
            Assert.False(string.IsNullOrWhiteSpace(p.title), "title is null or empty"); //Приходят пустые строки
            Assert.True(p.price is not null, "price is null");
        }
    }
}