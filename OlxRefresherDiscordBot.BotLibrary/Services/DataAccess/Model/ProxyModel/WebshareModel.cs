using System.Text.Json.Serialization;

namespace OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Model.ProxyModel;
    public struct WebshareModel
    {
        [JsonPropertyName("host_names")]    
        public List<string?> HostNames { get; set; }
    }
