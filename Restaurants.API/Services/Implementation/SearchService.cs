using Restaurants.API.Services.Helpers;
using System.Collections.Generic;
using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Models.Context;
using System.Threading.Tasks;
using Restaurants.API.Models.Api;

namespace Restaurants.API.Services.Implementation
{
	public class SearchService : BaseService, ISearchService
	{

		public SearchService(AppDbContext dbContext) : base(dbContext, null) { }

		public async Task<List<Employers>> SearchEmployersAsync(string searchPhrase, long restaurantId, int pageNumber, int pageSize)
		{
			return await EmployersRepo.GetEmployersNotOwnersToCurrentRestaurant(searchPhrase, restaurantId, pageNumber, pageSize);
		}

		public async Task<List<SearchRestaurantResult>> SearchRestaurantAsync(string searchTerm, float currentLongitude, float currentLatitude, int pageNumber, int pageSize)
		{
			return await RestaurantRepo.GetRestaurantsPagedListFilteredByCurrentLocation(
			   RepairSearchTerm(searchTerm), currentLatitude, currentLongitude, pageSize, pageNumber);
		}

		private string RepairSearchTerm(string searchTerm)
		{
			var repairedTerm = searchTerm ?? "";
			repairedTerm = repairedTerm.Trim();
			return repairedTerm;
		}
	}
}
