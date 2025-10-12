using ShopProducts.TestData;

namespace ShopProducts.test;

public class UnitTest1
{
    [Theory]
    [MemberData(nameof(ProductTestData.LoadProducts), MemberType = typeof(ProductTestData))]
    public async Task ProductFullScenario(Product testProduct)
    {
        ProductModel model = new ProductModel();

        // Добавляем товар
        int? id = await model.AddProduct(testProduct);
        Assert.NotNull(id);

        // Проверяем, что товар добавлен
        List<Product>? products = await model.GetProducts();
        Assert.NotNull(products);
        Assert.NotEmpty(products);

        Product? added = products.FirstOrDefault(p => p.id == id);
        Assert.NotNull(added);
        bool isValid = ProductModel.ValidateProduct(testProduct, added);
        Assert.True(isValid, $"Ожидание не совпало с реальностью для товара id:{id}");

        // Редактируем
        added.title += "_edit";
        bool isEdited = await model.EditProduct(added);
        Assert.True(isEdited);

        // Удаляем
        bool isDeleted = await model.DeleteProductById(id.Value);
        Assert.True(isDeleted);
    }
}