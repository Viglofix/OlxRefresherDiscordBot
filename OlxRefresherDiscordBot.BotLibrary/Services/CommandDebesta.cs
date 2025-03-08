using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using OlxRefresherDiscordBot.BotLibrary.Services.Business.Helper;
using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Json;
using System.Text.Json;
public class CommandDebestsa : BaseCommandModule {

    [Command("tu")]
    public async Task SendTuTego(CommandContext ctx){
       await ctx.Channel.SendMessageAsync("tego").ConfigureAwait(false);
    }
    [Command("set_channel")]
    public async Task SetChannel(CommandContext ctx, [Description("Channel Id")] ulong number)
    {
        string ConfigFileName = "configChannel.json";
        var path = new CurrentDirectoryManager(ConfigFileName).CurrentPath!;

        try
        {
            var jsonContent = await JsonFileReadManager<ConfigJsonChannel>.GetJsonContent(path);
            var deserialized = JsonSerializer.Deserialize<ConfigJsonChannel>(jsonContent);
            deserialized.Channel = number;
            var serializedAgain = JsonSerializer.Serialize(deserialized);
            await JsonFileWriterManager.WriterJsonContent(path, serializedAgain);
        }
        catch(Exception ex) { }

        await ctx.Channel.SendMessageAsync(number.ToString());
    }
}