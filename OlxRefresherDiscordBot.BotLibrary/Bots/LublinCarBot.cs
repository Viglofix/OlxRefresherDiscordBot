using OlxRefresherDiscordBot.BotLibrary.Services.Business.ConfigChannelBusiness;
using OlxRefresherDiscordBot.BotLibrary.Services.Business.Helper;
using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Json;
using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Proxies;
using OlxRefresherDiscordBot.BotLibrary.Services.Business.BasicBotConfiguration;
using System.Text.Json;
using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Model;
using DSharpPlus.Entities;
using OlxRefresherDiscordBot.BotLibrary.Services.Business.RequestManager;

namespace OlxRefresherDiscordBot.BotLibrary.Bots;
public class LublinCarBot : IBotCar
{
    private const string ConfigFileNameChannel = "configChannelCar.json";
    private const string ConfigBotFileName = "configBotCar.json";
    private const string WebshareFile = "proxies/webshare.json";

    private readonly IDiscordClientService _discordClientService;
    private readonly IProxyService _proxyService;
    public LublinCarBot(IDiscordClientService discordClientService, IProxyService proxyService)
    {
        _discordClientService = discordClientService;
        _proxyService = proxyService;
    }
    public async Task BotRunner(string configFileName)
    {
        var discordClient = _discordClientService.GetDiscordClient(configFileName);

        var path = new CurrentDirectoryManager(ConfigBotFileName).CurrentPath;
        string jsonContent = JsonFileReadManager.GetJsonContent<ConfigJsonBot>(path!);

        // async to implement
        var jsonObj = JsonSerializer.Deserialize<ConfigJsonBot>(jsonContent);

        // Proxy uzyc inne gdy catch wystapi
        // WebShareProxy proxyService = new WebShareProxy();
        // proxyService?.AddProxies(WebshareFile);

        while (true)
        {
            // config Channel START
            string pathChannel = new CurrentDirectoryManager(ConfigFileNameChannel).CurrentPath!;
            var channelId = new ChannelServiceId().GetChannelId(pathChannel!);

            ChannelServiceRun channelServiceRun = new ChannelServiceRun();
            await channelServiceRun.RunChannelValidator(channelId, pathChannel);
            // config Channel END


            IOfferModel obj = null!;
            try
            {
                obj = await new LublinCarRequest(_proxyService!, jsonObj!).GetResponseContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Task.Delay(10000);
                continue;
            }

            try
            {
                if (obj.Link == jsonObj!.LatestCard)
                {
                    await Task.Delay(25000);
                    continue;
                }
                else
                {
                    jsonObj.LatestCard = obj.Link!;
                    var serialized = JsonSerializer.Serialize(jsonObj);
                    await JsonFileWriterManager.WriterJsonContent(path!, serialized);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            DiscordEmbedBuilder? embed = new DiscordEmbedBuilder()
            {
                Title = $"**{obj.Title}**",
                Color = new DiscordColor(169, 216, 255),
            };

            embed.AddField("**Link do oferty** ", obj.Link);
            embed.AddField("**Cena 💵** ", obj.Price.ToString() + " PLN");
            embed.ImageUrl = obj.Picture;

            if (obj.Picture is null)
            {
                var emP = embed.Build();
                await discordClient.SendMessageAsync(await discordClient.GetChannelAsync(channelId), embed);
            }
            else
            {
                embed.ImageUrl = obj.Picture;
                var emP = embed.Build();
                await discordClient.SendMessageAsync(await discordClient.GetChannelAsync(channelId), embed);
            }

            await Task.Delay(25000);
        }
    }
}
