using Microsoft.AspNetCore.Mvc;

namespace FlurlPokeApi.Controllers
{
    public class PokemonController : ControllerBase
    {
        public PokemonController() { }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
