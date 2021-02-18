using Masiv.Roulette.API.Domain.Enums;
using System.Collections.Generic;

namespace Masiv.Roulette.API.Domain.Dtos
{
    public class RouletteCloseResponseDto
    {
        public RouletteCloseResponseDto()
        {
            Bets = new List<BetCloseDto>();
        }

        public int WinningNumber { get; set; }
        public ColorEnum WinnigColor { get; set; }
        public List<BetCloseDto> Bets { get; set; }
    }
}
