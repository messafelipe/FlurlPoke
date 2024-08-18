namespace FlurlPoke.Infrastructure.Persistence
{
    public sealed class PokeConnection
    {
        private readonly string baseUrl = "https://pokeapi.co/api/v2/";

        public string ConnectToApi()
        {
            return $"{baseUrl}";
        }
    }
}
