namespace RetroGalerie.IA.Intent
{
    public class Intent
    {
        public string Action { get; set; } = "";
        public string Subject { get; set; } = "";
        public List<string> Filters { get; set; } = new();
    }
}
