using ShopProducts;

List<Product>? products = ProductModel.LoadProducts("products.json");
Console.WriteLine($"Загружено товаров: {products?.Count}");