using RickAndMortyBackend.Interfaces;
using RickAndMortyBackend.Models;

namespace RickAndMortyBackend.Services
{
    public class EpisodeService : IDTO // Servicio para manejar la lógica de negocio relacionada con episodios y mapear a DTOs
    {
        private readonly IEpisodeRepository _repository;

        public EpisodeService(IEpisodeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EPISODEDTO>> GetEpisodesAsync()
        {
            var episodes = await _repository.GetAllEpisodesAsync();

            // Mapeo manual
            return episodes.Select(e => new EPISODEDTO
            {
                Id = e.Id,
                Name = e.Name,
                Code = e.EpisodeCode
            }).ToList();
        }
    }
}