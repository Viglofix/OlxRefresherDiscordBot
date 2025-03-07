using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

namespace OlxRefresherDiscordBot.CLI.Services.Business.BasicBotConfiguration;
public class InteractivityConfigurationService : ContractInteractivityConfigurationService
{
    private readonly IDiscordClientService _discordClientService;
    private const int TimeSpanAmount = 10;
    public InteractivityConfigurationService(IDiscordClientService discordClientService)
    {
        _discordClientService = discordClientService;
    }
    public async Task SetInteracivityConfiguration()
    {
        var clinet = await _discordClientService.GetDiscordClient();
        clinet.UseInteractivity(new InteractivityConfiguration()
        {
            PollBehaviour = DSharpPlus.Interactivity.Enums.PollBehaviour.KeepEmojis,
            Timeout = TimeSpan.FromSeconds(TimeSpanAmount)
        });
    }
}
