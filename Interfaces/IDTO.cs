using RickAndMortyBackend.Models;

namespace RickAndMortyBackend.Interfaces
{
    public interface IDTO // Interfaz para definir métodos de mapeo a DTOs
    {
        Task<List<EPISODEDTO>> GetEpisodesAsync();
    }
}
