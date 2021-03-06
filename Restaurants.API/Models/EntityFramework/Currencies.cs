﻿using Restaurants.API.Extensions;
using Restaurants.API.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class Currencies : BaseEnumEntity
    {
        [Required, MaxLength(100)]
        [Column(Order = 1)]
        public string CurrencySign { get; set; }

        [MaxLength(100)]
        [Column(Order = 2)]
        public string CurrencyFullName { get; set; }

        protected Currencies() { } //For EF

        private Currencies(CurrencyEnum @enum)
        {
            this.Id = (int)@enum;
            this.CurrencySign = @enum.ToString();
            this.CurrencyFullName = @enum.GetEnumDescription();
        }

        public static implicit operator Currencies(CurrencyEnum @enum)
            => new Currencies(@enum);

        public static implicit operator CurrencyEnum(Currencies currency)
            => (CurrencyEnum)currency.Id;
    }
}
