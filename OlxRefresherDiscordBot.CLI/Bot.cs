using DSharpPlus.CommandsNext;
using System.Net;
using RestSharp;
using System.Text.Json;
using OlxRefresherDiscordBot.CLI.Services.DataAccess;
using OlxRefresherDiscordBot.CLI.Services.Business.BasicBotConfiguration;
using OlxRefresherDiscordBot.CLI.Services.Business.AuthToken;
using DSharpPlus;
using OlxRefresherDiscordBot.CLI.Services.DataAccess.Json;
using System.Diagnostics;

namespace OlxRefresherDiscordBot.CLI;
public class Bot
{
    private readonly IDiscordClientService _discordClientService;
    private readonly ContractInteractivityConfigurationService _interactivityConfigurationService;
    private readonly ICommandsNextConfigurationService _commandsNextConfigurationService;
    private readonly IAuthorizationJson _authorizationJson;
    private readonly object _lockObject = new();

    public CommandsNextExtension? commandsNext { get; private set; }
    public Bot(IDiscordClientService discordClientService, ContractInteractivityConfigurationService interactivityConfigurationService,ICommandsNextConfigurationService commandsNextConfigurationService,IAuthorizationJson authorizationJson)
    {
        _discordClientService = discordClientService;
        _interactivityConfigurationService = interactivityConfigurationService;
        _commandsNextConfigurationService = commandsNextConfigurationService;
        _authorizationJson = authorizationJson;
    }

    public async Task RunAsync()
    {
        var clinet = await _discordClientService.GetDiscordClient(); 
        var cmdConfig = await _commandsNextConfigurationService.GetCommandsConfiguration();
        var configJson = await _authorizationJson.GetConfigJson();

        await _interactivityConfigurationService.SetInteracivityConfiguration(); 
     
        clinet.Ready += async (sender, args) => {
            await Task.Run(() => {
                Console.WriteLine("Bot is connected");
            }
            );
        };
       
        commandsNext = clinet.UseCommandsNext(cmdConfig);
        commandsNext.RegisterCommands<CommandDebesta>();

        await clinet.ConnectAsync();

        await BotRunner(clinet);
    }

