//-----------------------------------------------------------------------------
// <copyright file="GenerateRandom.cs" company="Roulette API">
//     Copyright © Roulette API All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Masiv.Roulette.API.Utilities
{
    using System;

    /// <summary>
    /// Class to generate number random.
    /// </summary>
    public class GenerateRandom : IGenerateRandom
    {
        /// <summary>
        /// Get to number random with range min and max.
        /// </summary>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>Return of number</returns>
        public int NextNumber(int min, int max)
        {
            return new Random().Next(min, max);
        }
    }
}
