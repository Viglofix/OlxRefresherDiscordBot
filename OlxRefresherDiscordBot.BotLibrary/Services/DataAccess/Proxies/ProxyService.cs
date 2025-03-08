namespace OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Proxies;
    public class ProxyService
    {
        public List<string> Proxies { get; private set; }

        public ProxyService()
        {
            Proxies = new();
        }
        public void AddProxies()
        {
        for (int i = 0; i < 100; i++)
        {
            if (i < 10)
            {
                Proxies.Add($"https://dc.smartproxy.com:1000{i}");
            }
            if (i >= 10)
            {
                Proxies.Add($"https://dc.smartproxy.com:100{i}");
            }
        }
        }
        public string? GetRandomProxy()
        {
            var randomIndex = new Random().Next(Proxies.Count());
            return Proxies[randomIndex];
        }
    }
