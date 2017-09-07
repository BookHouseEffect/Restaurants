using Newtonsoft.Json;
using Restaurants.API.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class EmployeeTypes : BaseEnumEntity
    {
        [Required]
        [Column(Order = 1)]
        public string EmployeeTypeName { get; set; }

        protected EmployeeTypes() { } //For EF

        private EmployeeTypes(EmployeeTypeEnum @enum)
        {
            this.Id = (long)@enum;
            this.EmployeeTypeName = @enum.ToString();
        }

        public static implicit operator EmployeeTypes(EmployeeTypeEnum @enum)
            => new EmployeeTypes(@enum);

        public static implicit operator EmployeeTypeEnum(EmployeeTypes type)
            => (EmployeeTypeEnum)type.Id;

        [NonSerialized]
        private ICollection<AssignedEmployeeTypes> _AssignedEmployees;

		[JsonIgnore]
        public virtual ICollection<AssignedEmployeeTypes> TheAssignedEmployees
        {
            get
            {
                return _AssignedEmployees;
            }
            set
            {
                _AssignedEmployees = value;
            }
        }
    }
}
