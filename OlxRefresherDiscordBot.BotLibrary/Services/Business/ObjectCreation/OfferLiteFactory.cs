using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Model;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.ObjectCreation;
public class OfferLiteFactory : IOfferFactory
{
    public IOfferModel CreateOfferObject(OfferType offerType)
    {
        switch (offerType)
        {
            case OfferType.LublinIphone:
                return new LublinIphoneModelLite();
            case OfferType.LublinCar:
                throw new NotImplementedException();
            default:
                throw new ArgumentNullException(nameof(offerType));
        }
    }
}
