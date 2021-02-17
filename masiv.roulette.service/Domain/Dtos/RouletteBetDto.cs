﻿using Masiv.Roulette.API.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Masiv.Roulette.API.Domain.Dtos
{
    public class RouletteBetDto
    {
        public string IdRoulette { get; set; }

        [Range(0, 36, ErrorMessage = "Los número validos para apostar son del 0 al 36")]
        public int Number { get; set; }

        public ColorEnum Color { get; set; }

        [Range(0, 10000, ErrorMessage = "El valor maximo apostar es de USD$10000")]
        public double CashAmount { get; set; }
    }
}
