using RickAndMortyBackend.Models;

namespace RickAndMortyBackend.Interfaces
{
    public interface IEpisodeRepository // Repositorio para acceder a datos de episodios
    {
        Task<List<EPISODE>> GetAllEpisodesAsync();
    }
}
