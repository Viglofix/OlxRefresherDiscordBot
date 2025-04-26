using System.Text.Json.Serialization;

namespace OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Json;
public class ConfigJsonBot
{
    [JsonPropertyName("latest_card")]
    public string? LatestCard { get; set; }
    [JsonPropertyName("graphQl")]
    public GraphQlConfig? GraphQl { get; set; }
}
public class GraphQlConfig
{
    [JsonPropertyName("headers")]
    public Headers? Headers { get; set; }

    [JsonPropertyName("referrer")]
    public string? Referrer { get; set; }

    [JsonPropertyName("referrerPolicy")]
    public string? ReferrerPolicy { get; set; }

    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [JsonPropertyName("method")]
    public string? Method { get; set; }

    [JsonPropertyName("mode")]
    public string? Mode { get; set; }

    [JsonPropertyName("credentials")]
    public string? Credentials { get; set; }
}

public class Headers
{
    [JsonPropertyName("accept")]
    public string? Accept { get; set; }

    [JsonPropertyName("accept-language")]
    public string? AcceptLanguage { get; set; }

    [JsonPropertyName("cache-control")]
    public string? CacheControl { get; set; }

    [JsonPropertyName("content-type")]
    public string? ContentType { get; set; }

    [JsonPropertyName("pragma")]
    public string? Pragma { get; set; }

    [JsonPropertyName("priority")]
    public string? Priority { get; set; }

    [JsonPropertyName("sec-ch-ua")]
    public string? SecChUa { get; set; }

    [JsonPropertyName("sec-ch-ua-mobile")]
    public string? SecChUaMobile { get; set; }

    [JsonPropertyName("sec-ch-ua-platform")]
    public string? SecChUaPlatform { get; set; }

    [JsonPropertyName("sec-fetch-dest")]
    public string? SecFetchDest { get; set; }

    [JsonPropertyName("sec-fetch-mode")]
    public string? SecFetchMode { get; set; }

    [JsonPropertyName("sec-fetch-site")]
    public string? SecFetchSite { get; set; }
}