using System.Text.Json.Serialization;

namespace OlxRefresherDiscordBot.CLI.Services.DataAccess.Json;
public struct ConfigJson
{
    [JsonPropertyName("token")]
    public string Token { get; set; }
    [JsonPropertyName("prefix")]
    public string Prefix { get; set; }
}
