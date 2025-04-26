using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Model;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.RequestManager;
    public interface IRequestScheme
    {
        public Task<IOfferModel> GetResponseContent();
    }
