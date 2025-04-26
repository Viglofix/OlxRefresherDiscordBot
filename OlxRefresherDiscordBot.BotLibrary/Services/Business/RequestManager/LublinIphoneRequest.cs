using OlxRefresherDiscordBot.BotLibrary.Services.Business.ObjectCreation;
using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Json;
using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Model;
using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Proxies;
using RestSharp;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.RequestManager;
public class LublinIphoneRequest : IRequestScheme
{
    private readonly IProxyService _proxyService;
    private readonly ConfigJsonBot jsonObj;
    private IRestClient restClient;

    public LublinIphoneRequest(IProxyService proxyService,ConfigJsonBot configJsonBot)
    {
        _proxyService = proxyService;
        jsonObj = configJsonBot;
    }

    public async Task<IOfferModel> GetResponseContent()
    { 
        _proxyService.AddProxies();
        string? randomProxy = _proxyService!.GetRandomProxy();
        restClient = new RestClientService().GetRestClient(randomProxy)!;

        var request = new RestRequest("https://www.olx.pl/apigateway/graphql", Method.Post);

        addHeaders(request);

        RestResponse response = await restClient.ExecuteAsync(request);
        var result = response.Content;

        IOfferModel obj = ObjectCreatorFromJson.GetClassTypeLightWeightContent(result!);
        return obj;
    }

    private void addHeaders(RestRequest request)
    {
        request.AddHeader(nameof(jsonObj.GraphQl.Headers.Accept).ToLower(), jsonObj.GraphQl!.Headers!.Accept!);
        request.AddHeader(nameof(jsonObj.GraphQl.Headers.AcceptLanguage).ToLower(), jsonObj.GraphQl.Headers!.AcceptLanguage!);
        request.AddHeader(nameof(jsonObj.GraphQl.Headers.CacheControl).ToLower(), jsonObj.GraphQl.Headers!.CacheControl!);
        request.AddHeader(nameof(jsonObj.GraphQl.Headers.ContentType).ToLower(), jsonObj.GraphQl.Headers!.ContentType!);
        request.AddHeader(nameof(jsonObj.GraphQl.Headers.Pragma).ToLower(), jsonObj.GraphQl.Headers!.Pragma!);
        request.AddHeader(nameof(jsonObj.GraphQl.Headers.Priority).ToLower(), jsonObj.GraphQl.Headers!.Priority!);
        request.AddHeader(nameof(jsonObj.GraphQl.Headers.SecChUa).ToLower(), jsonObj.GraphQl.Headers!.SecChUa!);
        request.AddHeader(nameof(jsonObj.GraphQl.Headers.SecChUaMobile).ToLower(), jsonObj.GraphQl.Headers!.SecChUaMobile!);
        request.AddHeader(nameof(jsonObj.GraphQl.Headers.SecChUaPlatform).ToLower(), jsonObj.GraphQl.Headers!.SecChUaPlatform!);
        request.AddHeader(nameof(jsonObj.GraphQl.Headers.SecFetchDest).ToLower(), jsonObj.GraphQl.Headers!.SecFetchDest!);
        request.AddHeader(nameof(jsonObj.GraphQl.Headers.SecFetchMode).ToLower(), jsonObj.GraphQl.Headers!.SecFetchMode!);
        request.AddHeader(nameof(jsonObj.GraphQl.Headers.SecFetchSite).ToLower(), jsonObj.GraphQl.Headers!.SecFetchSite!);

        request.AddHeader(nameof(jsonObj.GraphQl.Referrer).ToLower(), jsonObj.GraphQl.Referrer!);
        request.AddHeader(nameof(jsonObj.GraphQl.ReferrerPolicy).ToLower(), jsonObj.GraphQl.ReferrerPolicy!);
        request.AddHeader(nameof(jsonObj.GraphQl.Method).ToLower(), jsonObj.GraphQl.Method!);
        request.AddHeader(nameof(jsonObj.GraphQl.Mode).ToLower(), jsonObj.GraphQl.Mode!);
        request.AddHeader(nameof(jsonObj.GraphQl.Credentials).ToLower(), jsonObj.GraphQl.Credentials!);

        request.AddBody(jsonObj.GraphQl.Body!);
    }

}
