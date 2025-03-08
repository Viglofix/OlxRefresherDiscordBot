using DSharpPlus.CommandsNext;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.BasicBotConfiguration;
    public interface ICommandsNextConfigurationService
    {
        public Task<CommandsNextConfiguration> GetCommandsConfiguration();
    }
