namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.ConfigChannelBusiness;
    public interface IChannelServiceId
    {
        public Task<ulong> GetChannelId(string pathName);
    }
