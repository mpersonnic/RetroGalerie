namespace RetroGalerie.Data
{
    /// <summary>
    /// Classe correspondant à un jeu vidéo
    /// [1 Game -> 1 Console]
    /// [N Games <-> N Gamers via GameGamer]
    /// [1 Game -> N Screenshots]
    /// </summary>
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int YearOfPublication { get; set; }
        public string Description { get; set; } = string.Empty;
        public int NumberOfPlayers { get; set; }

        public string CoverImageUrl { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;

        // Relation vers Console
        public int ConsoleId { get; set; }
        public Console Console { get; set; } = null!;

        // Relation vers les images associées au jeu :
        public ICollection<Screenshot>? Screenshots { get; set; }
    }
}
