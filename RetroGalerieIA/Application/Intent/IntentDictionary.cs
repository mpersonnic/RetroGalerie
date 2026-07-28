namespace RetroGalerieIA.Application.Intents
{
    public class IntentDictionary
    {
        public List<string> GameNames { get; set; } = new();
        public List<string> Platforms { get; set; } = new();
        public List<string> Genres { get; set; } = new();
        public List<string> Attributes { get; set; } = new();
        public Dictionary<string, List<string>> GameKeywords { get; set; } = new();
    }
}
