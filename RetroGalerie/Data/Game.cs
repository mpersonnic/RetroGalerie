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
        public string Title { get; set; }
        public int YearOfPublication { get; set; }
        public string Description { get; set; }
        public int NumberOfPlayers { get; set; }

        public string CoverImageUrl { get; set; }
        public string Genre { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Region { get; set; }
        public string Language { get; set; }

        // Relation vers Console
        public int ConsoleId { get; set; }
        public Console Console { get; set; }

        // Relation vers les images associées au jeu :
        public ICollection<Screenshot>? Screenshots { get; set; }
    }


}