    private async Task BotRunner(DiscordClient? discordClient)
    {
        string ConfigFileName = "configBot.json";
        var baseDirectory = AppContext.BaseDirectory;
        var path = Path.Combine(baseDirectory, ConfigFileName);

        string jsonContent = string.Empty;

        using (var fileStream = new FileStream(path, FileMode.Open))
        {
            using (var streamReader = new StreamReader(fileStream))
            {
                jsonContent = await streamReader.ReadToEndAsync();
            }
        }

        // tutaj async
        var jsonObj = JsonSerializer.Deserialize<ConfigJsonBot>(jsonContent);

        // START WORKING SECTION

        List<string> listOfProxies = new();
        for (int i = 0; i < 100; i++)
        {
            if (i < 10)
            {
                listOfProxies.Add($"https://dc.smartproxy.com:1000{i}");
            }
            if (i >= 10)
            {
                listOfProxies.Add($"https://dc.smartproxy.com:100{i}");
            }
        }

        var task = Task.Run(async () =>
        {
            while (true)
            {
                // config Channel START
                string ConfigFileNameChannel = "configChannel.json";
                var baseDirectoryChannel = AppContext.BaseDirectory;
                var pathChannel = Path.Combine(baseDirectoryChannel, ConfigFileNameChannel);

                string jsonContent = string.Empty;

                using (var fileStream = new FileStream(pathChannel, FileMode.Open))
                {
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        jsonContent = await streamReader.ReadToEndAsync();
                    }
                }

                var deserializedChannel = JsonSerializer.Deserialize<ConfigJsonChannel>(jsonContent);
                var channelId = deserializedChannel.Channel;

                while (channelId == 0)
                {
                    await Task.Delay(10000);
                    try
                    {
                        using (var fileStream = new FileStream(pathChannel, FileMode.Open))
                        {
                            using (var streamReader = new StreamReader(fileStream))
                            {
                                jsonContent = await streamReader.ReadToEndAsync();
                            }
                        }
                        deserializedChannel = JsonSerializer.Deserialize<ConfigJsonChannel>(jsonContent);
                        channelId = deserializedChannel.Channel;
                    }
                    catch (IOException ex)
                    {

                    }
                }


                // config Channel END


                var randomIndex = new Random().Next(listOfProxies.Count());
                string randomProxy = listOfProxies[randomIndex];


                WebProxy proxy = new WebProxy(randomProxy)
                {
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential()
                    {
                        UserName = "spg7j78vwo",
                        Password = "QSb4eGlPpko0b1td4+",
                    }
                };

                var options = new RestClientOptions()
                {
                    Proxy = proxy,
                };
                var restClient = new RestClient(options);

                var requestPing = new RestRequest("https://httpbin.org/ip", Method.Get);
                var request = new RestRequest("https://www.olx.pl/api/v1/offers/?offset=0&limit=40&query=iphone&region_id=8&city_id=10119&sort_by=created_at%3Adesc&filter_refiners=spell_checker&suggest_filters=true&sl=194a20d203ex7d16ebf2", Method.Get);
                request.AddHeader("Cookie", "deviceGUID=77b41815-ffa2-425b-8298-332ab382377d; OptanonAlertBoxClosed=2025-01-26T09:59:19.211Z; eupubconsent-v2=CQL1kFgQL1kFgAcABBENBaF8AP_gAAAAAAYgJxNX_G_fbXlj8X50aftkeY1f99h6rsQxBhbJk-4FyLvW_JwX32EzNA16pqYKmRIAu3TBIQFlGIDUBUCgaogVrTDMaEyEoTNKJ6BEiFMRY2dYCFxvm4FDeQCY5trtd1d52R-t7dr83dzyy4hnn3Kp_2S1WJCdA5cgAAAAAAAAAAAAAAAQAAAAFAAAAQAIAAAAAAAAAAAAAAAAAAAAF_cAAABQSCEAAgABcAFAAVAA4AB4AEEALwA1AB4AEQAJgAVQA3gB6AD8AISAQwBEgCOAEsAK0AYAAw4BlAGWANkAc8A7gDvgHsAfEA-wD9gH-AgABFICLgIwARoAksBPwFBgKgAq4BcwC9AGKANEAbQA3ABxAEOwI9AkQBOwChwFHgKRAU2AtgBcgC7wF5gMNgZGBkgDJwGXAMzAZzA1cDWQGxgNvAbqA4IByYDlwIXBAC4ADgASABHAIOARwAmgBfQErAJlATaApABYQCxAF5AL_AYgAxYBkIDRgGpgNoAbcA3QcApAARAA4ADwALgAkAB-AEcANAAjgByAEAgIOAhABHACaAFQAOkAhABKwCYgEygJtAUmArsBYgC1AF0AL_AYIAxABiwDIQGTANGAamA14BtADbAG3AN0AceA5aBzoHPjoJQAC4AKAAqABwAEEALgA1AB4AEQAJgAVYAuAC6AGIAN4AegA_QCGAIkASwAowBWgDAAGGAMoAaIA2QBzwDuAO8Ae0A-wD9AH_ARQBGICOgJLAT8BQYCogKuAWIAucBeQF6AMUAbQA3ABxADqAH2AQ6Ai-BHoEiAJkATsAoeBR4FIQKaApsBVgCxQFsALdAXAAuQBdoC7wF5gL6AYaAx6BkYGSAMnAZVAywDLgGZgM5AabA1cDWAG3gN1AcWA5MBy5AAoAAgAB4AaAByAEcALEAX0BNoCkwFiALyAYIAzwBowDUwG2ANuAboA5YBz5CBAAAsACgALgAagBVAC4AGIAN4AegBHADAAHPAO4A7wB_gEUAJSAUGAqICrgFzAMUAbQA6gCPQFNAKsAWKAtEBcAC5AGRgMnAZySgRgAIAAWABQADgAPAAiABMACqAFwAMUAhgCJAEcAKMAVoAwABsgDvAH5AVEBVwC5gGKAOoAh0BEwCL4EegSIAo8BYoC2AF5wMjAyQBk4DOQGsANvJAEgALgBHAHcAQAAg4BHACoAJWATEAm0BSYC_wGLAMsAZ4A3IBugDlikDwABcAFAAVAA4ACAAGgAPAAiABMACqAGIAP0AhgCJAFGAK0AYAAygBogDZAHOAO-AfgB-gEWAIxAR0BJQCgwFRAVcAuYBeQDFAG0ANwAdQA9oB9gEOgImARfAj0CRAE7AKHAUgApsBVgCxQFsALgAXIAu0BeYC-gGGwMjAyQBk8DLAMuAZzA1gDWQG3gN1AcEA5MoAeAAuACQAFwAMgAjgCOAHIAO4AfYBAACDgFiANeAdsA_4CYgE2gKkAV2AugBeQDBAGLAMmAZ4A0YBqYDXgG6AOWAA.f_wAAAAAAAAA; OTAdditionalConsentString=1~89.318.320.1421.1423.1659.1985.1987.2008.2072.2135.2322.2465.2501.2958.2999.3028.3225.3226.3231.3234.3235.3236.3237.3238.3240.3244.3245.3250.3251.3253.3257.3260.3270.3272.3281.3288.3290.3292.3293.3296.3299.3300.3306.3307.3309.3314.3315.3316.3318.3324.3328.3330.3331.3531.3731.3831.4131.4531.4631.4731.4831.5231.6931.7235.7831.7931.8931.9731.10231.10631.10831.11031.11531.12831.13632.13731.14034.14237.14332.15731.16831.16931.21233.23031.25131.25731.25931.26031.26831.27731.27831.28031.28731.28831.29631.31631.32531.33631.34231.34631.36831.39131.39531.40632; _gcl_au=1.1.1408593071.1737885560; _hjSessionUser_1685071=eyJpZCI6IjYxM2Y3NTZiLWZmMzEtNThjZC04OTlmLWY0ZWRhMTRjNzQyMCIsImNyZWF0ZWQiOjE3Mzc4ODU1NjAzNzIsImV4aXN0aW5nIjp0cnVlfQ==; __gsas=ID=8a12ba9affa0a8ad:T=1738875121:RT=1738875121:S=ALNI_MbPEen1jVkyepxVXq8glGY7YibgFw; _fbp=fb.1.1739017807181.32002176725892937; WPabs=bccec3; invite=%22sr=google&cn=cpc--jobs-work-abroad-&td=1739196693%22; _gac_UA-124076552-1=1.1739196694.CjwKCAiA5Ka9BhB5EiwA1ZVtvE3OdfAsbZUvXz0ZvwNUNmfhvm2rhf7imGWP0q10k1IvR-7cP1EyfBoCFUoQAvD_BwE; _gcl_gs=2.1.k1$i1739196654$u192412532; _gcl_aw=GCL.1739196719.CjwKCAiA5Ka9BhB5EiwA1ZVtvE3OdfAsbZUvXz0ZvwNUNmfhvm2rhf7imGWP0q10k1IvR-7cP1EyfBoCFUoQAvD_BwE; _gid=GA1.2.1475644442.1740833051; datadome=mZhYJY1uckwWdByLxoW4qcVCg3fcDImtohbfT6DuIpTr2qPPo6fMCuCiBLyeoXarSt_0bVJnpXE30_1VloZSNUXGRaw8gPzVAwKCkHx3PRZjhWS3xkthi2XC05T4kIVt; mobile_default=desktop; auto-extension-info-seen=true; _ga_T4YRX9C789=GS1.1.1740851358.2.0.1740851358.0.0.0; __rtbh.uid=%7B%22eventType%22%3A%22uid%22%2C%22id%22%3A%221151394710%22%2C%22expiryDate%22%3A%222026-03-04T17%3A06%3A58.585Z%22%7D; laquesisff=a2b-000#aut-1425#aut-388#bl-2928#buy-2279#buy-2489#buy-4410#cou-1670#dat-2874#de-1927#de-1928#de-2170#de-2547#de-2559#de-2724#decision-256#do-2963#do-3418#euit-2250#euonb-114#f8nrp-1779#grw-124#jobs-7611#kuna-307#kuna-554#kuna-603#mart-1341#mou-1052#oesx-1437#oesx-2063#oesx-2798#oesx-2864#oesx-2926#oesx-3069#oesx-3150#oesx-3713#oesx-4295#oesx-645#oesx-867#olxeu-0000#pos-1302#rm-28#rm-707#rm-780#rm-824#rm-852#sd-2240#sd-2759#sd-3192#sd-570#srt-1289#srt-1346#srt-1434#srt-1593#srt-1758#srt-683#uacc-529#uacc-561#udp-1535#ul-3512#ul-3704#up-90; laquesis=dc-18@b#dc-263@b#dv-3239@b#erm-1722@a#jobs-6603@b#jobs-8617@a#jobs-8636@b#jobs-8920@c#jobs-8962@a#jobs-9018@a#oecs-927@b#oesx-4408@b#olxeu-42448@a#ream-28@b#search-1534@a; __gfp_64b=xJ5mnIcnwItf0lLvVkCdav4dCGupWKGSJiwZ1eF2iWj.I7|1737885371|2|||8:1:3; session_start_date=1741275848406; _hjSession_1685071=eyJpZCI6IjFjMTcwMDc5LTdmOTYtNDRiMS05ZmZjLWY5NmE1Y2M1YmM0MCIsImMiOjE3NDEyNzQwNDk4MzEsInMiOjAsInIiOjAsInNiIjowLCJzciI6MCwic2UiOjAsImZzIjowLCJzcCI6MX0=; PHPSESSID=9n0pd32sr6plpgtde57v194v68; lqstatus=1741275314|1956c057c5fx1d0b8135|search-1534#jobs-6603|||0; ab.storage.deviceId.535b859e-9238-4873-a90e-4c76b15ce078=g%3A6ba63870-c1a2-4f37-dfc9-7eeb721398d8%7Ce%3Aundefined%7Cc%3A1741109884283%7Cl%3A1741274055906; __gads=ID=07658c13218ba015:T=1738875122:RT=1741274466:S=ALNI_Ma0OEJyj1L5VbEUPWPoFu04eI93RQ; __eoi=ID=fd231f745f8971d1:T=1738875122:RT=1741274466:S=AA-AfjYpt75L3xD5pnF97dd0SXX_; OptanonConsent=isGpcEnabled=0&datestamp=Thu+Mar+06+2025+16%3A21%3A38+GMT%2B0100+(Central+European+Standard+Time)&version=202402.1.0&browserGpcFlag=0&isIABGlobal=false&hosts=&genVendors=V10%3A0%2C&consentId=e88812fc-6140-4b76-902f-37136f7d3506&interactionCount=1&isAnonUser=1&landingPath=NotLandingPage&groups=C0001%3A1%2CC0002%3A1%2CC0003%3A1%2CC0004%3A1%2Cgad%3A1&geolocation=PL%3B18&AwaitingReconsent=false; ldTd=true; _gat_clientNinja=1; ab.storage.sessionId.535b859e-9238-4873-a90e-4c76b15ce078=g%3A94deff08-09c7-d005-85ae-5c310142691a%7Ce%3A1741276318214%7Cc%3A1741274055905%7Cl%3A1741274518214; _ga=GA1.1.875466844.1737885559; __rtbh.lid=%7B%22eventType%22%3A%22lid%22%2C%22id%22%3A%22zoXknAtBhuGfXkfemUMC%22%2C%22expiryDate%22%3A%222026-03-06T15%3A21%3A59.043Z%22%7D; _ga_V1KE40XCLR=GS1.1.1741274056.31.1.1741274536.13.0.0; onap=194a20d203ex7d16ebf2-26-1956c057c5fx1d0b8135-98-1741276341");
                request.AddHeader("User-Agent", "User-Agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/132.0.0.0 Safari/537.36 OPR/117.0.0.0");

                RestResponse response = await restClient.ExecuteAsync(request);
                var result = response.Content;

                OfferData? obj = null;

                obj = JsonSerializer.Deserialize<OfferData>(result!);

                var url = obj!.data
               .Where(x => x.promotion.top_ad == false)
               .FirstOrDefault()!.url;

                if (url == jsonObj.LatestCard)
                {
                    await Task.Delay(300000);
                    continue;
                }
                else
                {
                    try
                    {
                        jsonObj.LatestCard = url;
                        var serialized = JsonSerializer.Serialize(jsonObj);

                        using (var fs = new FileStream(path, FileMode.Open))
                        {
                            using (var sr = new StreamWriter(fs))
                            {
                                await sr.WriteAsync(serialized);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }

                var price = obj!.data
                .Where(x => x.promotion.top_ad == false)
                .FirstOrDefault()!.Params[0].value.value;

                var image = obj!.data
                .Where(x => x.promotion.top_ad == false)
                .FirstOrDefault()!.photos[0].link;

                var imageFullHD = $"{image};s={2048}x{1080}";

                await discordClient!.SendMessageAsync(await discordClient.GetChannelAsync(channelId), "**-----------top--------------------**");
                await discordClient!.SendMessageAsync(await discordClient.GetChannelAsync(channelId), "| **Nowa Oferta w kategorii Lublin ** ");
                await discordClient.SendMessageAsync(await discordClient.GetChannelAsync(channelId),  $"| **link do oferty:** {url}");
                await discordClient.SendMessageAsync(await discordClient.GetChannelAsync(channelId),  "| **CENA: **" + price);
                await discordClient.SendMessageAsync(await discordClient.GetChannelAsync(channelId), $"| **link do zdjęcia:** {imageFullHD}");
                await discordClient!.SendMessageAsync(await discordClient.GetChannelAsync(channelId), "**-----------bottom--------------------**");

                await Task.Delay(300000);
            }
        });

        // END WORKING SECTION

        // await Task.WhenAll(task);
        await Task.Delay(-1);
    }
}
