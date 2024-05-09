using FlurlPoke.Application.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace FlurlPoke.Application.Services.Interfaces
{
    public interface IPokemonService
    {
        NamedAPIResourceList GetAll(int limit);
    }
}
