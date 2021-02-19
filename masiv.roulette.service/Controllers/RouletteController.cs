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

    [ApiController]
    [Route("[controller]")]
    public class RouletteController : ControllerBase
    {
        private readonly IRouletteService rouletteService;

        public RouletteController(IRouletteService rouletteService)
        {
            this.rouletteService = rouletteService;
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(RouletteAddResponseDto), StatusCodes.Status200OK)]
        public RouletteAddResponseDto AddRoulette()
        {
            return this.rouletteService.AddRoulette();
        }

        [HttpPost("start")]
        [ProducesResponseType(typeof(RouletteStartResponseDto), StatusCodes.Status200OK)]
        public RouletteStartResponseDto StartRoulette(RouletteStartDto rouletteStartDto)
        {
            return this.rouletteService.StartRoulette(rouletteStartDto);
        }

        [HttpPost("bet")]
        public void Bet([FromHeader(Name = "user-id")] string userId, RouletteBetDto rouletteBetDto)
        {
            this.rouletteService.Bet(userId, rouletteBetDto);
        }

        [HttpPost("close")]
        [ProducesResponseType(typeof(RouletteCloseResponseDto), StatusCodes.Status200OK)]
        public RouletteCloseResponseDto CloseRoulette(RouletteCloseDto rouletteCloseDto)
        {
            return this.rouletteService.CloseRoulette(rouletteCloseDto);
        }

        [HttpGet("get_all")]
        [ProducesResponseType(typeof(List<RouletteDto>), StatusCodes.Status200OK)]
        public List<RouletteDto> GetAllRouletteWithStatus()
        {
            return this.rouletteService.GetAllRoulettesWithStatus();
        }
    }
}
