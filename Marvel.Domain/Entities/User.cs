using System;

namespace Marvel.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public ICollection<PokemonFavorite> Favorites { get; private set; } = new List<PokemonFavorite>();
    }
}
