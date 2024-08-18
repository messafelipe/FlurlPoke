using FlurlPoke.Application.InputModels;

namespace FlurlPoke.Application.Services.Interfaces
{
    public interface IPokemonService
    {
        Task<APIResourceList> GetLimitedPokemonListAsync(int limit);

        Task<List<Abilities>> GetPokemonAbilitiesAsync(string namePokemon);

        Task<string> DownloadPokemonImageAsync(string namePokemon);

        Task<List<string?>> GetPaginatedPokemonNamesAsync(int offset, int limit);
    }
}
