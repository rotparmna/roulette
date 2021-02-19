//-----------------------------------------------------------------------------
// <copyright file="IRouletteService.cs" company="Roulette API">
//     Copyright © Roulette API All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Masiv.Roulette.API.Contracts
{
    using System.Collections.Generic;
    using Masiv.Roulette.API.Domain.Dtos;

    /// <summary>
    /// The roulette service interface.
    /// </summary>
    public interface IRouletteService
    {
        /// <summary>
        /// Add new roulette.
        /// </summary>
        /// <returns>Object with Id the new roulette.</returns>
        RouletteAddResponseDto Add();

        /// <summary>
        /// Start the roulette.
        /// </summary>
        /// <param name="rouletteStartDto">Object with Id roulette to start.</param>
        /// <returns>Object with information the operation.</returns>
        RouletteStartResponseDto Start(RouletteStartDto rouletteStartDto);

        /// <summary>
        /// Bet to roulette.
        /// </summary>
        /// <param name="userId">Id user that bet.</param>
        /// <param name="rouletteBetDto">Object with information the roulette.</param>
        void Bet(string userId, RouletteBetDto rouletteBetDto);

        /// <summary>
        /// Close the roulette.
        /// </summary>
        /// <param name="rouletteCloseDto">Object with information the roulette.</param>
        /// <returns>Object with information the operation.</returns>
        RouletteCloseResponseDto Close(RouletteCloseDto rouletteCloseDto);

        /// <summary>
        /// Get all roulettes with status.
        /// </summary>
        /// <returns>Object with information.</returns>
        List<RouletteDto> GetAll();
    }
}
