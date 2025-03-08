using DSharpPlus;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.BasicBotConfiguration;
    public interface IDiscordClientService
    {
        public Task<DiscordClient> GetDiscordClient();
    }
