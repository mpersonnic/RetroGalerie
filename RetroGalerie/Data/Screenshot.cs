namespace RetroGalerie.Data
{
    /// <summary>
    /// [N Screenshots -> 1 Game]
    /// </summary>
    public class Screenshot
    {
        public int Id { get; set; }
        public string FilePath { get; set; } // chemin relatif ou absolu

        public int? GameId { get; set; }
        public Game? Game { get; set; }
    }

}
