using Masiv.Roulette.API.Domain.Dtos;
using System.Collections.Generic;

namespace Masiv.Roulette.API.Contracts
{
    public interface IRouletteService
    {
        RouletteAddResponseDto Add();

        RouletteStartResponseDto Start(RouletteStartDto rouletteStartDto);

        List<RouletteAddResponseDto> GetAll();

        void Bet(string userId, RouletteBetDto rouletteBetDto);
    }
}
