using Flurl.Http;
using FlurlPoke.Application.InputModels;
using FlurlPoke.Application.Services.Interfaces;
using FlurlPoke.Infrastructure.Persistence;

namespace FlurlPoke.Application.Services.Implementations
{
    public class PokemonService : IPokemonService
    {
        private readonly PokeConnection _pokeConnection;

        public PokemonService(PokeConnection pokeConnection)
        {
            _pokeConnection = pokeConnection;
        }

        public NamedAPIResourceList GetAll(int limit)
        {
            //var jsonData = _pokeConnection.ConnectToApi("pokemon").Result;

            var pokemonUrl = "https://pokeapi.co/api/v2/pokemon";

            var responseWithParams = pokemonUrl
                .GetJsonAsync<NamedAPIResourceList>().Result;

            return responseWithParams;
        }
    }
}
