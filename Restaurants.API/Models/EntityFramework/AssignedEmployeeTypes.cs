using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
	[Serializable]
    public class AssignedEmployeeTypes : BaseEntity
    {
        [Column(Order =1)]
        [Required]
        public long TypeId { get; set; }

        [Column(Order = 2)]
        [Required]
        public long EmployeeId { get; set; }

        private EmployeeTypes _Type;

        [ForeignKey("TypeId")]
        public virtual EmployeeTypes TheType
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

        [NonSerialized]
        private Employees _Employee;

		[JsonIgnore]
        [ForeignKey("EmployeeId")]
        public virtual Employees TheEmployee
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
