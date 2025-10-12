namespace mock;
using System.Text.Json;

class Program
{
    static async Task Main()
    {
        String from = "USD";
        String to = "RUB";
        String apiUrl = $"http://localhost:4545/rate?from={from}&to={to}";

        using HttpClient httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
        string json = await response.Content.ReadAsStringAsync();
        RateResponse? rateResp = JsonSerializer.Deserialize<RateResponse>(json);

        Console.WriteLine(rateResp != null ? $"Курс {from} -> {to}: {rateResp.Rate}" : "Ошибка при получении курса.");
    }
}