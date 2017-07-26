using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class Guests : BaseEntity
    {
        [Required]
        [Column(Order = 1)]
        public long PersonId { get; set; }

        [NonSerialized]
        private People _Person;

        [ForeignKey("PersonId")]
        public virtual People TheGuestDetails
        {
            get
            {
                return _Person;
            }
            set
            {
                _Person = value;
            }
        }

        [NonSerialized]
        private ICollection<Orders> _Orders;

        public virtual ICollection<Orders> TheOrdersFromThisGuest
        {
            get
            {
                return _Orders;
            }
            set
            {
                _Orders = value;
            }
        }
    }
}
