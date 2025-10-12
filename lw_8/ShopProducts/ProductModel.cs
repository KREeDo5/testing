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
            // foreach (Product p in products)
            // {
            //     Console.WriteLine($"{p.id}: {p.title} — {p.price}");
            // }
        }
        else
        {
            Console.WriteLine("Не удалось получить список товаров или он пуст.");
        }
    }
    
    public async Task AddAndShowProduct(Product product)
    {
        HttpResponseMessage response = await api.AddProduct(product);
        Console.WriteLine($"Код ответа на добавление: {response.StatusCode}");
        await ShowThreeLastProducts();
    }


    private async Task ShowThreeLastProducts()
    {
        List<Product>? products = await api.LoadProducts();

        if (products != null)
        {
            Console.WriteLine($"Получено товаров: {products.Count}");
            IEnumerable<Product> lastThree = products.TakeLast(3);
            Console.WriteLine($"Последние 3 товара:");
            foreach (Product p in lastThree)
            {
                Console.WriteLine($"{p.id}: {p.title} — {p.price}");
            }
        }
        else
        {
            Console.WriteLine("Не удалось получить список товаров или он пуст.");
        }
    }
}