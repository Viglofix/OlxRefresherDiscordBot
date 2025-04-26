using OlxRefresherDiscordBot.BotLibrary.Services.Business.Helper;
using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Model.ProxyModel;

namespace OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Proxies;
    public class WebShareProxy : IProxyService
    {
        private const string WebShareProxyFilePath = "proxies/webshare.json";
        private static readonly object _lockObj = new();
        private IList<string>? _proxies;
        public void AddProxies()
        {
            lock (_lockObj) {
                var obj = JsonFileReadManager.GetClassTypeContent<WebshareModel>(WebShareProxyFilePath);
                _proxies = obj.HostNames!;
            }
        }
        public string? GetFilePath() => WebShareProxyFilePath;
        public string? GetRandomProxy()
        {
            int randomIndex = new Random().Next(_proxies!.Count());
            return _proxies![randomIndex];
        }
    }
