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

    /// <summary>
    /// The roulette service.
    /// </summary>
    public class RouletteService : IRouletteService
    {
        /// <summary>
        /// The key of cache to roulette.
        /// </summary>
        private const string NAMEKEY = "roulette";

        /// <summary>
        /// The cache middleware.
        /// </summary>
        private readonly ICacheMiddleware<List<Domain.Entities.Roulette>> cacheMiddleware;

        /// <summary>
        /// The random utility.
        /// </summary>
        private readonly IGenerateRandom generateRandom;

        /// <summary>
        /// The objects roulettes in cache.
        /// </summary>
        private List<Roulette> roulettesInCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouletteService"/> class.
        /// </summary>
        /// <param name="cacheMiddleware">The cache middleware.</param>
        /// <param name="generateRandom">The random utility.</param>
        public RouletteService(ICacheMiddleware<List<Roulette>> cacheMiddleware, IGenerateRandom generateRandom)
        {
            this.roulettesInCache = cacheMiddleware.GetValue(NAMEKEY);
            this.cacheMiddleware = cacheMiddleware;
            this.generateRandom = generateRandom;
        }

        /// <summary>
        /// Add new roulette.
        /// </summary>
        /// <returns>Object with Id the new roulette.</returns>
        public RouletteAddResponseDto Add()
        {
            var newRoulette = new Roulette
            {
                Id = Guid.NewGuid().ToString()
            };
            this.roulettesInCache = this.cacheMiddleware.GetValue(NAMEKEY);
            this.roulettesInCache.Add(newRoulette);
            this.UpdateCache();

            return new RouletteAddResponseDto
            {
                Id = newRoulette.Id
            };
        }

        /// <summary>
        /// Bet to roulette.
        /// </summary>
        /// <param name="userId">Id user that bet.</param>
        /// <param name="rouletteBetDto">Object with information the roulette.</param>
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

        /// <summary>
        /// Close the roulette.
        /// </summary>
        /// <param name="rouletteCloseDto">Object with information the roulette.</param>
        /// <returns>Object with information the operation.</returns>
        public RouletteCloseResponseDto Close(RouletteCloseDto rouletteCloseDto)
        {
            RouletteCloseResponseDto winner = this.GetWinner();
            var roulette = this.GetById(rouletteCloseDto.Id);
            roulette.Status = StatusEnum.Close;
            this.UpdateWinnerIntoBets(roulette.Bets, winner);
            this.UpdateCache();

            return winner;
        }

        /// <summary>
        /// Start the roulette.
        /// </summary>
        /// <param name="rouletteStartDto">Object with Id roulette to start.</param>
        /// <returns>Object with information the operation.</returns>
        public RouletteStartResponseDto Start(RouletteStartDto rouletteStartDto)
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

        /// <summary>
        /// Get all roulettes with status.
        /// </summary>
        /// <returns>Object with information.</returns>
        public List<RouletteDto> GetAll()
        {
            return this.roulettesInCache.Select(x => new RouletteDto
            {
                Id = x.Id,
                Status = x.Status
            }).ToList();
        }

        /// <summary>
        /// Update the winner into the bets.
        /// </summary>
        /// <param name="bets">The bets of roulette.</param>
        /// <param name="winner">The object with information to winner.</param>
        private void UpdateWinnerIntoBets(List<Bet> bets, RouletteCloseResponseDto winner)
        {
            foreach (var item in bets)
            {
                double cash = 0;
                item.IsWin = item.Number == winner.WinningNumber || item.Color == winner.WinnigColor;
                if (item.Number == winner.WinningNumber)
                {
                    cash = item.CashAmount * 5;
                }
                if (item.Color == winner.WinnigColor)
                {
                    cash += item.CashAmount * 1.8;
                }
                item.WinnigCash = cash;
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

        /// <summary>
        /// Generate the number and color winner.
        /// </summary>
        /// <returns>Object to information the winner.</returns>
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

        /// <summary>
        /// Get roulette by id.
        /// </summary>
        /// <param name="id">The identification.</param>
        /// <returns>Entity roulette.</returns>
        private Roulette GetById(string id)
        {
            return this.roulettesInCache
                .Where(x => x.Id == id)
                .DefaultIfEmpty(new Domain.Entities.Roulette { Id = Guid.Empty.ToString() })
                .FirstOrDefault();
        }

        /// <summary>
        /// Update the cache.
        /// </summary>
        private void UpdateCache()
        {
            this.cacheMiddleware.SetValue(NAMEKEY, this.roulettesInCache);
        }
    }
}
