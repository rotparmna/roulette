using Masiv.Roulette.API.Contracts;
using Masiv.Roulette.API.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Masiv.Roulette.API.Controllers
{
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
        public RouletteAddResponseDto Add()
        {
            return rouletteService.Add();
        }

        [HttpPost("start")]
        [ProducesResponseType(typeof(RouletteStartResponseDto), StatusCodes.Status200OK)]
        public RouletteStartResponseDto Start(RouletteStartDto rouletteStartDto)
        {
            return rouletteService.Start(rouletteStartDto);
        }

        [HttpPost("bet")]
        public void Bet([FromHeader(Name = "user-id")] string userId, RouletteBetDto rouletteBetDto)
        {
            rouletteService.Bet(userId, rouletteBetDto);
        }

        //public void Close(int idRoulette)
        //{

        //}

        [HttpGet]
        [ProducesResponseType(typeof(RouletteAddResponseDto), StatusCodes.Status200OK)]
        public string GetAll()
        {
            return string.Empty;
        }
    }
}
