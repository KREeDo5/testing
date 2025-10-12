using ShopProducts;

class Program
{
    static async Task Main(string[] args)
    {
        ProductModel model = new ProductModel();
        
        // Получение товаров
        await model.GetProducts();
        
        // Добавляем новый товар
        Product testProduct = new Product
        {
            category_id = 5,
            title = "Тестовый товар by dmitrii",
            alias = "testoviy-tovar",
            content = "Ooops, podorozhalo",
            price = 228333.0m,
            old_price = 1200.0m,
            status = ProductStatus.Active,
            keywords = "тест,магазин,пример",
            description = "sry, guys",
            hit = HitStatus.Hit
        };
        int? newProductId = await model.AddAndShowProduct(testProduct);
        if (newProductId != null)
        {   
            // Удаляем новый товар
            await model.DeleteProductById(newProductId.Value);
        }
        
        /// ОЧИЩЕНИЕ товаров
        // ClearProductsModel clearModel = new ClearProductsModel();
        // await clearModel.DeleteLastProducts(54);
    }
}