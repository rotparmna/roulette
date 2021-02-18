using Masiv.Roulette.API.Contracts;
using Masiv.Roulette.API.Domain.Dtos;
using Masiv.Roulette.API.Domain.Enums;
using Masiv.Roulette.API.Middleware.Cache;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Masiv.Roulette.API.Service
{
    public class RouletteService : IRouletteService
    {
        private const string NAME_KEY = "roulette";
        private readonly List<Domain.Entities.Roulette> roulettes;
        private readonly ICacheMiddleware<List<Domain.Entities.Roulette>> cacheMiddleware;

        public RouletteService(ICacheMiddleware<List<Domain.Entities.Roulette>> cacheMiddleware)
        {
            roulettes = new List<Domain.Entities.Roulette>();
            this.cacheMiddleware = cacheMiddleware;
        }

        public RouletteAddResponseDto Add()
        {
            var newRoulette = new Domain.Entities.Roulette
            {
                Id = Guid.NewGuid().ToString()
            };
            var roulettesInCache = cacheMiddleware.GetValue(NAME_KEY);
            roulettesInCache.Add(newRoulette);
            cacheMiddleware.SetValue(NAME_KEY, roulettesInCache);
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

        public RouletteCloseResponseDto Close(RouletteCloseDto rouletteCloseDto)
        {
            int winningNumber = new Random().Next(0, 36);
            ColorEnum winningColor = ColorEnum.Black;
            if (winningNumber % 2 == 0)
                winningColor = ColorEnum.Red;
            RouletteCloseResponseDto rouletteClose = new RouletteCloseResponseDto()
            {
                WinnigColor = winningColor,
                WinningNumber = winningNumber
            };
            var roulette = GetById(rouletteCloseDto.Id);
            foreach (var item in roulette.Bets)
            {
                double cash = 0;
                item.IsWin = item.Number == winningNumber || item.Color == winningColor;
                if (item.Number == winningNumber)
                    cash = item.CashAmount * 5;
                if (item.Color == winningColor)
                    cash += item.CashAmount * 1.8;
                item.WinnigCash = cash;
                if (!item.IsWin.Value)
                    item.CashAmount *= -1;
            }

            return rouletteClose;
        }

        public RouletteStartResponseDto Start(RouletteStartDto rouletteStartDto)
        {
            var roulette = GetById(rouletteStartDto.Id);
            if (roulette.Id != Guid.Empty.ToString())
            {
                roulette.Status = StatusEnum.Open;

                return new RouletteStartResponseDto
                {
                    Result = ResultEnum.Success
                };
            }
            else
            {
                return new RouletteStartResponseDto
                {
                    Result = ResultEnum.Denied
                };
            }
        }

        public List<RouletteDto> GetAll()
        {
            return roulettes.Select(x => new RouletteDto
            {
                Id = x.Id,
                Status = x.Status
            }).ToList();
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
