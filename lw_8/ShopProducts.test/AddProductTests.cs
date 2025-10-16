using ShopProducts.Entities;
using ShopProducts.TestData;

namespace ShopProducts.test;

public class AddProductTests
{
    // Валидные товары
    [Theory]
    [MemberData(nameof(ProductTestData.ValidProducts), MemberType = typeof(ProductTestData))]
    public async Task Test_AddValidProduct(Product product)
    {
        ProductModel model = new ProductModel();
        int? id = await model.AddProduct(product);
        Assert.NotNull(id);

        List<Product>? products = await model.GetProducts();
        Assert.NotNull(products);
        Assert.Contains(products, p => p.id == id);
        
        await model.DeleteProductById(id.Value);
    }
    
    [Theory]
    [MemberData(nameof(ProductTestData.InvalidCategoryProducts), MemberType = typeof(ProductTestData))]
    public async Task Test_AddInvalidProductWithWrongCategoryId(Product product)
    {
        ProductModel model = new ProductModel();
        int? id = await model.AddProduct(product);
        if (id != null) await model.DeleteProductById(id.Value);
        Assert.Null(id);
    }
    
    [Theory]
    [MemberData(nameof(ProductTestData.EmptyOrSpaceFieldsProducts), MemberType = typeof(ProductTestData))]
    public async Task Test_AddProductWithEmptyOrSpacesFields(Product product)
    {
        ProductModel model = new ProductModel();
        int? id = await model.AddProduct(product);
        if (id != null) await model.DeleteProductById(id.Value);
        Assert.Null(id);
    }
    
    [Theory]
    [MemberData(nameof(ProductTestData.SpecialCharProducts), MemberType = typeof(ProductTestData))]
    public async Task Test_AddProductWithSpecialCharacters(Product product)
    {
        ProductModel model = new ProductModel();
        int? id = await model.AddProduct(product);
        if (id != null) await model.DeleteProductById(id.Value);
        Assert.NotNull(id);
    }

    // Очень длинный title
    [Theory]
    [MemberData(nameof(ProductTestData.LongTitleProducts), MemberType = typeof(ProductTestData))]
    public async Task Test_AddProductWithVeryLongTitle(Product product)
    {
        ProductModel model = new ProductModel();
        int? id = await model.AddProduct(product);
        if (id != null) await model.DeleteProductById(id.Value);
        Assert.True(id == null);
    }

    // Цена == 0 или null
    [Theory]
    [MemberData(nameof(ProductTestData.ZeroPriceProducts), MemberType = typeof(ProductTestData))]
    public async Task Test_AddProductWithZeroOrMissingPrice(Product product)
    {
        ProductModel model = new ProductModel();
        int? id = await model.AddProduct(product);
        if (id != null) await model.DeleteProductById(id.Value);
        Assert.Null(id);
    }

    // Дубли title
    [Theory]
    [MemberData(nameof(ProductTestData.SameTitleProducts), MemberType = typeof(ProductTestData))]
    public async Task Test_AddSeveralValidProductWithSameTitle(Product product)
    {
        ProductModel model = new ProductModel();
        int? id1 = await model.AddProduct(product);
        int? id2 = await model.AddProduct(product);
        Assert.NotNull(id1);
        Assert.NotNull(id2);
        Assert.NotEqual(id1, id2);
        await model.DeleteProductById(id1.Value);
        await model.DeleteProductById(id2.Value);
    }
}