using DSharpPlus;
using DSharpPlus.CommandsNext;
using OlxRefresherDiscordBot.BotLibrary.Services.Business.AuthToken;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.BasicBotConfiguration;
public class CommandsNextConfigurationService : ICommandsNextConfigurationService
{
    private readonly IAuthorizationJson _authorizationJson;
    private readonly IDiscordClientService _discordClientService;
    public CommandsNextConfigurationService(IAuthorizationJson authorizationJson,IDiscordClientService discordClientService)
    {
        _authorizationJson = authorizationJson;
        _discordClientService = discordClientService;
    }

    public async Task<CommandsNextConfiguration> GetCommandsConfiguration()
    {
        var json = await _authorizationJson.GetConfigJson("config.json");
        CommandsNextConfiguration commandConfig = new()
        {
            StringPrefixes = new string[] { json.Prefix },
            EnableDms = false,
            EnableMentionPrefix = true,
        };
        return commandConfig;
    }

}
