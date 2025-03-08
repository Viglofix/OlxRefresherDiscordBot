using System.Text.Json;
using OlxRefresherDiscordBot.BotLibrary.Services.Business.Helper;
using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Json;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.AuthToken;
public class AuthorizationJson : IAuthorizationJson
{
    private const string ConfigFileName = "config.json";
    private const string IOExceptionMessage = "Please set the appropriate token and Prefix";
    public async Task<ConfigJson> GetConfigJson()
    {
        var path = new CurrentDirectoryManager(ConfigFileName).CurrentPath!;
        string? json = string.Empty;

        try
        {
            json = await JsonFileReadManager<ConfigJson>.GetJsonContent(path);
        }
        catch(IOException ex)
        {
            throw new Exception($"{IOExceptionMessage} {ex.Message} ");
        }

        var jsonConfig = JsonSerializer.Deserialize<ConfigJson>(json);
        return jsonConfig;
    }
}
