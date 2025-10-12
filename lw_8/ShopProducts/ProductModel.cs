namespace ShopProducts;
using System.Text.Json;

public class ProductModel
{
    public static List<Product>? LoadProducts(string path)
    {
        string json = File.ReadAllText(path);
        List<Product>? products = JsonSerializer.Deserialize<List<Product>>(json);
        return products;
    }
    
    /*
    public async Task<List<Product>?> LoadProducts()
    {
        //  Список всех товаров (GET): BASE_URL/api/products
    }

    public async Task<HttpResponseMessage> DeleteProduct(Product product)
    {
        // Удаление (GET): BASE_URL/api/deleteproduct?id=ID
    }
    
    public async Task<HttpResponseMessage> AddProduct(Product product)
    {
        // Добавление (POST): BASE_URL/api/addproduct
    }
    
    public async Task<HttpResponseMessage> EditProduct(Product product)
    {
        // Редактирование (POST): BASE_URL/api/editproduct
    }
    */
}