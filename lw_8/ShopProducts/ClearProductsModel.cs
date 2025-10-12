using System.Net;

namespace ShopProducts;

public class ClearProductsModel
{
    readonly ProductApi api = new ProductApi();

    private async Task GetProducts()
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


    private async Task DeleteProductById(int id)
    {
        HttpResponseMessage response = await api.DeleteProduct(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine($"Товар с id={id} успешно удалён");
        }
        else
        {
            Console.WriteLine($"Код ответа на удаление: {response.StatusCode}");
            Console.WriteLine($"Ошибка при удалении товара: {response.ReasonPhrase}");
        }
    }

    public async Task DeleteLastProducts(int countToDelete)
    {
        List<Product>? products = await api.LoadProducts();
        if (products == null || products.Count == 0)
        {
            Console.WriteLine("Нет товаров для удаления.");
            return;
        }

        if (countToDelete <= 0)
        {
            Console.WriteLine("Некорректное количество для удаления.");
            return;
        }

        int actualCount = Math.Min(countToDelete, products.Count);
        List<Product> lastProducts = products.GetRange(products.Count - actualCount, actualCount);
        foreach (Product product in lastProducts)
        {
            if (product.id.HasValue)
                await DeleteProductById(product.id.Value);
            else
                Console.WriteLine("Товар без id, пропущен.");
        }
    }
}