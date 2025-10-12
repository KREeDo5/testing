using System.Net;

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
        }
        else
        {
            Console.WriteLine("Не удалось получить список товаров или он пуст.");
        }
    }
    
    public async Task AddAndShowProduct(Product product)
    {
        HttpResponseMessage response = await api.AddProduct(product);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine($"Товар успешно добавлен");
        }
        else
        {
            Console.WriteLine($"Код ответа на добавление: {response.StatusCode}");
            Console.WriteLine($"Ошибка при добавлении товара: {response.ReasonPhrase}");
        }

        Console.WriteLine($"Код ответа на добавление: {response.StatusCode}");

    }
}