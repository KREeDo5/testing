namespace ShopProducts;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;

public class ProductApi
{
    private static readonly HttpClient HttpClient = new HttpClient();
    private const string BaseUrl = "http://shop2.qatl.ru/shop/api";
    
    /// <summary>
    /// GET: Получить список всех товаров
    /// </summary>
    public async Task<List<Product>?> LoadProducts()
    {
        const string url = $"{BaseUrl}/products";
        HttpResponseMessage response = await HttpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Product>>(json);
    }

    /// <summary>
    /// GET: Удалить товар по его ID
    /// </summary>
    public async Task<HttpResponseMessage> DeleteProduct(int productId)
    {
        string url = $"{BaseUrl}/deleteproduct?id={productId}";
        HttpResponseMessage response = await HttpClient.GetAsync(url);
        return response;
    }
    
    /// <summary>
    /// POST: Добавление товара
    /// </summary>
    public async Task<HttpResponseMessage> AddProduct(Product product)
    {
        const string url = $"{BaseUrl}/addproduct";
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(url, product);
        return response;
    }
    
    /// <summary>
    /// POST: Редактирование товара
    /// </summary>
    public async Task<HttpResponseMessage> EditProduct(Product product)
    {   
        const string url = $"{BaseUrl}/editproduct";
        HttpResponseMessage response = await HttpClient.PostAsJsonAsync(url, product);
        return response;
    }
    
}