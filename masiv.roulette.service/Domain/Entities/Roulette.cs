//-----------------------------------------------------------------------------
// <copyright file="Roulette.cs" company="Roulette API">
//     Copyright © Roulette API All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Masiv.Roulette.API.Domain.Entities
{
    using System.Collections.Generic;
    using Masiv.Roulette.API.Domain.Enums;

    public class Roulette
    {
        public Roulette()
        {
            this.Status = StatusEnum.None;
            this.Bets = new List<Bet>();
        }

        public string Id { get; set; }
        public StatusEnum Status { get; set; }
        public List<Bet> Bets { get; set; }
    }
}
