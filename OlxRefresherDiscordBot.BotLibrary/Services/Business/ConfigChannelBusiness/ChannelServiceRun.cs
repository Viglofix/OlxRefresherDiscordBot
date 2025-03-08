namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.ConfigChannelBusiness;
    public class ChannelServiceRun
    {
        private ChannelServiceId _channelServiceId = new();
        public async void RunChannelValidator(ulong channelId,string pathChannel)
        {

        while (channelId == 0)
        {
            await Task.Delay(10000);
            try
            {
                channelId = await _channelServiceId.GetChannelId(pathChannel);
            }
            catch (IOException ex)
            {
                continue;
            }
        }
    }
    }
