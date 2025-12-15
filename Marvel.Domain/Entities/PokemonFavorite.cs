namespace Marvel.Domain.Entities
{
    /// <summary>
    /// Representa un Pokémon marcado como favorito por un usuario.
    /// </summary>
    public class PokemonFavorite
    {
        public Guid UserId { get; private set; }
        public string PokemonId { get; private set; } = string.Empty;

        protected PokemonFavorite() { } // Requerido por EF Core

        public PokemonFavorite(Guid userId, string pokemonId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId no puede estar vacío.", nameof(userId));

            if (string.IsNullOrWhiteSpace(pokemonId))
                throw new ArgumentException("PokemonId no puede estar vacío.", nameof(pokemonId));

            UserId = userId;
            PokemonId = pokemonId;
        }
    }
}