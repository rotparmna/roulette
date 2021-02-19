//-----------------------------------------------------------------------------
// <copyright file="BetCloseDto.cs" company="Roulette API">
//     Copyright © Roulette API All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Masiv.Roulette.API.Domain.Dtos
{
    using Masiv.Roulette.API.Domain.Enums;

    public class BetCloseDto
    {
        public int Number { get; set; }
        public ColorEnum Color { get; set; }
        public string UserId { get; set; }
        public double CashAmount { get; set; }
        public bool? IsWin { get; set; }
        public double WinnerAmount { get; set; }
    }
}
