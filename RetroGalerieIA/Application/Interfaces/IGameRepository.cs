using RetroGalerie.Data;

namespace RetroGalerieIA.Application.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync();

        Task<IEnumerable<Game>> SearchAsync(string query);

        Task<IEnumerable<Game>> SearchAsync(string subject, IEnumerable<string> filters);
    }
}
