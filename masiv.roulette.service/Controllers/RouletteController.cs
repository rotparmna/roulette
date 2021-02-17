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

        //public void Bet(int number, int idRoulette)
        //{

        //}

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
