using RetroGalerie.Data;
using System.ComponentModel.DataAnnotations;

namespace RetroGalerie.Models
{
    // ViewModel pour une console
    public record ConsoleViewModel
    {
        public int Id { get; init; }

        [Required, StringLength(100)]
        public string Name { get; init; } = string.Empty; // ex: "Super Nintendo"

        [Required, StringLength(100)]
        public string Manufacturer { get; init; } = string.Empty; // ex: "Nintendo"

        [Range(1970, 2030)]
        public int ReleaseYear { get; init; }

        public IFormFile? ImageFile { get; init; }
        public string? ImageUrl { get; set; }

        [StringLength(2000)]
        public string? Description { get; init; }

        public ICollection<Game> Games { get; init; } = Array.Empty<Game>();
    }
}
