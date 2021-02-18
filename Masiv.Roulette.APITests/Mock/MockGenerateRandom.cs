using Masiv.Roulette.API.Utilities;

namespace Masiv.Roulette.APITests.Mock
{
    public class MockGenerateRandom : IGenerateRandom
    {
        public int NextNumber(int min, int max)
        {
            return 10;
        }
    }
}
