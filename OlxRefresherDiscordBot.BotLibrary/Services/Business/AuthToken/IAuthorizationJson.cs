using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Json;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.AuthToken;
    public interface IAuthorizationJson
    {
        public Task<ConfigJson> GetConfigJson(string configFileName);
    }
