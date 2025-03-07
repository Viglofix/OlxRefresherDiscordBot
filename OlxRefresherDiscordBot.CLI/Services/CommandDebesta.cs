using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

public class CommandDebesta : BaseCommandModule {

    [Command("tu")]
    public async Task SendTuTego(CommandContext ctx){
       await ctx.Channel.SendMessageAsync("tego").ConfigureAwait(false);
    }
}