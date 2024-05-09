using Flurl.Http;

namespace FlurlPoke.Infrastructure.Persistence
{
    public sealed class PokeConnection
    {
        private readonly string baseUrl = "https://pokeapi.co/api/v2/";

        public async Task<string> ConnectToApi(string endpoint)
        {
            try
            {
                string apiUrl = $"{baseUrl}{endpoint}";
                return await apiUrl.GetStringAsync();
            }
            catch (FlurlHttpException ex)
            {
                var err = await ex.GetResponseStringAsync(); 
                return err;
            }
        }
    }
}
