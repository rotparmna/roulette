//-----------------------------------------------------------------------------
// <copyright file="RouletteService.cs" company="Roulette API">
//     Copyright © Roulette API All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Masiv.Roulette.API.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Masiv.Roulette.API.Contracts;
    using Masiv.Roulette.API.Domain.Dtos;
    using Masiv.Roulette.API.Domain.Entities;
    using Masiv.Roulette.API.Domain.Enums;
    using Masiv.Roulette.API.Middleware.Cache;
    using Masiv.Roulette.API.Utilities;

    public class RouletteService : IRouletteService
    {
        private const string NAMEKEY = "roulette";
        private readonly ICacheMiddleware<List<Roulette>> cacheMiddleware;
        private readonly IGenerateRandom generateRandom;
        private List<Roulette> roulettesInCache;

        public RouletteService(ICacheMiddleware<List<Roulette>> cacheMiddleware, IGenerateRandom generateRandom)
        {
            this.cacheMiddleware = cacheMiddleware;
            this.generateRandom = generateRandom;
            this.LoadCache();
        }

        public RouletteAddResponseDto AddRoulette()
        {
            var newRoulette = new Roulette
            {
                Id = Guid.NewGuid().ToString()
            };
            this.roulettesInCache.Add(newRoulette);
            this.UpdateCache();

            return new RouletteAddResponseDto
            {
                Id = newRoulette.Id
            };
        }

        public void Bet(string userId, RouletteBetDto rouletteBetDto)
        {
            var roulette = this.GetById(rouletteBetDto.IdRoulette);
            if (roulette.Id != Guid.Empty.ToString() && roulette.Status == StatusEnum.Open)
            {
                roulette.Bets.Add(new Bet
                {
                    CashAmount = rouletteBetDto.CashAmount,
                    Color = rouletteBetDto.Color,
                    Number = rouletteBetDto.Number,
                    UserId = userId
                });
                this.UpdateCache();
            }
        }

        public RouletteCloseResponseDto CloseRoulette(RouletteCloseDto rouletteCloseDto)
        {
            RouletteCloseResponseDto winner = this.GetWinner();
            var roulette = this.GetById(rouletteCloseDto.Id);
            roulette.Status = StatusEnum.Close;
            this.UpdateWinnerIntoBets(roulette.Bets, winner);
            this.UpdateCache();

            return winner;
        }

        public RouletteStartResponseDto StartRoulette(RouletteStartDto rouletteStartDto)
        {
            var roulette = this.GetById(rouletteStartDto.Id);
            if (roulette.Id != Guid.Empty.ToString() && roulette.Status == StatusEnum.None)
            {
                roulette.Status = StatusEnum.Open;
                this.UpdateCache();

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

        public List<RouletteDto> GetAllRoulettesWithStatus()
        {
            return this.roulettesInCache.Select(x => new RouletteDto
            {
                Id = x.Id,
                Status = x.Status
            }).ToList();
        }

        private void LoadCache()
        {
            this.roulettesInCache = this.cacheMiddleware.GetValue(NAMEKEY);
        }

        private void UpdateWinnerIntoBets(List<Bet> bets, RouletteCloseResponseDto winner)
        {
            foreach (var item in bets)
            {
                item.IsWin = item.Number == winner.WinningNumber || item.Color == winner.WinnigColor;
                item.WinnigCash = this.CalculateCashAmount(item, winner);
                winner.Bets.Add(new BetCloseDto
                {
                    CashAmount = item.CashAmount,
                    Color = item.Color,
                    IsWin = item.IsWin,
                    Number = item.Number,
                    UserId = item.UserId,
                    WinnerAmount = item.WinnigCash.Value
                });
            }
        }

        private double CalculateCashAmount(Bet bet, RouletteCloseResponseDto winner)
        {
            double cash = 0;
            if (bet.Number == winner.WinningNumber)
            {
                cash = bet.CashAmount * 5;
            }
            if (bet.Color == winner.WinnigColor)
            {
                cash += bet.CashAmount * 1.8;
            }
            
            return cash;
        }

        private RouletteCloseResponseDto GetWinner()
        {
            int winningNumber = this.generateRandom.NextNumber(0, 36);
            ColorEnum winningColor = (winningNumber % 2 == 0) ? ColorEnum.Red : ColorEnum.Black;
            
            return new RouletteCloseResponseDto()
            {
                WinnigColor = winningColor,
                WinningNumber = winningNumber
            };
        }

        private Roulette GetById(string id)
        {
            return this.roulettesInCache
                .Where(x => x.Id == id)
                .DefaultIfEmpty(new Domain.Entities.Roulette { Id = Guid.Empty.ToString() })
                .FirstOrDefault();
        }

        private void UpdateCache()
        {
            this.cacheMiddleware.SetValue(NAMEKEY, this.roulettesInCache);
        }
    }
}
