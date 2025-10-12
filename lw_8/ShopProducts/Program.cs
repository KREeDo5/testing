using ShopProducts;

class Program
{
    static async Task Main(string[] args)
    {
        ProductModel model = new ProductModel();
        // await model.GetProducts();


        // Product testProduct1 = new Product { title = "Товар", price = 10.5m };
        // await model.AddAndShowProduct(testProduct1);
        // Product testProduct2 = new Product
        // {
        //     // id = 12345, // или не задавай, если id проставляет сервер
        //     category_id = 2,
        //     title = "Тестовый товар",
        //     alias = "testoviy-tovar",
        //     content = "Описание тестового товара",
        //     price = 1000.0m,
        //     old_price = 1200.0m,
        //     status = ProductStatus.Active,
        //     keywords = "тест,магазин,пример",
        //     description = "Первый тестовый полностью заполненный товар (без id)",
        //     hit = HitStatus.Hit
        // };
        // await model.AddAndShowProduct(testProduct2);
        
        await model.DeleteProductById(363);
    }
}