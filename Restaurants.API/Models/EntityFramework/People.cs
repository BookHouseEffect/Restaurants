using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class People : BaseEntity
    {
        [Required]
        public string PersonFirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string PersonLastName { get; set; }
    }
}
