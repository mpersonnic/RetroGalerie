namespace RetroGalerie.Data
{
    /// <summary>
    /// [1 Console -> N Games]
    /// </summary>
    public class Console
    {
        public int Id { get; set; }
        public required string Name { get; set; }        // ex: "Super Nintendo"
        public string Manufacturer { get; set; } = string.Empty; // ex: "Nintendo"
        public int ReleaseYear { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Navigation vers les jeux liés
        public ICollection<Game>? Games { get; set; }
    }

}
