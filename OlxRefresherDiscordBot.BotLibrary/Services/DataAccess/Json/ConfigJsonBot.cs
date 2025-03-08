using System.Text.Json.Serialization;

namespace OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Json;
public struct ConfigJsonBot
{
    [JsonPropertyName("latest_card")]
    public string LatestCard { get; set; }
}
