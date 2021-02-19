//-----------------------------------------------------------------------------
// <copyright file="Bet.cs" company="Roulette API">
//     Copyright © Roulette API All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Masiv.Roulette.API.Domain.Entities
{
    using Masiv.Roulette.API.Domain.Enums;

    public class Bet
    {
        public int Number { get; set; }
        public ColorEnum Color { get; set; }
        public string UserId { get; set; }
        public double CashAmount { get; set; }
        public double? WinnigCash { get; set; }
        public bool? IsWin { get; set; }
    }
}
