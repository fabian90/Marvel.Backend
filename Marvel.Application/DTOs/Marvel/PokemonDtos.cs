   namespace Marvel.Application.DTOs.Marvel
    {
        public class PokemonSummaryDto
        {
            public string Name { get; set; } = string.Empty;
            public string Url { get; set; } = string.Empty;
        }

        public class PokemonListResponseDto
        {
            public int Count { get; set; }
            public string? Next { get; set; }
            public string? Previous { get; set; }
            public List<PokemonSummaryDto> Results { get; set; } = new();
        }
    public class PokemonDetailDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public int Height { get; set; }
            public int Weight { get; set; }

            public List<PokemonTypeDto> Types { get; set; } = new();
            public List<PokemonAbilityDto> Abilities { get; set; } = new();

            public string? Sprite { get; set; }
        }

        public class PokemonTypeDto
        {
            public string Name { get; set; } = string.Empty;
        }

        public class PokemonAbilityDto
        {
            public string Name { get; set; } = string.Empty;
        }
    }

