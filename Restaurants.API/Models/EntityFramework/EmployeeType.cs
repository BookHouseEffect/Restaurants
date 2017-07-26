using Restaurants.API.Models.Enums;
using System;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class EmployeeType : BaseEnumEntity
    {
        public string EmployeeTypeName { get; set; }

        protected EmployeeType() { } //For EF

        private EmployeeType(EmployeeTypeEnum @enum)
        {
            this.Id = (int)@enum;
            this.EmployeeTypeName = @enum.ToString();
        }

        public static implicit operator EmployeeType(EmployeeTypeEnum @enum)
            => new EmployeeType(@enum);

        public static implicit operator EmployeeTypeEnum(EmployeeType type)
            => (EmployeeTypeEnum)type.Id;
    }
}
