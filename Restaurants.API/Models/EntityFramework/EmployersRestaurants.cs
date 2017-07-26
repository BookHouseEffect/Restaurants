﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    public class EmployersRestaurants : BaseEntity
    {

        public long EmployerId { get; set; }

        public long RestaurantId { get; set; }

        [NonSerialized]
        private Employers _Employer;

        [ForeignKey("EmployerId")]
        public virtual Employers TheEmployer
        {
            get
            {
                return _Employer;
            }
            set
            {
                _Employer = value;
            }
        }

        [NonSerialized]
        private RestaurantObjects _Restaurant;

        [ForeignKey("RestaurantId")]
        public virtual RestaurantObjects TheRestaurant
        {
            get
            {
                return _Restaurant;
            }
            set
            {
                _Restaurant = value;
            }
        }
    }
}