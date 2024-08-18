using FlurlPoke.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlurlPoke.Api.Controllers
{
    [Route("api/Pokemon")]
    public class PokemonController(IPokemonService pokemonService) : ControllerBase
    {
        private readonly IPokemonService _pokemonService = pokemonService;

        [HttpGet("getLimitedPokemonListAsync")]
        public async Task<IActionResult> GetLimitedPokemonListAsync(int limit)
        {
            var pokemon = await _pokemonService.GetLimitedPokemonListAsync(limit);

            return Ok(pokemon);
        }

        [HttpGet("getPokemonAbilitiesAsync")]
        public async Task<IActionResult> GetPokemonAbilitiesAsync(string namePokemon)
        {
            var pokemon = await _pokemonService.GetPokemonAbilitiesAsync(namePokemon);

            return Ok(pokemon);
        }

        [HttpGet("downloadPokemonImageAsync")]
        public async Task<IActionResult> DownloadPokemonImageAsync(string namePokemon)
        {
            var pokemon = await _pokemonService.DownloadPokemonImageAsync(namePokemon);

            return Ok(pokemon);
        }

        [HttpGet("getPaginatedPokemonNamesAsync")]
        public async Task<IActionResult> GetPaginatedPokemonNamesAsync(int offset, int limit)
        {
            var pokemon = await _pokemonService.GetPaginatedPokemonNamesAsync(offset, limit);

            return Ok(pokemon);
        }
    }
}
