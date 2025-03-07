using OlxRefresherDiscordBot.CLI.Services.DataAccess.Json;
using System.Text;
using System.Text.Json;

namespace OlxRefresherDiscordBot.CLI.Services.Business.AuthToken;
public class AuthorizationJson : IAuthorizationJson
{
    private const string ConfigFileName = "config.json";
    public async Task<ConfigJson> GetConfigJson()
    {
        var baseDirectory = AppContext.BaseDirectory;
        var path = Path.Combine(baseDirectory, ConfigFileName);
        var json = string.Empty;
        using (var fs = new FileStream(path, FileMode.Open))
        {
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
            {
                json = await sr.ReadToEndAsync();
            }
        }

        var jsonConfig = JsonSerializer.Deserialize<ConfigJson>(json);
        return jsonConfig;
    }
}
