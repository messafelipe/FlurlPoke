using FlurlPoke.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlurlPoke.Api.Controllers
{
    [Route("api/Pokemon")]
    public class PokemonController : ControllerBase
    {

        private readonly IPokemonService _pokemonService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pokemonService"></param>
        public PokemonController(IPokemonService pokemonService) {
        
            _pokemonService = pokemonService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll(int limit)
        {
            var pokemon = _pokemonService.GetAll(limit);
            return Ok(pokemon);
        }
    }
}
