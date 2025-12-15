using Marvel.Application.DTOs.Marvel;
using Marvel.Application.Interfaces;
using System.Net;
using System.Text.Json;

namespace Marvel.Infrastructure.Services
{
    public class PokeApiService : IPokeApiService
    {
        private readonly HttpClient _httpClient;

        public PokeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
        }

        public async Task<PokemonListResponseDto> GetPokemonsAsync(int offset, int limit)
        {
            var response = await _httpClient.GetAsync($"pokemon?offset={offset}&limit={limit}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PokemonListResponseDto>(
                       content,
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                   ) ?? new PokemonListResponseDto();
        }

        public async Task<PokemonDetailDto?> GetPokemonByIdOrNameAsync(string idOrName)
        {
            var response = await _httpClient.GetAsync($"pokemon/{idOrName}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Pokémon no encontrado");

            using var stream = await response.Content.ReadAsStreamAsync();
            using var document = await JsonDocument.ParseAsync(stream);
            var root = document.RootElement;

            return new PokemonDetailDto
            {
                Id = root.GetProperty("id").GetInt32(),
                Name = root.GetProperty("name").GetString()!,
                Height = root.GetProperty("height").GetInt32(),
                Weight = root.GetProperty("weight").GetInt32(),

                Types = root.GetProperty("types")
                    .EnumerateArray()
                    .Select(t => new PokemonTypeDto
                    {
                        Name = t.GetProperty("type").GetProperty("name").GetString()!
                    })
                    .ToList(),

                Abilities = root.GetProperty("abilities")
                    .EnumerateArray()
                    .Select(a => new PokemonAbilityDto
                    {
                        Name = a.GetProperty("ability").GetProperty("name").GetString()!
                    })
                    .ToList(),

                Sprite = root.GetProperty("sprites")
                    .GetProperty("front_default")
                    .GetString()
            };
        }
    }
}
