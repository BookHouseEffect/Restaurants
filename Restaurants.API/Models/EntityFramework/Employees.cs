using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class Employees : BaseEntity
    {
        [Column(Order = 1)]
        public Nullable<long> RestaurantId { get; set; }

        [Required]
        [Column(Order = 2)]
        public long PersonId { get; set; }

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

		[JsonIgnore]
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

        private ICollection<AssignedEmployeeTypes> _AssignedType;

		public virtual ICollection<AssignedEmployeeTypes> TheAssignedTypes
        {
            get
            {
                return _AssignedType;
            }
            set
            {
                _AssignedType = value;
            }
        }
    }
}
