using System.Text.Json;
using System.Net.Http.Headers;
using HumanDesign.Application.Interfaces;

namespace HumanDesign.Application.Services.Reference;
public class OpenAIContentGenerator(IConfiguration config, HttpClient http) : IAIContentGenerator
{
    private readonly HttpClient _http = http;
    private readonly string _apiKey = config["OpenAI:ApiKey"]!;

    public async Task<string?> GenerateAsync(string type, string key, string level)
    {
        var prompt = $"""
        Write a Human Design interpretation.

        Content Type: {type}
        Key: {key}
        Level: {level}

        Level meanings:
        Preview = short 1 paragraph
        Summary = 3 paragraphs
        Detail = deep explanation

        Tone: spiritual, professional, clear.
        """;

        var request = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);

        var response = await _http.PostAsJsonAsync(
            "https://api.openai.com/v1/chat/completions",
            request);

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();

        return json
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString()!;
    }
}