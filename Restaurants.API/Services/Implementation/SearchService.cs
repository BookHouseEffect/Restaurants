using Restaurants.API.Services.Helpers;
using System.Collections.Generic;
using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Persistence.Implementation;
using Restaurants.API.Models.Context;

namespace Restaurants.API.Services.Implementation
{
	public class SearchService : BaseService, ISearchService
	{
		private EmployersRepository EmployersRepo;

		public SearchService(AppDbContext dbContext)
		{
			this.EmployersRepo = new EmployersRepository(dbContext);
		}

		public List<Employers> SearchEmployers(string searchPhrase, long restaurantId, int pageNumber, int pageSize)
		{
			return EmployersRepo.GetEmployersNotOwnersToCurrentRestaurant(searchPhrase, restaurantId, pageNumber, pageSize);
		}
	}
}
