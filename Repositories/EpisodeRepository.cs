using RickAndMortyBackend.Interfaces;
using RickAndMortyBackend.Models;
using System.Text.Json;

namespace RickAndMortyBackend.Repositories
{
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly HttpClient _httpClient;

        // IHttpClientFactory 
        public EpisodeRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("RickAndMortyClient");
        }

        public async Task<List<EPISODE>> GetAllEpisodesAsync()
        {
            var allEpisodes = new List<EPISODE>();
            string? nextUrl = "episode"; // Endpoint inicial (relativo)

            // BUCLE
            while (!string.IsNullOrEmpty(nextUrl))
            {
                var response = await _httpClient.GetAsync(nextUrl);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<RickAndMortyResponse>(content);

                if (data?.Results != null)
                {
                    // Convertimos el modelo externo a nuestra Entidad de Dominio
                    var domainEpisodes = data.Results.Select(r => new EPISODE
                    {
                        Id = r.Id,
                        Name = r.Name,
                        EpisodeCode = r.Episode,
                        AirDate = r.AirDate
                    });

                    allEpisodes.AddRange(domainEpisodes);
                }


                nextUrl = data?.Info?.Next;
            }

            return allEpisodes;
        }
    }
}