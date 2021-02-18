using System;

namespace Masiv.Roulette.API.Utilities
{
    public class GenerateRandom : IGenerateRandom
    {
        public int NextNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}
