using Masiv.Roulette.API.Contracts;
using Masiv.Roulette.API.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masiv.Roulette.API.Service
{
    public class RouletteService : IRouletteService
    {
        private readonly List<Domain.Entities.Roulette> roulettes;

        public RouletteService()
        {
            this.roulettes = new List<Domain.Entities.Roulette>();
        }

        public RouletteAddDto Add()
        {
            var newRoulette = new Domain.Entities.Roulette
            {
                Id = Guid.NewGuid().ToString()
            };
            this.roulettes.Add(newRoulette);

            return new RouletteAddDto
            {
                Id = newRoulette.Id
            };
        }

        public List<RouletteAddDto> GetAll()
        {
            return this.roulettes.Select(x => new RouletteAddDto
            {
                Id = x.Id
            }).ToList();
        }
    }
}
