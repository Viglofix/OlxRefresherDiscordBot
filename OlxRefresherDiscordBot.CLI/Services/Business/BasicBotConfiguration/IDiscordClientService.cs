using DSharpPlus;

namespace OlxRefresherDiscordBot.CLI.Services.Business.BasicBotConfiguration;
    public interface IDiscordClientService
    {
        public Task<DiscordClient> GetDiscordClient();
    }
