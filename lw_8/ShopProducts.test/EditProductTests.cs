namespace ShopProducts.test;

using Entities;
using TestData;

public class EditProductTests
{
     /// <summary>
    /// Редактирование существующего товара.
    /// </summary>
    [Theory]
    [MemberData(nameof(ProductTestData.ValidProducts), MemberType = typeof(ProductTestData))]
    public async Task Test_EditExistingProduct(Product product)
    {
        ProductModel model = new ProductModel();
        int? id = await model.AddProduct(product);
        Assert.NotNull(id);

        List<Product>? products = await model.GetProducts();
        Assert.NotNull(products);
        Product? newProduct = products.FirstOrDefault(p => p.id == id);

        Assert.NotNull(newProduct);
        newProduct.title += "_edited";
        bool edited = await model.EditProduct(newProduct);
        Assert.True(edited);
        await model.DeleteProductById(id.Value);
    }

    /// <summary>
    /// Редактирование несуществующего товара
    /// </summary>
    [Fact]
    public async Task Test_EditNotExistingProduct()
    {
        ProductModel model = new ProductModel();
        Product fake = new Product { id = -99999, title = "Fake", price = 1m, category_id = 1, status = ProductStatus.Active, hit = HitStatus.NotHit };
        bool edited = await model.EditProduct(fake);
        Assert.False(edited);
    }
    
    /// <summary>
    /// Изменение товара без указания его id
    /// </summary>
    [Theory]
    [MemberData(nameof(ProductTestData.ValidProducts), MemberType = typeof(ProductTestData))]
    public async Task Test_EditProductWithoutId(Product product)
    {
        ProductModel model = new ProductModel();
        int? id = await model.AddProduct(product);
        Assert.NotNull(id);

        product.id = null;
        bool edited = await model.EditProduct(product);
        Assert.False(edited);

        await model.DeleteProductById(id.Value);
    }
    
    /// <summary>
    /// Изменение товара на дублирующий title
    /// </summary>
    [Theory]
    [MemberData(nameof(ProductTestData.SameTitleProducts), MemberType = typeof(ProductTestData))]
    public async Task Test_EditProductWithDublicateTitle(Product product)
    {
        ProductModel model = new ProductModel();
        int? id1 = await model.AddProduct(product);
        int? id2 = await model.AddProduct(product);

        Assert.NotNull(id1);
        Assert.NotNull(id2);

        Product? prod1 = (await model.GetProducts() ?? throw new InvalidOperationException()).FirstOrDefault(p => p.id == id1);
        Product? prod2 = (await model.GetProducts() ?? throw new InvalidOperationException()).FirstOrDefault(p => p.id == id2);
        
        Assert.NotNull(prod1);
        Assert.NotNull(prod2);
        prod2.title = prod1.title;

        bool edited = await model.EditProduct(prod2);
        Assert.True(edited);

        await model.DeleteProductById(id1.Value);
        await model.DeleteProductById(id2.Value);
    }

    /// <summary>
    /// Редактирование товара с пустыми параметрами.
    /// </summary>
    [Theory]
    [MemberData(nameof(ProductTestData.ValidProducts), MemberType = typeof(ProductTestData))]
    public async Task Test_EditProductAllFieldsToEmpty(Product product)
    {
        ProductModel model = new ProductModel();
        int? id = await model.AddProduct(product);
        Assert.NotNull(id);

        List<Product>? products = await model.GetProducts();
        Assert.NotNull(products);
        Product? item = products.FirstOrDefault(p => p.id == id);

        Assert.NotNull(item);
        item.title = "";
        item.content = "";
        item.alias = "";
        item.keywords = "";
        item.description = "";
        bool edited = await model.EditProduct(item);
        Assert.False(edited); //Заменить на True, если допустимо

        await model.DeleteProductById(id.Value);
    }
}