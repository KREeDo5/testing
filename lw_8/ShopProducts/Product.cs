using ShopProducts.Helpers;

namespace ShopProducts;

using System.Text.Json.Serialization;

public class Product()
{   
    [JsonConverter(typeof(ParseStringToIntConverter))]
    public int? id { get; set; }

    [JsonConverter(typeof(ParseStringToIntConverter))]
    public int? category_id { get; set; } // От 1 до 15

    public string? title { get; set; }

    // Формируется из поля title через транслит на латиницу. Но если такой алиас существует, то добавляется префикс -0
    public string? alias { get; set; }
    public string? content { get; set; }

    [JsonConverter(typeof(ParseStringToDecimalConverter))]
    public decimal? price { get; set; }

    [JsonConverter(typeof(ParseStringToDecimalConverter))]
    public decimal? old_price { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProductStatus? status { get; set; }

    public string? keywords { get; set; }
    public string? description { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HitStatus? hit { get; set; }
}

public enum ProductStatus
{
    Inactive = 0,
    Active = 1
}

public enum HitStatus
{
    NotHit = 0,
    Hit = 1
}