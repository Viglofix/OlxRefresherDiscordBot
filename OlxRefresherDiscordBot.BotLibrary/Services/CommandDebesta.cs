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
    [Command("set_channel_iphone")]
    public async Task SetChannelIphone(CommandContext ctx, [Description("Channel Iphone Id")] ulong number)
    {
        string ConfigFileName = "configChannel.json";
        var path = new CurrentDirectoryManager(ConfigFileName).CurrentPath!;

        try
        {
            var jsonContent =  JsonFileReadManager.GetJsonContent<ConfigJson>(path);
            var deserialized = JsonSerializer.Deserialize<ConfigJsonChannel>(jsonContent);
            deserialized.Channel = number;
            var serializedAgain = JsonSerializer.Serialize(deserialized);
            await JsonFileWriterManager.WriterJsonContent(path, serializedAgain);
        }
        catch(Exception ex) { }

        await ctx.Channel.SendMessageAsync(number.ToString());
    }
    [Command("set_channel_car")]
    public async Task SetChannelCar(CommandContext ctx, [Description("Channel Car Id")] ulong number)
    {
        string ConfigFileName = "configChannelCar.json";
        var path = new CurrentDirectoryManager(ConfigFileName).CurrentPath!;

        try
        {
            var jsonContent =  JsonFileReadManager.GetJsonContent<ConfigJson>(path);
            var deserialized = JsonSerializer.Deserialize<ConfigJsonChannel>(jsonContent);
            deserialized.Channel = number;
            var serializedAgain = JsonSerializer.Serialize(deserialized);
            await JsonFileWriterManager.WriterJsonContent(path, serializedAgain);
        }
        catch (Exception ex) { }

        await ctx.Channel.SendMessageAsync(number.ToString());
    }
}