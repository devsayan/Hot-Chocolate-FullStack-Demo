using HotChocolate;
using System.Linq;

/// <summary>
/// Provides a resolver method for getting pokemon.
/// </summary>
public class Query
{
    /// <summary>
    /// Gets all pokemon.
    /// </summary>
    /// <param name="pokemonContext">The pokemon DB context.</param>
    /// <returns>All pokemon from the context.</returns>
    public IQueryable<Pokemon> GetPokemon([Service]PokemonContext pokemonContext)
        => pokemonContext.Pokemon;

    /// <summary>
    /// Gets the pokemon with the specified id
    /// </summary>
    /// <param name="pokemonContext">The pokemon DB context.</param>
    /// <returns>All pokemon from the context.</returns>
    public IQueryable<Pokemon> GetPokemonFromId([Service]PokemonContext pokemonContext, int id)
    {
        return pokemonContext.Pokemon.Where(p => p.id == id);
    }
}