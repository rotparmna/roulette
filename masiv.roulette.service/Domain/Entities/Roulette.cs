using Masiv.Roulette.API.Domain.Enums;
using System.Collections.Generic;

namespace Masiv.Roulette.API.Domain.Entities
{
    public class Roulette
    {
        public Roulette()
        {
            Status = StatusEnum.None;
            Bets = new List<Bet>();
        }

        public string Id { get; set; }
        public StatusEnum Status { get; set; }
        public List<Bet> Bets { get; set; }
    }
}
