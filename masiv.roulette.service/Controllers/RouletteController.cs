//-----------------------------------------------------------------------------
// <copyright file="RouletteController.cs" company="Roulette API">
//     Copyright © Roulette API All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Masiv.Roulette.API.Controllers
{
    using System.Collections.Generic;
    using Masiv.Roulette.API.Contracts;
    using Masiv.Roulette.API.Domain.Dtos;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Roulette Controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RouletteController : ControllerBase
    {
        /// <summary>
        /// The roulette service.
        /// </summary>
        private readonly IRouletteService rouletteService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouletteController"/> class.
        /// </summary>
        /// <param name="rouletteService">The roulette service.</param>
        public RouletteController(IRouletteService rouletteService)
        {
            this.rouletteService = rouletteService;
        }

        /// <summary>
        /// Add new roulette.
        /// </summary>
        /// <returns>Object with Id the new roulette.</returns>
        [HttpPost("add")]
        [ProducesResponseType(typeof(RouletteAddResponseDto), StatusCodes.Status200OK)]
        public RouletteAddResponseDto Add()
        {
            return this.rouletteService.Add();
        }

        /// <summary>
        /// Start the roulette.
        /// </summary>
        /// <param name="rouletteStartDto">Object with Id roulette to start.</param>
        /// <returns>Object with information the operation.</returns>
        [HttpPost("start")]
        [ProducesResponseType(typeof(RouletteStartResponseDto), StatusCodes.Status200OK)]
        public RouletteStartResponseDto Start(RouletteStartDto rouletteStartDto)
        {
            return this.rouletteService.Start(rouletteStartDto);
        }

        /// <summary>
        /// Bet to roulette.
        /// </summary>
        /// <param name="userId">Id user that bet.</param>
        /// <param name="rouletteBetDto">Object with information the roulette.</param>
        [HttpPost("bet")]
        public void Bet([FromHeader(Name = "user-id")] string userId, RouletteBetDto rouletteBetDto)
        {
            this.rouletteService.Bet(userId, rouletteBetDto);
        }

        /// <summary>
        /// Close the roulette.
        /// </summary>
        /// <param name="rouletteCloseDto">Object with information the roulette.</param>
        /// <returns>Object with information the operation.</returns>
        [HttpPost("close")]
        [ProducesResponseType(typeof(RouletteCloseResponseDto), StatusCodes.Status200OK)]
        public RouletteCloseResponseDto Close(RouletteCloseDto rouletteCloseDto)
        {
            return this.rouletteService.Close(rouletteCloseDto);
        }

        /// <summary>
        /// Get all roulettes with status.
        /// </summary>
        /// <returns>Object with information.</returns>
        [HttpGet("get_all")]
        [ProducesResponseType(typeof(List<RouletteDto>), StatusCodes.Status200OK)]
        public List<RouletteDto> GetAll()
        {
            return this.rouletteService.GetAll();
        }
    }
}
