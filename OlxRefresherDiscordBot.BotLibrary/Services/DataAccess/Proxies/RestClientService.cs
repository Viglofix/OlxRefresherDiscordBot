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
                UserName = "spg7j78vwo",
                Password = "QSb4eGlPpko0b1td4+",
            }
        };

        var options = new RestClientOptions()
        {
            Proxy = proxy,
        };
        var restClient = new RestClient(options);
        return restClient;
    }
}