using ShopProducts;

class Program
{
    static async Task Main(string[] args)
    {
        ProductModel pm = new ProductModel();
        List<Product>? products = await pm.LoadProducts();

        if (products != null)
        {
            Console.WriteLine($"Получено товаров: {products.Count}");
            foreach (Product p in products)
            {
                Console.WriteLine($"{p.id}: {p.title} — {p.price}₽");
            }
        }
        else
        {
            Console.WriteLine("Не удалось получить список товаров или он пуст.");
        }
    }
}