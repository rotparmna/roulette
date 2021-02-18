using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;

namespace Masiv.Roulette.API.Middleware.Cache
{
    public class CacheMiddleware<T> : ICacheMiddleware<T>
    {
        private readonly IDistributedCache distributedCache;

        public CacheMiddleware(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }
        public T GetValue(string id)
        {
            var cache = this.distributedCache.GetString(id);
            var data = (T)Activator.CreateInstance(typeof(T));
            if (cache != null)
                data = JsonConvert.DeserializeObject<T>(cache);

            return data;
        }

        public void SetValue(string id, T value)
        {
            this.distributedCache.SetString(id, JsonConvert.SerializeObject(value));
        }
    }
}
