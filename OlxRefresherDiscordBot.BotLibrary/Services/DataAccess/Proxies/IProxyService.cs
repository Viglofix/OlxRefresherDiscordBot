namespace OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Proxies;
    public interface IProxyService
    {
        public void AddProxies();
        public string? GetRandomProxy();
        public string? GetFilePath();
    }
