using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using OlxRefresherDiscordBot.CLI.Services.DataAccess.Json;
using System.Text.Json;
public class CommandDebesta : BaseCommandModule {

    [Command("tu")]
    public async Task SendTuTego(CommandContext ctx){
       await ctx.Channel.SendMessageAsync("tego").ConfigureAwait(false);
    }
    [Command("set_channel")]
    public async Task SetChannel(CommandContext ctx, [Description("Channel Id")] ulong number)
    {
        string ConfigFileName = "configChannel.json";
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

        var deserializedObj = JsonSerializer.Deserialize<ConfigJsonChannel>(jsonContent);
        deserializedObj.Channel = number;

        using (var fs = new FileStream(path, FileMode.Open))
        {
            using (var sr = new StreamWriter(fs))
            {
                var serializedObject = JsonSerializer.Serialize(deserializedObj);
                await sr.WriteAsync(serializedObject);
            }
        }
        await ctx.Channel.SendMessageAsync(deserializedObj.Channel.ToString()).ConfigureAwait(false);

    }
}