using DSharpPlus.CommandsNext;
using OlxRefresherDiscordBot.BotLibrary.Services.Business.BasicBotConfiguration;
using OlxRefresherDiscordBot.BotLibrary.Services.Business.AuthToken;
using OlxRefresherDiscordBot.BotLibrary.Bots;

namespace OlxRefresherDiscordBot.CLI;
public class Bot
{
    private const string InitialMessage = "Bot is connected version 1.0"; 
    private readonly IDiscordClientService _discordClientService;
    private readonly ContractInteractivityConfigurationService _interactivityConfigurationService;
    private readonly ICommandsNextConfigurationService _commandsNextConfigurationService;
    private readonly IBot _bot;
    private readonly IBotCar _botCar;
    public CommandsNextExtension? commandsNext { get; private set; }
    public Bot(IDiscordClientService discordClientService, ContractInteractivityConfigurationService interactivityConfigurationService,ICommandsNextConfigurationService commandsNextConfigurationService,IAuthorizationJson authorizationJson,IBot bot,IBotCar botCar)
    {
        _discordClientService = discordClientService;
        _interactivityConfigurationService = interactivityConfigurationService;
        _commandsNextConfigurationService = commandsNextConfigurationService;
        _bot = bot;
        _botCar = botCar;
    }

    public async Task RunAsync()
    {
        var clinet = await _discordClientService.GetDiscordClient("config.json"); 
        var cmdConfig = await _commandsNextConfigurationService.GetCommandsConfiguration();

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

        // await _bot.BotRunner();
        // await _botCar.BotRunner();

        List<Task> tasks = new List<Task>();
        tasks.Add(Task.Run( () => _bot.BotRunner("config.json") ));
        tasks.Add(Task.Run( () => _botCar.BotRunner("configCar.json")));

        await Task.WhenAll(tasks);

        await Task.Delay(-1);
    }
}
