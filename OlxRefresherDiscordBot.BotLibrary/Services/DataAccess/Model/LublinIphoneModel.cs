using System.Text.Json.Serialization;

namespace OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Model;

public class OfferData
{
    public List<Offer> data { get; set; }
}

public class Offer
{
    public long id { get; set; }
    public string url { get; set; }
    public string title { get; set; }
    public DateTime last_refresh_time { get; set; }
    public DateTime created_time { get; set; }
    public DateTime valid_to_time { get; set; }
    public DateTime? pushup_time { get; set; }
    public string description { get; set; }
    public Promotion promotion { get; set; }
    [JsonPropertyName("params")]
    public List<Param> Params { get; set; }
    public List<object> key_params { get; set; }
    public bool business { get; set; }
    public User user { get; set; }
    public string status { get; set; }
    public Contact contact { get; set; }
    public Map map { get; set; }
    public Location location { get; set; }
    public List<Photo> photos { get; set; }
    public object partner { get; set; }
    public Category category { get; set; }
    public Delivery delivery { get; set; }
    public Safedeal safedeal { get; set; }
    public Shop shop { get; set; }
    public string offer_type { get; set; }
}

public class Promotion
{
    public bool highlighted { get; set; }
    public bool urgent { get; set; }
    public bool top_ad { get; set; }
    public List<string> options { get; set; }
    public bool b2c_ad_page { get; set; }
    public bool premium_ad_page { get; set; }
}

public class Param
{
    public string key { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public Value value { get; set; }
}

public class Value
{
    public decimal value { get; set; }
    public string type { get; set; }
    public bool arranged { get; set; }
    public bool budget { get; set; }
    public string currency { get; set; }
    public bool negotiable { get; set; }
    public object converted_value { get; set; }
    public object previous_value { get; set; }
    public object converted_previous_value { get; set; }
    public object converted_currency { get; set; }
    public string label { get; set; }
    public object previous_label { get; set; }
}

public class User
{
    public long id { get; set; }
    public DateTime created { get; set; }
    public bool other_ads_enabled { get; set; }
    public string name { get; set; }
    public object logo { get; set; }
    public string logo_ad_page { get; set; }
    public object social_network_account_type { get; set; }
    public object photo { get; set; }
    public string banner_mobile { get; set; }
    public string banner_desktop { get; set; }
    public string company_name { get; set; }
    public string about { get; set; }
    public bool b2c_business_page { get; set; }
    public bool is_online { get; set; }
    public DateTime last_seen { get; set; }
    public object seller_type { get; set; }
    public string uuid { get; set; }
}

public class Contact
{
    public string name { get; set; }
    public bool phone { get; set; }
    public bool chat { get; set; }
    public bool negotiation { get; set; }
    public bool courier { get; set; }
}

public class Map
{
    public int zoom { get; set; }
    public decimal lat { get; set; }
    public decimal lon { get; set; }
    public int radius { get; set; }
    public bool show_detailed { get; set; }
}

public class Location
{
    public City city { get; set; }
    public Region region { get; set; }
}

public class City
{
    public long id { get; set; }
    public string name { get; set; }
    public string normalized_name { get; set; }
}

public class Region
{
    public long id { get; set; }
    public string name { get; set; }
    public string normalized_name { get; set; }
}

public class Photo
{
    public long id { get; set; }
    public string filename { get; set; }
    public int rotation { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public string link { get; set; }
}

public class Category
{
    public long id { get; set; }
    public string type { get; set; }
}

public class Delivery
{
    public Rock rock { get; set; }
}

public class Rock
{
    public string offer_id { get; set; }
    public bool active { get; set; }
    public string mode { get; set; }
}

public class Safedeal
{
    public int weight { get; set; }
    public int weight_grams { get; set; }
    public string status { get; set; }
    public bool safedeal_blocked { get; set; }
    public List<object> allowed_quantity { get; set; }
}

public class Shop
{
    public object subdomain { get; set; }
}
