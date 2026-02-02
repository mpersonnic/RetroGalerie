using System.ComponentModel.DataAnnotations;

namespace RetroGalerie.Models
{
    // Groupement de jeux par console
    public record ConsoleGroupViewModel
    {
        [Required]
        public string ConsoleName { get; init; } = string.Empty;

        public IReadOnlyCollection<GameViewModel> Games { get; init; } = Array.Empty<GameViewModel>();
        public int GameCount => Games?.Count ?? 0; // Nombre de jeux dans le groupe
    }
}
