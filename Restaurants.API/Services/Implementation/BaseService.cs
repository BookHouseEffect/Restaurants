using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Persistence.Implementation;
using System;

namespace Restaurants.API.Services.Implementation
{
	public abstract class BaseService
    {
		private People LogedInPerson;
		protected RestaurantRepository RestaurantRepo;
		protected EmployersRepository EmployersRepo;
		protected EmployersRestaurantsRepository EmployerRestaurantRepo;

		protected BaseService(AppDbContext dbContext, People logedInPerson){
			this.LogedInPerson = logedInPerson;

			this.RestaurantRepo = new RestaurantRepository(dbContext);
			this.EmployersRepo = new EmployersRepository(dbContext);
			this.EmployerRestaurantRepo = new EmployersRestaurantsRepository(dbContext);
		}

		protected void CheckTheLoggedInPerson()
		{
			if (this.LogedInPerson == null || this.LogedInPerson.Id == 0)
				throw new Exception("Operation not permitted for anonymous user");
		}

		//protected RestaurantObjects CheckRestaurantExistence(long restaurantId)
		//{
		//	RestaurantObjects rest = RestaurantRepo.FindById(restaurantId);
		//	if (rest == null)
		//		throw new Exception(String.Format("Restaurant with id {0} does not exist", restaurantId));

		//	return rest;
		//}

		protected Employers CheckEmployerExistence(long employerId)
		{
			Employers empl = EmployersRepo.FindById(employerId);
			if (empl == null)
				throw new Exception(String.Format("Employer with id {0} does not exist", employerId));

			return empl;
		}

		protected EmployersRestaurants CheckEmployerRestaurant(long employerId, long restaurantId)
		{
			EmployersRestaurants emplRest = EmployerRestaurantRepo.GetByRestaurantIdAndEmployerId(restaurantId, employerId);
			if (emplRest == null)
				throw new Exception(String.Format("The restaurant with {0} does not have owner with id {1}", restaurantId, employerId));

			return emplRest;
		}

		protected long ModifierId {
			get
			{
				if (LogedInPerson != null)
					return LogedInPerson.Id;
				return 0;
			}
		}
	}
}
