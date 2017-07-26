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
        [Column(Order = 1)]
        public long RestaurantId { get; set; }

        [Required]
        [Column(Order = 2)]
        public long OwnerId { get; set; }

        [Required]
        [Column(Order = 3)]
        public long PersonId { get; set; }

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
