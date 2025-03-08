using System.Text.Json;
using OlxRefresherDiscordBot.BotLibrary.Services.Business.Helper;
using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Json;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.ConfigChannelBusiness;
public class ChannelServiceId : IChannelServiceId
{
    public async Task<ulong> GetChannelId(string path)
    {
        var pathChannel = new CurrentDirectoryManager(path).CurrentPath;
        string jsonContent = string.Empty;
        try
        {
            jsonContent = await JsonFileReadManager<ConfigJsonChannel>.GetJsonContent(pathChannel!);
        }
        catch (IOException ex) {
            return 0;
        }

        var deserializedChannel = JsonSerializer.Deserialize<ConfigJsonChannel>(jsonContent);
        var channelId = deserializedChannel.Channel;
        return channelId;
    }
}
