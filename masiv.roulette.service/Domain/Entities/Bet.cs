using Masiv.Roulette.API.Domain.Enums;

namespace Masiv.Roulette.API.Domain.Entities
{
    public class Bet
    {
        public int Number { get; set; }
        public ColorEnum Color { get; set; }
        public string UserId { get; set; }
        public double CashAmount { get; set; }
    }
}
