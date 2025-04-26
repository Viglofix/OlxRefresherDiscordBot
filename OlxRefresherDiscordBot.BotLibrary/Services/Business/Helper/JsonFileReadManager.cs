using OlxRefresherDiscordBot.BotLibrary.Services.Business.ObjectCreation;
using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Model;
using System.Text.Json;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.Helper;
public class JsonFileReadManager
{
    private const string IOErrorMessage = "files access problem has appeared";
    public static string GetJsonContent<T>(string path)
    {
        string? serializedFinal = string.Empty;
        if (!File.Exists(path))
        {
            throw new Exception(IOErrorMessage);
        }

        string jsonContent = string.Empty;
        string serializedAgain = string.Empty;

        using var fileStream = new FileStream(path, FileMode.Open);
        using var streamReader = new StreamReader(fileStream);

        jsonContent = streamReader.ReadToEnd();
        var deserializedJson = JsonSerializer.Deserialize<T>(jsonContent);
        serializedFinal = JsonSerializer.Serialize(deserializedJson);

        return serializedFinal;
    }
    public static T GetClassTypeContent<T>(string path) where T : struct
    {
        if (!File.Exists(path))
        {
            throw new Exception(IOErrorMessage);
        }
        var serializedObj = GetJsonContent<T>(path);
        return JsonSerializer.Deserialize<T>(serializedObj);
    }

}
