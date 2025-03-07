using Microsoft.Extensions.DependencyInjection;
using OlxRefresherDiscordBot.CLI;
using OlxRefresherDiscordBot.CLI.Services.Business.AuthToken;
using OlxRefresherDiscordBot.CLI.Services.Business.BasicBotConfiguration;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddTransient<IAuthorizationJson, AuthorizationJson>();
        services.AddTransient<IDiscordClientService, DiscordClientService>();
        services.AddTransient<ICommandsNextConfigurationService, CommandsNextConfigurationService>();
        services.AddTransient<ContractInteractivityConfigurationService, InteractivityConfigurationService>();

        var serviceProvider = services.BuildServiceProvider();

        Bot bot = new(serviceProvider.GetService<IDiscordClientService>()!
            ,serviceProvider.GetService<ContractInteractivityConfigurationService>()!
            ,serviceProvider.GetService<ICommandsNextConfigurationService>()!
            ,serviceProvider.GetService<IAuthorizationJson>()!);

        await bot.RunAsync();
    }
}
