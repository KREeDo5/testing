using ShopProducts;

class Program
{
    static async Task Main(string[] args)
    {

        
        ProductModel model = new ProductModel();
        
        // Получение товаров
        List<Product>? productList = await model.GetProducts();
        Console.WriteLine(productList != null
            ? $"Получено товаров: {productList.Count}"
            : "Не удалось получить список товаров или он пуст.");
        
        
        // Добавляем новый товар
        Product testProduct = new Product
        {
            category_id = 5,
            title = "Котлы генерала Дмитрия",
            alias = "testoviy-tovar",
            content = "Ooops, podorozhalo",
            price = 228333.0m,
            old_price = 1200.0m,
            status = ProductStatus.Active,
            keywords = "тест,магазин,пример",
            description = "mena? mena!",
            hit = HitStatus.Hit
        };
        int? newProductId = await model.AddAndShowProduct(testProduct);
        if (newProductId != null)
        {   
            List<Product>? newProductList = await model.GetProducts();
            if (newProductList != null)
            {
                Product? addedProduct = newProductList.FirstOrDefault(product => product.id == newProductId);
                if (addedProduct != null)
                {   
                    addedProduct.title = "Dmitrii_" + addedProduct.title;
                    Boolean isEdited = await model.EditProduct(addedProduct);
                    if (isEdited)
                    {
                        Console.WriteLine($"Товар с id={newProductId} успешно изменён");
                    }
                    // Удаляем новый товар
                    Boolean isDeleted = await model.DeleteProductById(newProductId.Value);
                    if (isDeleted)
                    {
                        Console.WriteLine($"Товар с id={newProductId} успешно удалён");
                    }
                }
            }
        }

        // / ОЧИЩЕНИЕ товаров
        // ClearProductsModel clearModel = new ClearProductsModel();
        // await clearModel.DeleteLastProducts(2);
    }
}