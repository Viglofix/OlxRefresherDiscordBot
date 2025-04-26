using System.Net;
using RestSharp;

namespace OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Proxies;
public class RestClientService
{
    public RestClient? GetRestClient(string? randomProxy)
    {
        WebProxy proxy = new WebProxy(randomProxy)
        {
            BypassProxyOnLocal = false,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential()
            {
                UserName = "hrqvdamh",
                Password = "ehyya1uwj7yk",
            }
        };

        var options = new RestClientOptions()
        {
            Proxy = proxy,
            AutomaticDecompression = DecompressionMethods.GZip
        };
        var restClient = new RestClient(options);
        return restClient;
    }
}