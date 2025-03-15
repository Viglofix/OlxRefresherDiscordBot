using System.Text.Json.Serialization;
using System.Text.Json;
using System.Globalization;
using System.Diagnostics;

public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(typeToConvert == typeof(DateTime));
        return DateTime.Parse(reader.GetString() ?? string.Empty);
    }
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

public class LublinCarModel
{
    public List<Data> data { get; set; } // 'data' is a list of Data objects
}

public class Data
{
    public int id { get; set; }
    public string url { get; set; }
    public string title { get; set; }
    public DateTime last_refresh_time { get; set; }
    public DateTime created_time { get; set; }
    public DateTime valid_to_time { get; set; }
    [JsonIgnore]
    public DateTime pushup_time { get; set; }
    public DateTime omnibus_pushup_time { get; set; }
    public string description { get; set; }
    public Promotion promotion { get; set; }
    [JsonPropertyName("params")]
    public List<Param> Params { get; set; } 
    public List<string> key_params { get; set; }
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
    public string key { get; set; }
    public string label { get; set; }
    public int? value { get; set; }
    public string type { get; set; }
    public bool arranged { get; set; }
    public bool budget { get; set; }
    public string currency { get; set; }
    public bool negotiable { get; set; }
    public object converted_value { get; set; }
    public int? previous_value { get; set; }
    public object converted_previous_value { get; set; }
    public object converted_currency { get; set; }
    public string previous_label { get; set; }
}

public class User
{
    public long id { get; set; }
    public DateTime created { get; set; }
    public bool other_ads_enabled { get; set; }
    public string name { get; set; }
    public object logo { get; set; }
    public object logo_ad_page { get; set; }
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
    public double lat { get; set; }
    public double lon { get; set; }
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
    public int id { get; set; }
    public string name { get; set; }
    public string normalized_name { get; set; }
}

public class Region
{
    public int id { get; set; }
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
    public int id { get; set; }
    public string type { get; set; }
}

public class Delivery
{
    public Rock rock { get; set; }
}

public class Rock
{
    public object offer_id { get; set; }
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