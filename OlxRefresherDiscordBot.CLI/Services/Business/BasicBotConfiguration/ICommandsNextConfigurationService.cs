using DSharpPlus.CommandsNext;

namespace OlxRefresherDiscordBot.CLI.Services.Business.BasicBotConfiguration;
    public interface ICommandsNextConfigurationService
    {
        public Task<CommandsNextConfiguration> GetCommandsConfiguration();
    }
