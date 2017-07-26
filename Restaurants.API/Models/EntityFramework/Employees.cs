using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class Employees : BaseEntity
    {

        [Required]
        public long PersonId { get; set; }

        [Required]
        public long RestaurantId { get; set; }

        [Required]
        public long OwnerId { get; set; }

        [NonSerialized]
        private People _Person;

        [ForeignKey("PersonId")]
        public virtual People TheEmployeeDetails
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
        private RestaurantObjects _Restaurnant;

        [ForeignKey("RestaurantId")]
        public virtual RestaurantObjects TheRestaurantEmployeeWorksIn
        {
            get
            {
                return _Restaurnant;
            }
            set
            {
                _Restaurnant = value;
            }
        }

        [NonSerialized]
        private Employers _Owner;

        [ForeignKey("OwnerId")]
        public virtual Employers TheEmployerEmployeeWorksFor
        {
            get
            {
                return _Owner;
            }
            set
            {
                _Owner = value;
            }
        }

        [NonSerialized]
        private ICollection<EmployeeType> _Type;

        public virtual ICollection<EmployeeType> TheTypesOfThisEmployee
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }
    }
}
