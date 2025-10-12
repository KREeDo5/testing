namespace ShopProducts;

using System.Text.Json.Serialization;
public class Product
{
    public int? id { get; set; }
    public int? category_id { get; set; } // От 1 до 15
    public string? title { get; set; }
    public string? alias { get; set; } // Формируется из поля title через транслит на латиницу. Но если такой алиас существует, то добавляется префикс -0
    public string? content { get; set; }
    public decimal price { get; set; }
    public decimal? old_price { get; set; }
    public int? status { get; set; } // 0 или 1
    public string? keywords { get; set; }
    public string? description { get; set; }
    public int? hit { get; set; } // 0 или 1
}