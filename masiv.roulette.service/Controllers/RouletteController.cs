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

        [HttpPost]
        [ProducesResponseType(typeof(RouletteAddDto), StatusCodes.Status200OK)]
        public RouletteAddDto Add()
        {
            return this.rouletteService.Add();
        }

        //public bool Start(int idRoulette)
        //{
        //    return true;
        //}

        //public void Bet(int number, int idRoulette)
        //{

        //}

        //public void Close(int idRoulette)
        //{

        //}

        [HttpGet]
        [ProducesResponseType(typeof(RouletteAddDto), StatusCodes.Status200OK)]
        public string GetAll()
        {
            return string.Empty;
        }
    }
}
