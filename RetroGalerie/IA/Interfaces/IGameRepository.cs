using RetroGalerie.Data;

namespace RetroGalerie.IA.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllAsync();

        Task<IEnumerable<Game>> SearchAsync(string query);

        Task<IEnumerable<Game>> SearchAsync(string subject, IEnumerable<string> filters);
    }
}
