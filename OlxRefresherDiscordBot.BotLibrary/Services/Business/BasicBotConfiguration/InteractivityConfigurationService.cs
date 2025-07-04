﻿using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.BasicBotConfiguration;
public class InteractivityConfigurationService : ContractInteractivityConfigurationService
{
    private readonly IDiscordClientService _discordClientService;
    private const int TimeSpanAmount = 10;
    public InteractivityConfigurationService(IDiscordClientService discordClientService)
    {
        _discordClientService = discordClientService;
    }
    public void SetInteracivityConfiguration()
    {
        var clinet = _discordClientService.GetDiscordClient("config.json");
        clinet.UseInteractivity(new InteractivityConfiguration()
        {
            PollBehaviour = DSharpPlus.Interactivity.Enums.PollBehaviour.KeepEmojis,
            Timeout = TimeSpan.FromSeconds(TimeSpanAmount)
        });
    }
}
