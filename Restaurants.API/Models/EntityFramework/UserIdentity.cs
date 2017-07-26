using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurants.API.Models.EntityFramework
{
    [Serializable]
    public class UserIdentity : IdentityUser<long>
    {

        [Required]
        public long PersonId { get; set; }

        [NonSerialized]
        private People _Person;

        public People ThePersonOwningThisIdentity
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
    }
}
