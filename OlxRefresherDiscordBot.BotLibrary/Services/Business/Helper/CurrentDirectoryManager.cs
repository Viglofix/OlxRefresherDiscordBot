namespace OlxRefresherDiscordBot.BotLibrary.Services.Business.Helper;
    public class CurrentDirectoryManager
    {
        public string? CurrentPath { get; private set; }
        public CurrentDirectoryManager(string pathJson)
        {
            CurrentPath = GetCurrentPath(pathJson);
        }
        private string GetCurrentPath(string pathJson)
        {
            string ConfigFileName = pathJson;
            var baseDirectory = AppContext.BaseDirectory;
            var path = Path.Combine(baseDirectory, ConfigFileName);
            return path;
        }
    }
