using DSharpPlus;
using OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Model;
using System.Text.Json;

namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.ObjectCreation;
    public class ObjectCreatorFromJson
    {
    // Document Comments

    /// <summary>
    /// Returns deserialized object using lightweight
    /// way by using JsonDocument.Parse
    public static IOfferModel GetClassTypeLightWeightContent(string path)
    {
        string? title = null;
        string? url = null;
        decimal? price = null;
        string? photo = null;

        IOfferModel? offerLiteFactory = null;
        using (JsonDocument jsonDocument = JsonDocument.Parse(path))
        {
            JsonElement root = jsonDocument.RootElement;
            offerLiteFactory = new OfferLiteFactory()
               .CreateOfferObject(OfferType.LublinIphone);

            var data = root.GetProperty("data");
            var clientCompatibleListings = data.GetProperty("clientCompatibleListings");
            var _data = clientCompatibleListings.GetProperty("data");

            foreach(var el in _data.EnumerateArray())
            {
                var obj = el.Deserialize<JsonElement>();
                var promotion = obj.GetProperty("promotion");
                var top_ad = promotion.GetProperty("top_ad").GetBoolean();
                var _params = obj.GetProperty("params").Deserialize<JsonElement>();
                var _photos = obj.GetProperty("photos").Deserialize<JsonElement>();

                if (top_ad == false)
                {
                    title = obj.GetProperty("title").GetString();
                    url = obj.GetProperty("url").GetString();

                    try
                    {
                        foreach (var value in _params.EnumerateArray())
                        {
                            var values = value.GetProperty("value");
                            price = values.GetProperty("value").GetDecimal();
                            break;
                        }
                        foreach(var pic in _photos.EnumerateArray())
                        {
                            var value = pic.GetProperty("link");
                            photo = value.GetString();

                            offerLiteFactory.Title = title;
                            offerLiteFactory.Link = url;

                            // replace width
                            photo = photo!.Replace(";s={width}x{height}", ";=1920x1080");
                            offerLiteFactory.Picture = photo;
                            offerLiteFactory.Price = price;

                            return offerLiteFactory;
                        }
                    }
                    catch (KeyNotFoundException err)
                    {
                        Console.WriteLine(err);
                    }
                }
            }
        }

        throw new ArgumentException();
    }

    }
