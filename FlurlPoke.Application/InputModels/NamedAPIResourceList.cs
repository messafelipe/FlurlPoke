namespace FlurlPoke.Application.InputModels
{
    public class NamedAPIResourceList
    {
        public int Count { get; set; }
        public string? Next { get; set; }
        public string? Previous { get; set; }
        public List<NamedAPIResource> Results { get; set; } = new List<NamedAPIResource>();
    }
}
