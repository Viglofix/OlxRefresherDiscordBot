using System.Text.Json;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.Helper;
public class JsonFileReadManager<T> where T : struct
{
    public async static Task<string> GetJsonContent(string pathChannel) 
    {
            string jsonContent = string.Empty;
            string serializedAgain = string.Empty;
        using (var fileStream = new FileStream(pathChannel,FileMode.Open))
            {
            using (var streamReader = new StreamReader(fileStream))
            {
                jsonContent = await streamReader.ReadToEndAsync();
                var deserializedJson = JsonSerializer.Deserialize<T>(jsonContent);
                serializedAgain = JsonSerializer.Serialize(deserializedJson);
            }
            }
            return serializedAgain;
        }
    }
