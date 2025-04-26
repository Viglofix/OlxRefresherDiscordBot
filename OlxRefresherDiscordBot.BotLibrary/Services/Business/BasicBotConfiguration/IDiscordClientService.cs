using DSharpPlus;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.BasicBotConfiguration;
    public interface IDiscordClientService
    {
        public DiscordClient GetDiscordClient(string configFileName);
    }
