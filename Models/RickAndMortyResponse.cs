using System.Text.Json.Serialization;

namespace RickAndMortyBackend.Models
{
    public class RickAndMortyResponse //Modelo para deserializar la respuesta de la API externa
    {
        [JsonPropertyName("info")]
        public Info Info { get; set; }

        [JsonPropertyName("results")]
        public List<EpisodeResult> Results { get; set; }
    }

    public class Info
    {
        [JsonPropertyName("next")]
        public string? Next { get; set; }
    }

    public class EpisodeResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("characters")]
        public List<string> Characters { get; set; }
        [JsonPropertyName("episode")]
        public string Episode { get; set; }
        [JsonPropertyName("air_date")]
        public string AirDate { get; set; }
    }
    public class Character
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}