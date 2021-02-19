//-----------------------------------------------------------------------------
// <copyright file="RouletteCloseResponseDto.cs" company="Roulette API">
//     Copyright © Roulette API All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Masiv.Roulette.API.Domain.Dtos
{
    using System.Collections.Generic;
    using Masiv.Roulette.API.Domain.Enums;

    public class RouletteCloseResponseDto
    {
        public RouletteCloseResponseDto()
        {
            this.Bets = new List<BetCloseDto>();
        }

        public int WinningNumber { get; set; }
        public ColorEnum WinnigColor { get; set; }
        public List<BetCloseDto> Bets { get; set; }
    }
}
