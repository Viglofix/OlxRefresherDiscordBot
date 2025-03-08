using DSharpPlus.CommandsNext;
using OlxRefresherDiscordBot.BotLibrary.Services.Business.BasicBotConfiguration;
using OlxRefresherDiscordBot.BotLibrary.Services.Business.AuthToken;
using OlxRefresherDiscordBot.BotLibrary.Bots;

namespace OlxRefresherDiscordBot.CLI;
public class Bot
{
    private const string InitialMessage = "Bot is connected"; 
    private readonly IDiscordClientService _discordClientService;
    private readonly ContractInteractivityConfigurationService _interactivityConfigurationService;
    private readonly ICommandsNextConfigurationService _commandsNextConfigurationService;
    private readonly IAuthorizationJson _authorizationJson;
    private readonly IBot _bot;
    public CommandsNextExtension? commandsNext { get; private set; }
    public Bot(IDiscordClientService discordClientService, ContractInteractivityConfigurationService interactivityConfigurationService,ICommandsNextConfigurationService commandsNextConfigurationService,IAuthorizationJson authorizationJson,IBot bot)
    {
        _discordClientService = discordClientService;
        _interactivityConfigurationService = interactivityConfigurationService;
        _commandsNextConfigurationService = commandsNextConfigurationService;
        _authorizationJson = authorizationJson;
        _bot = bot;
    }

    public async Task RunAsync()
    {
        var clinet = await _discordClientService.GetDiscordClient(); 
        var cmdConfig = await _commandsNextConfigurationService.GetCommandsConfiguration();
        var configJson = await _authorizationJson.GetConfigJson();

        await _interactivityConfigurationService.SetInteracivityConfiguration(); 
     
        clinet.Ready += async (sender, args) => {
            await Task.Run(() => {
                Console.WriteLine(InitialMessage);
            }
            );
        };
       
        commandsNext = clinet.UseCommandsNext(cmdConfig);
        commandsNext.RegisterCommands<CommandDebestsa>();

        await clinet.ConnectAsync();
        await _bot.BotRunner();
    }
}
