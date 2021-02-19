//-----------------------------------------------------------------------------
// <copyright file="ICacheMiddleware.cs" company="Roulette API">
//     Copyright © Roulette API All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Masiv.Roulette.API.Middleware.Cache
{
    /// <summary>
    /// The middleware for cache to interface.
    /// </summary>
    /// <typeparam name="T">Entity to cache.</typeparam>
    public interface ICacheMiddleware<T>
    {
        /// <summary>
        /// Get value from cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Object with data.</returns>
        T GetValue(string key);

        /// <summary>
        /// Set value to cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The entity to cache.</param>
        void SetValue(string key, T value);
    }
}