using Masiv.Roulette.API.Domain.Enums;

namespace Masiv.Roulette.API.Domain.Entities
{
    public class Roulette
    {
        public Roulette()
        {
            this.Status = StatusEnum.None;
        }

        public string Id { get; set; }
        public StatusEnum Status { get; set; }
    }
}
