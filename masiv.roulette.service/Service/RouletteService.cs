using Masiv.Roulette.API.Contracts;
using Masiv.Roulette.API.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Masiv.Roulette.API.Service
{
    public class RouletteService : IRouletteService
    {
        private readonly List<Domain.Entities.Roulette> roulettes;

        public RouletteService()
        {
            roulettes = new List<Domain.Entities.Roulette>();
        }

        public RouletteAddResponseDto Add()
        {
            var newRoulette = new Domain.Entities.Roulette
            {
                Id = Guid.NewGuid().ToString()
            };
            roulettes.Add(newRoulette);

            return new RouletteAddResponseDto
            {
                Id = newRoulette.Id
            };
        }

        public void Bet(string userId, RouletteBetDto rouletteBetDto)
        {
            var roulette = GetById(rouletteBetDto.IdRoulette);
            if (roulette.Id != Guid.Empty.ToString())
            {
                roulette.Bets.Add(new Domain.Entities.Bet
                {
                    CashAmount = rouletteBetDto.CashAmount,
                    Color = rouletteBetDto.Color,
                    Number = rouletteBetDto.Number,
                    UserId = userId
                });
            }
        }

        public List<RouletteAddResponseDto> GetAll()
        {
            return roulettes.Select(x => new RouletteAddResponseDto
            {
                Id = x.Id
            }).ToList();
        }

        public RouletteStartResponseDto Start(RouletteStartDto rouletteStartDto)
        {
            var roulette = GetById(rouletteStartDto.Id);
            if (roulette.Id != Guid.Empty.ToString())
            {
                roulette.Status = Domain.Enums.StatusEnum.Open;

                return new RouletteStartResponseDto
                {
                    Result = Domain.Enums.ResultEnum.Success
                };
            }
            else
            {
                return new RouletteStartResponseDto
                {
                    Result = Domain.Enums.ResultEnum.Denied
                };
            }
        }

        private Domain.Entities.Roulette GetById(string id)
        {
            return roulettes
                .Where(x => x.Id == id)
                .DefaultIfEmpty(new Domain.Entities.Roulette { Id = Guid.Empty.ToString() })
                .FirstOrDefault();
        }
    }
}
