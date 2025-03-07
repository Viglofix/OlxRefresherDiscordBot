using DSharpPlus.CommandsNext;
using OlxRefresherDiscordBot.CLI.Services.Business.AuthToken;

namespace OlxRefresherDiscordBot.CLI.Services.Business.BasicBotConfiguration;
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
        var json = await _authorizationJson.GetConfigJson();
        CommandsNextConfiguration commandConfig = new()
        {
            StringPrefixes = new string[] { json.Prefix },
            EnableDms = false,
            EnableMentionPrefix = true,
        };
        return commandConfig;
    }

}
