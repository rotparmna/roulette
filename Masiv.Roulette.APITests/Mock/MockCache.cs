using Masiv.Roulette.API.Middleware.Cache;
using System.Collections.Generic;

namespace Masiv.Roulette.APITests.Mock
{
    class MockCache : ICacheMiddleware<List<API.Domain.Entities.Roulette>>
    {
        public List<API.Domain.Entities.Roulette> roulettes;

        public MockCache()
        {
            roulettes = new List<API.Domain.Entities.Roulette>();
        }

        public List<API.Domain.Entities.Roulette> GetValue(string id)
        {
            return roulettes;
        }

        public void SetValue(string id, List<API.Domain.Entities.Roulette> value)
        {
            roulettes = value;
        }
    }
}
