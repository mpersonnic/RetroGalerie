using RetroGalerie.Data;

namespace RetroGalerieIA.Application.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> SearchAsync(string query);
    }
}
