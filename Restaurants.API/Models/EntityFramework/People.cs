using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class People : BaseEntity
    {
        [Required]
        [Column(Order = 1)]
        public string PersonFirstName { get; set; }

        [Column(Order = 2)]
        public string PersonMiddleName { get; set; }

        [Required]
        [Column(Order = 3)]
        public string PersonLastName { get; set; }

        [NonSerialized]
        private Guests _Guest;

		[JsonIgnore]
		public virtual Guests ThePersonAsGuest
        {
            get
            {
                return _Guest;
            }
            set
            {
                _Guest = value;
            }
        }

        [NonSerialized]
        private Employees _Employee;

		[JsonIgnore]
		public virtual Employees ThePersonAsEmployee
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

        [NonSerialized]
        private Employers _Employer;

		[JsonIgnore]
		public virtual Employers ThePersonAsEmployer
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
    }
}
