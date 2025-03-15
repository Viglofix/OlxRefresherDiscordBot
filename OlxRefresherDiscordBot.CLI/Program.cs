using Microsoft.Extensions.DependencyInjection;
using OlxRefresherDiscordBot.CLI;
using OlxRefresherDiscordBot.BotLibrary.Services.Business.AuthToken;
using OlxRefresherDiscordBot.BotLibrary.Services.Business.BasicBotConfiguration;
using OlxRefresherDiscordBot.BotLibrary.Bots;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddTransient<IAuthorizationJson, AuthorizationJson>();
        services.AddTransient<IDiscordClientService, DiscordClientService>();
        services.AddTransient<ICommandsNextConfigurationService, CommandsNextConfigurationService>();
        services.AddTransient<ContractInteractivityConfigurationService, InteractivityConfigurationService>();
        services.AddTransient<IBot, LublinIphoneBot>();
        services.AddTransient<IBotCar, LublinCarBot>();

        var serviceProvider = services.BuildServiceProvider();

        Bot bot = new(serviceProvider.GetService<IDiscordClientService>()!
            ,serviceProvider.GetService<ContractInteractivityConfigurationService>()!
            ,serviceProvider.GetService<ICommandsNextConfigurationService>()!
            ,serviceProvider.GetService<IAuthorizationJson>()!,
            serviceProvider.GetService<IBot>()!,
            serviceProvider.GetService<IBotCar>()!
            );

        await bot.RunAsync();
    }
}
