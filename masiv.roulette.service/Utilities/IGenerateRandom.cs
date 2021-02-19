//-----------------------------------------------------------------------------
// <copyright file="IGenerateRandom.cs" company="Roulette API">
//     Copyright © Roulette API All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Masiv.Roulette.API.Utilities
{
    /// <summary>
    /// Interface to generate number random.
    /// </summary>
    public interface IGenerateRandom
    {
        /// <summary>
        /// Get to number random with range min and max.
        /// </summary>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>Return of number</returns>
        int NextNumber(int min, int max);
    }
}