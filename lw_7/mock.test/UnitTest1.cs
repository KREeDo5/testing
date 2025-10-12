using System.Text.Json;

namespace mock.test;

public class UnitTest1
{
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
}