using System.Text.Json;

namespace ShopProducts.Helpers;

public class LoggingHandler(HttpMessageHandler innerHandler) : DelegatingHandler(innerHandler)
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        PrintMeta("API REQUEST");
        Console.WriteLine($"Method:      {request.Method}");
        Console.WriteLine($"RequestUri:  {request.RequestUri}");
        Console.WriteLine("Headers:");
        foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers)
            Console.WriteLine($"   {header.Key}: {string.Join(", ", header.Value)}");
        if (request.Content != null)
        {
            Console.WriteLine("Content-Headers:");
            foreach (KeyValuePair<string, IEnumerable<string>> header in request.Content.Headers)
                Console.WriteLine($"   {header.Key}: {string.Join(", ", header.Value)}");
            string body = await request.Content.ReadAsStringAsync();
            Console.WriteLine("Body:");
            PrettyPrintJson(body);
        }

        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

        PrintMeta("API RESPONSE");
        Console.WriteLine($"StatusCode:  {(int)response.StatusCode} {response.StatusCode}");
        Console.WriteLine("Headers:");
        foreach (KeyValuePair<string, IEnumerable<string>> header in response.Headers)
            Console.WriteLine($"   {header.Key}: {string.Join(", ", header.Value)}");
        Console.WriteLine("Content-Headers:");
        foreach (KeyValuePair<string, IEnumerable<string>> header in response.Content.Headers)
            Console.WriteLine($"   {header.Key}: {string.Join(", ", header.Value)}");

        string responseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Body:");
        PrettyPrintJson(responseBody);

        Console.ResetColor();
        return response;
    }

    private static void PrettyPrintJson(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            Console.WriteLine("<empty>");
            return;
        }

        try
        {
            JsonDocument doc = System.Text.Json.JsonDocument.Parse(str);
            string formatted = System.Text.Json.JsonSerializer.Serialize(doc,
                new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(formatted);
        }
        catch
        {
            Console.WriteLine(str);
        }
    }

    private static void PrintMeta(string section)
    {
        Console.WriteLine($"\n========== {section} ==========");
        Console.WriteLine($"[Meta] Время: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");
        Console.WriteLine($"[Meta] Метод: {section}");
    }
}