namespace RetroGalerie.Data
{
    /// <summary>
    /// [1 Console -> N Games]
    /// </summary>
    public class Console
    {
        public int Id { get; set; }
        public string Name { get; set; }        // ex: "Super Nintendo"
        public string Manufacturer { get; set; } // ex: "Nintendo"
        public int ReleaseYear { get; set; }
        public string ImageUrl { get; set; }

        public string? Description { get; set; }

        // Navigation vers les jeux liés
        public ICollection<Game>? Games { get; set; }
    }

}
