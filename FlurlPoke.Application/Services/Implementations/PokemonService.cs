using Flurl;
using Flurl.Http;
using FlurlPoke.Application.InputModels;
using FlurlPoke.Application.Services.Interfaces;
using FlurlPoke.Infrastructure.Persistence;
using Newtonsoft.Json.Linq;

namespace FlurlPoke.Application.Services.Implementations
{
    public class PokemonService(PokeConnection pokeConnection) : IPokemonService
    {
        private readonly PokeConnection _pokeConnection = pokeConnection;

        /// <summary>
        /// Recupera uma lista de Pokémon com um limite especificado no número de resultados.
        /// </summary>
        /// <param name="limit">O número máximo de Pokémon a ser recuperado</param>
        /// <returns>Uma lista de Pokémon até o limite especificado</returns>
        public async Task<APIResourceList> GetLimitedPokemonListAsync(int limit)
        {
            var connection = _pokeConnection.ConnectToApi().AppendPathSegment("pokemon");

            var pokemon = await connection
                                .SetQueryParams(new { limit })
                                .GetJsonAsync<APIResourceList>();

            return pokemon;
        }


        /// <summary>
        /// Retorna uma lista de habilidades para um Pokémon específico pelo nome.
        /// </summary>
        /// <param name="namePokemon">O nome do Pokémon</param>
        /// <returns>Uma lista de habilidades do Pokémon especificado.</returns>
        public async Task<List<Abilities>> GetPokemonAbilitiesAsync(string namePokemon)
        {
            var result = await _pokeConnection.ConnectToApi()
                                              .AppendPathSegment("pokemon")
                                              .AppendPathSegment($"{namePokemon.ToLower()}")
                                              .GetJsonAsync<PokemonModel>();

            var abilities = result.Abilities;

            return abilities ?? [];
        }

        /// <summary>
        /// Faz o download da imagem frontal de um Pokémon especificado e a 
        /// salva na pasta de imagens no desktop.
        /// </summary>
        /// <param name="namePokemon">O nome do Pokémon cuja imagem deve ser baixada</param>
        /// <returns>Uma mensagem indicando o sucesso do download e o caminho onde a imagem foi salva</returns>
        public async Task<string> DownloadPokemonImageAsync(string namePokemon)
        {
            var result = await _pokeConnection.ConnectToApi()
                                              .AppendPathSegment("pokemon")
                                              .AppendPathSegment($"{namePokemon.ToLower()}")
                                              .GetStringAsync();

            var linkImage = JObject.Parse(result)["sprites"]?["front_default"]?.ToString();

            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var folderPath = Path.Combine(desktop, "img_pokemon", $"{namePokemon}");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            await linkImage.DownloadFileAsync(folderPath);

            return $"Imagem de {namePokemon} foi salva com sucesso!\nCaminho: {folderPath}";
        }

        /// <summary>
        /// Recupera uma lista paginada de nomes de Pokémon com base no deslocamento e no limite especificados.
        /// </summary>
        /// <param name="offset">O número de itens a serem ignorados antes de começar a coletar os nomes dos Pokémon</param>
        /// <param name="limit">O número máximo de nomes de Pokémon a ser recuperado</param>
        /// <returns>Uma lista de nomes de Pokémon dentro dos parâmetros especificados</returns>
        public async Task<List<string?>> GetPaginatedPokemonNamesAsync(int offset, int limit)
        {
            var r = await _pokeConnection.ConnectToApi()
                                         .AppendPathSegment("pokemon")
                                         .SetQueryParams(new { offset, limit })
                                         .GetJsonAsync<APIResourceList>();

            var pokemonList = r.Results as IEnumerable<APIResource>;
            var pokemonNames = pokemonList.OrderBy(o => o.Name).Select(p => p.Name).ToList();

            return pokemonNames;
        }

        /// <summary>
        /// Recupera as localizações onde um Pokémon específico pode ser encontrado.
        /// </summary>
        /// <param name="namePokemon">O nome do Pokémon</param>
        /// <returns>Uma lista de localizações onde o Pokémon pode ser encontrado.</returns>
        public async Task<List<string?>?> GetPokemonLocationsAsync(string namePokemon)
        {
            var result = await _pokeConnection.ConnectToApi()
                                              .AppendPathSegment("pokemon")
                                              .AppendPathSegment($"{namePokemon.ToLower()}")
                                              .AppendPathSegment("encounters")
                                              .GetStringAsync();

            var encounterData = JArray.Parse(result);

            var locations = encounterData
                .Select(location => location["location_area"]?["name"]?.ToString())
                .Where(name => !string.IsNullOrEmpty(name))
                .Distinct()
                .ToList();

            return locations ?? [];
        }
    }
}