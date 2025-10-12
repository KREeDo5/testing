using System.Net;

using ShopProducts.Entities;

namespace ShopProducts;

public class ProductModel
{
    readonly ProductApi api = new ProductApi();

    public async Task<List<Product>?> GetProducts()
    {
        List<Product>? products = await api.LoadProducts();
        return products;
    }

    public async Task<int?> AddProduct(Product product)
    {
        HttpResponseMessage response = await api.AddProduct(product);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            int? newId = await GetNewProductId(response);
            return newId;
        }

        Console.WriteLine($"Код ответа на добавление: {response.StatusCode}");
        Console.WriteLine($"Ошибка при добавлении товара: {response.ReasonPhrase}");
        return null;
    }

    public async Task<Boolean> DeleteProductById(int id)
    {
        HttpResponseMessage response = await api.DeleteProduct(id);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return true;
        }

        Console.WriteLine($"Код ответа на удаление: {response.StatusCode}");
        Console.WriteLine($"Ошибка при удалении товара: {response.ReasonPhrase}");
        return false;
    }

    public async Task<Boolean> EditProduct(Product product)
    {
        HttpResponseMessage response = await api.EditProduct(product);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return true;
        }

        Console.WriteLine($"Код ответа на изменение: {response.StatusCode}");
        Console.WriteLine($"Ошибка при изменении товара: {response.ReasonPhrase}");
        return false;
    }


    private static async Task<int?> GetNewProductId(HttpResponseMessage response)
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
    
    public static bool ValidateProduct(Product expected, Product actual)
    {
        return
            expected.title == actual.title &&
            expected.price == actual.price &&
            expected.category_id == actual.category_id &&
            expected.content == actual.content &&
            expected.old_price == actual.old_price &&
            expected.status == actual.status &&
            expected.keywords == actual.keywords &&
            expected.description == actual.description &&
            expected.hit == actual.hit;
    }
}