namespace ShopProducts;

public class ProductModel
{
    readonly ProductApi api = new ProductApi();
    
    public async Task GetProducts()
    {
        List<Product>? products = await api.LoadProducts();

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