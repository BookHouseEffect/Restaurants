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
    }
}
