using HotChocolate;
using System.Linq;
using System.Threading.Tasks;

namespace hotchocolate_fullstack_demo
{
    public class Mutation
    {
        /// <summary>
        /// Adds a new pokemon.
        /// </summary>
        /// <returns>A newly created pokemon.</returns>
        public async Task<Pokemon> Pokemon([Service] PokemonContext pokemonContext, string name, string type)
        {
            var pokemon = new Pokemon()
            {
                id = pokemonContext.Pokemon.Select(p => p.id).Max() + 1,
                name = name,
                type = type
            };

            pokemonContext.Pokemon.Add(pokemon);

            await pokemonContext.SaveChangesAsync();

            return pokemon;
        }

        /// <summary>
        /// Deletes the specified pokemon.
        /// </summary>
        /// <returns>The deleted Pokemon</returns>
        public async Task<Pokemon> RemovePokemon([Service] PokemonContext pokemonContext, int id)
        {
            var pokemon = pokemonContext.Pokemon.Find(id);
            pokemonContext.Pokemon.Remove(pokemon);

            await pokemonContext.SaveChangesAsync();

            return pokemon;
        }

        /// <summary>
        /// Updates the pokemon with the specified id
        /// </summary>
        /// <param name="pokemonContext">The pokemon DB context.</param>
        /// <returns>All pokemon from the context.</returns>
        public async Task<Pokemon> UpdatePokemon([Service]PokemonContext pokemonContext, int id, string name, string type)
        {
            var pokemon = pokemonContext.Pokemon.Find(id);
            pokemon.name = name;
            pokemon.type = type;

            await pokemonContext.SaveChangesAsync();

            pokemon = pokemonContext.Pokemon.Find(id);//retrieve the newly created pokemon
            return pokemon;
        }
    }
}