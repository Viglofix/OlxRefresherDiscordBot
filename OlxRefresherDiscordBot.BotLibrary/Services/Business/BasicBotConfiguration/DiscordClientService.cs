using DSharpPlus;
using Microsoft.Extensions.Logging;
using OlxRefresherDiscordBot.BotLibrary.Services.Business.AuthToken;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.BasicBotConfiguration;
public class DiscordClientService : IDiscordClientService
{
    private readonly IAuthorizationJson _authorizationJson;
    public DiscordClientService(IAuthorizationJson authorizationJson)
    {
        _authorizationJson = authorizationJson;
    }

    public async Task<DiscordClient> GetDiscordClient(string configFileName)
    {
        var jsonConfig = await _authorizationJson.GetConfigJson(configFileName);
        DiscordConfiguration configuration = new()
        {
            Token = jsonConfig.Token,
            TokenType = TokenType.Bot,
            AutoReconnect = true,
            MinimumLogLevel = LogLevel.Debug,
            Intents = DiscordIntents.MessageContents | DiscordIntents.AllUnprivileged | DiscordIntents.GuildMembers
        };

        return new DiscordClient(configuration);
    }
}
