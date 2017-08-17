using Restaurants.API.Models.EntityFramework;
using System;

namespace Restaurants.API.Services.Implementation
{
	public abstract class BaseService
    {
		protected People LogedInPerson;

		protected BaseService(){
			this.LogedInPerson = null;
		}

		protected BaseService(People logedInPerson){
			this.LogedInPerson = logedInPerson;
		}

		protected void CheckTheLoggedInPerson()
		{
			if (this.LogedInPerson == null || this.LogedInPerson.Id == 0)
				throw new Exception("Operation not permitted for anonymous user");
		}
	}
}
