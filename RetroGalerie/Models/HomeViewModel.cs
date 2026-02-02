namespace RetroGalerie.Models
{
    // ViewModel pour la page d'accueil
    public record HomeViewModel
    {
        public IReadOnlyCollection<ConsoleGroupViewModel> Consoles { get; init; } = Array.Empty<ConsoleGroupViewModel>();
    }
}
