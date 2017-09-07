using Restaurants.API.Models.Api;
using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurants.API.Services.Helpers
{
	interface ISearchService
    {
		Task<List<Employers>> SearchEmployersAsync(
			string searchPhrase,
			long restaurantId,
			int pageNumber,
			int pageSize
		);

		Task<List<SearchRestaurantResult>> SearchRestaurantAsync(
			string searchTerm,
			float currentLongitude,
			float currentLatitude,
			int pageNumber,
			int pageSize
		);
    }
}
