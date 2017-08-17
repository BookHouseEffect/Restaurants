using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class Employers : BaseEntity
    {
        [Required]
        [Column(Order =1)]
        public long PersonId { get; set; }

        private People _Person;

		[ForeignKey("PersonId")]
        public virtual People TheEmployerDetails
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
        private ICollection<EmployersRestaurants> _Restaurnant;

		[JsonIgnore]
		public virtual ICollection<EmployersRestaurants> TheEmployerRestaurantsOwned
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
        private ICollection<Employees> _Employee;

		[JsonIgnore]
		public virtual ICollection<Employees> TheEmployees
        {
            get
            {
                return _Employee;
            }
            set
            {
                _Employee = value;
            }
        }
    }
}
