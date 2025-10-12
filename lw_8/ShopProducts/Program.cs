using ShopProducts;

class Program
{
    static async Task Main(string[] args)
    {
        ProductModel model = new ProductModel();
        await model.GetProducts();
    }
}