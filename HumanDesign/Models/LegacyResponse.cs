using System.Text.Json.Serialization;

namespace HumanDesignApi.Models;

public class LegacyResponse<T>
{
    [JsonPropertyName("err")]
    public int Err { get; set; }

    [JsonPropertyName("msg")]
    public string Msg { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public T? Data { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }
}
