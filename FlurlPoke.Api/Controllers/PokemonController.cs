using Microsoft.AspNetCore.Mvc;

namespace FlurlPoke.Api.Controllers
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
