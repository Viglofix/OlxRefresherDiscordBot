namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.Helper;
public class JsonFileWriterManager
{
    public async static Task WriterJsonContent(string pathChannel,string serialized)
    {
        await File.WriteAllTextAsync(pathChannel, string.Empty);
        await File.WriteAllTextAsync(pathChannel, serialized);
    }
}
