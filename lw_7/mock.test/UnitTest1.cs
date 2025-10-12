using System.Text.Json;

namespace mock.test;

public class UnitTest1
{   
    // Позитивный тест на получение курса USD к RUB
    [Fact]
    public async Task TestGetRate_USD_RUB()
    {
        string from = "USD";
        string to = "RUB";
        string apiUrl = $"http://localhost:4545/rate?from={from}&to={to}";

        using HttpClient httpClient = new HttpClient();

   
        HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
        string json = await response.Content.ReadAsStringAsync();
        RateResponse? rateResp = JsonSerializer.Deserialize<RateResponse>(json);

   
        Assert.NotNull(rateResp);
        Assert.Equal(93, rateResp.Rate);
    }
    
    // Негативный тест на ошибку при несуществующей валюте
    [Fact]
    public async Task GetRate_WithWrongCurrency_Throws()
    {
        string from = "USD";
        string to = "XXX";
        string apiUrl = $"http://localhost:4545/rate?from={from}&to={to}";

        using HttpClient httpClient = new HttpClient();

        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
        });
    }
}