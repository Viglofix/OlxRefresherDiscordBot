using System.Text.Json.Serialization;

namespace OlxRefresherDiscordBot.CLI.Services.DataAccess.Json;
public struct ConfigJsonBot
{
    [JsonPropertyName("latest_card")]
    public string LatestCard { get; set; }
}
