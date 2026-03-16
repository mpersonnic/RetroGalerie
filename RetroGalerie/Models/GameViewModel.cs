using RetroGalerie.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetroGalerie.Models
{
    // ViewModel pour un jeu
    public record GameViewModel
    {
        public int Id { get; init; }

        [Required, StringLength(200)]
        public string Title { get; init; } = string.Empty;

        [Range(1970, 2030)]
        public int YearOfPublication { get; init; }

        [StringLength(2000)]
        public string Description { get; init; } = string.Empty;

        [Range(1, 16)]
        public int NumberOfPlayers { get; init; }

        // Champ pour upload
        [NotMapped]
        public IFormFile? CoverImageFile { get; init; }

        // Stockage du chemin une fois sauvegardé
        public string? CoverImageUrl { get; init; }

        public IReadOnlyCollection<Screenshot> Screenshots { get; init; } = Array.Empty<Screenshot>();

        [Required, StringLength(100)]
        public string Genre { get; init; } = string.Empty;

        [Required, StringLength(100)]
        public string Developer { get; init; } = string.Empty;

        [Required, StringLength(100)]
        public string Publisher { get; init; } = string.Empty;

        [Required, StringLength(50)]
        public string Region { get; init; } = string.Empty;

        [Required, StringLength(50)]
        public string Language { get; init; } = string.Empty;

        [Required]
        public bool Owned { get; set; }

        // Clé étrangère vers Console
        public int ConsoleId { get; init; }

        public string ConsoleName { get; init; } = string.Empty;
    }
    
}
