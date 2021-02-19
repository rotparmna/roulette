
//-----------------------------------------------------------------------------
// <copyright file="RouletteDto.cs" company="Roulette API">
//     Copyright © Roulette API All rights reserved.
// </copyright>
//-----------------------------------------------------------------------------
namespace Masiv.Roulette.API.Domain.Dtos
{
    using Masiv.Roulette.API.Domain.Enums;

    public class RouletteDto
    {
        public string Id { get; set; }
        public StatusEnum Status { get; set; }
    }
}
