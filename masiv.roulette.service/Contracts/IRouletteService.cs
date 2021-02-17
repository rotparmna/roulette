using Masiv.Roulette.API.Domain.Dtos;
using System.Collections.Generic;

namespace Masiv.Roulette.API.Contracts
{
    public interface IRouletteService
    {
        RouletteAddDto Add();

        List<RouletteAddDto> GetAll();
    }
}
