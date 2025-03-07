using OlxRefresherDiscordBot.CLI.Services.DataAccess.Json;

namespace OlxRefresherDiscordBot.CLI.Services.Business.AuthToken;
    public interface IAuthorizationJson
    {
        public Task<ConfigJson> GetConfigJson();
    }
