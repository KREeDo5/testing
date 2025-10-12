namespace mock;
using System.Text.Json.Serialization;

public class RateResponse
{
    [JsonPropertyName("rate")]
    public decimal Rate { get; set; }
}