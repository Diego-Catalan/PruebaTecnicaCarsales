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
            string? nextUrl = "episode";

            while (!string.IsNullOrEmpty(nextUrl))
            {
                var response = await _httpClient.GetAsync(nextUrl);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var data = JsonSerializer.Deserialize<RickAndMortyResponse>(content, options);

                if (data?.Results != null)
                {
                    var domainEpisodes = data.Results.Select(r => new EPISODE
                    {
                        Id = r.Id,
                        Name = r.Name,
                        EpisodeCode = r.Episode,
                        AirDate = r.AirDate,
                        Characters = r.Characters ?? new List<string>() 
                    });
                    allEpisodes.AddRange(domainEpisodes);
                }
                nextUrl = data?.Info?.Next;
            }
            var allCharacterIds = allEpisodes
                .SelectMany(e => e.Characters)
                .Select(url => url.Split('/').Last()) 
                .Distinct() // Eliminamos duplicados (para no pedir a Rick 50 veces)
                .ToList();

            if (allCharacterIds.Any())
            {
                string idsString = string.Join(",", allCharacterIds);

                var charResponse = await _httpClient.GetAsync($"character/{idsString}");

                if (charResponse.IsSuccessStatusCode)
                {
                    var charContent = await charResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var charactersDto = JsonSerializer.Deserialize<List<Character>>(charContent, options);
                    if (charactersDto != null)
                    {
                        var namesMap = charactersDto.ToDictionary(k => k.Url, v => v.Name);

                        foreach (var episode in allEpisodes)
                        {
                            episode.CharacterNames = episode.Characters
                                .Where(url => namesMap.ContainsKey(url))
                                .Select(url => namesMap[url])
                                .ToList();
                        }
                    }
                }
            }

            return allEpisodes;
        }
    }
}