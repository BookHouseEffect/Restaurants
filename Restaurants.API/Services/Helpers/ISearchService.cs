using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;

namespace Restaurants.API.Services.Helpers
{
	interface ISearchService
    {
		List<Employers> SearchEmployers(
			string searchPhrase,
			long restaurantId,
			int pageNumber,
			int pageSize
		);
    }
}
