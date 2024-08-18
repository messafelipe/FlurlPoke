namespace FlurlPoke.Application.InputModels
{
    public class APIResourceList
    {
        public int Count { get; set; }
        public string? Next { get; set; }
        public string? Previous { get; set; }
        public List<APIResource> Results { get; set; } = [];
    }
}
