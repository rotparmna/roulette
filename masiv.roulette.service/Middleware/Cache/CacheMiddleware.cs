//-----------------------------------------------------------------------------
// <copyright file="CacheMiddleware.cs" company="Roulette API">
//     Copyright © Roulette API All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Masiv.Roulette.API.Middleware.Cache
{
    using System;
    using Microsoft.Extensions.Caching.Distributed;
    using Newtonsoft.Json;

    /// <summary>
    /// The middleware for cache.
    /// </summary>
    /// <typeparam name="T">Entity to cache.</typeparam>
    public class CacheMiddleware<T> : ICacheMiddleware<T>
    {
        /// <summary>
        /// The distributed cache.
        /// </summary>
        private readonly IDistributedCache distributedCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheMiddleware<>"/> class.
        /// </summary>
        /// <param name="distributedCache">The distributed cache.</param>
        public CacheMiddleware(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        /// <summary>
        /// Get value from cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Object with data.</returns>
        public T GetValue(string key)
        {
            string cache = null;
            try
            {
                cache = this.distributedCache.GetString(key);
            }
            catch (Exception ex)
            {
                Console.Write("Error with cache " + ex.Message);
            }
            
            var data = (T)Activator.CreateInstance(typeof(T));
            if (cache != null)
                data = JsonConvert.DeserializeObject<T>(cache);

            return data;
        }

        /// <summary>
        /// Set value to cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The entity to cache.</param>
        public void SetValue(string key, T value)
        {
            try
            {
                this.distributedCache.SetString(key, JsonConvert.SerializeObject(value));
            }
            catch (Exception ex)
            {
                Console.Write("Error with cache " + ex.Message);
            }
        }
    }
}
