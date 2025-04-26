namespace OlxRefresherDiscordBot.BotLibrary.Services.DataAccess.Model;
    public interface IOfferModel
    {
        public string? Title { get; set; }
        public string? Link { get; set; }
        public decimal? Price { get; set; }
        public string? Picture { get; set; }
    };
