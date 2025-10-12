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

    public async Task<int?> AddAndShowProduct(Product product)
    {
        HttpResponseMessage response = await api.AddProduct(product);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            int? newId = await getNewProductId(response);
            return newId;
        }
        Console.WriteLine($"Код ответа на добавление: {response.StatusCode}");
        Console.WriteLine($"Ошибка при добавлении товара: {response.ReasonPhrase}");
        return null;
    }

    public async Task DeleteProductById(int id)
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


    private async Task<int?> getNewProductId(HttpResponseMessage response)
    {
        string respBody = await response.Content.ReadAsStringAsync();
        try
        {
            AddProductResponse? addResp = System.Text.Json.JsonSerializer.Deserialize<AddProductResponse>(respBody);
            if (addResp != null) //&& addResp.status == 1
            {
                Console.WriteLine($"Товар успешно добавлен, id: {addResp.id}");
                return addResp.id;
            }
            else
            {
                Console.WriteLine("Ответ сервера не содержит id.");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка десериализации ответа: {ex.Message}");
            return null;
        }
    }
}