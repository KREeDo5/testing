using ShopProducts.Entities;
using ShopProducts.TestData;

namespace ShopProducts.test;

public class DeleteProductTests
{
    [Theory]
    [MemberData(nameof(ProductTestData.ValidProducts), MemberType = typeof(ProductTestData))]
    public async Task Test_DeleteExistingProduct(Product product)
    {
        ProductModel model = new ProductModel();

        // Добавляем валидный товар
        int? id = await model.AddProduct(product);
        Assert.NotNull(id);

        // Удаляем этот товар
        bool deleted = await model.DeleteProductById(id.Value);
        Assert.True(deleted);

        // Проверяем, что он действительно удалён
        List<Product>? products = await model.GetProducts();
        Assert.NotNull(products);
        Assert.DoesNotContain(products, p => p.id == id);
    }

   [Fact]
    public async Task Test_DeleteNotExistingProduct()
    {
        ProductModel model = new ProductModel();
        int notExistingId = -123456;
        bool deleted = await model.DeleteProductById(notExistingId);
        Assert.False(deleted);
    }
}