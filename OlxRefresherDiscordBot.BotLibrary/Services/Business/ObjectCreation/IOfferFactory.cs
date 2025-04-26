using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Model;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.ObjectCreation;
    public interface IOfferFactory
    {
        IOfferModel CreateOfferObject(OfferType offerType);
    }
