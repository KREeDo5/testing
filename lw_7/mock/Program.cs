namespace mock;

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

        Console.WriteLine($"Курс {from} -> {to}: {json}");
    }
}