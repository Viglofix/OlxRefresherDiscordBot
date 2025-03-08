using System.Text.Json.Serialization;

namespace OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Json;
public struct ConfigJsonChannel
{
    [JsonPropertyName("channel")]
    public ulong Channel { get; set; }
}
