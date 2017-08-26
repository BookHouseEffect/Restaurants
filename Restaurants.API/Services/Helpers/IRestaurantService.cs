using System.Collections.Generic;
using Restaurants.API.Models.EntityFramework;
using System;
using System.Threading.Tasks;

namespace Restaurants.API.Services.Helpers
{
	interface IRestaurantService
	{
		Task<List<RestaurantObjects>> GetOwnerRestaurantsAsync(
			long ownerId,
			int pageNumber,
			int pageSize
		);

		Task<List<EmployersRestaurants>> GetRestaurantOwnersAsync(
			long restaurantId,
			int pageNumber,
			int pageSize
		);

		Task<RestaurantObjects> GetRestaurantAsync(
			long id
		);

		Task<Tuple<RestaurantObjects, EmployersRestaurants>> AddNewRestaurantAsync(
			long ownerId,
			string restaurantName,
			string restaurantDescription
		);

		Task<EmployersRestaurants> AddCoownerAsync(
			long ownerId,
			long restaurantId,
			long coownerId
		);

		Task<RestaurantObjects> UpdateRestaurantAsync(
			long ownerId,
			long restaurantId,
			string restaurantName,
			string restaurantDescription
		);

		Task<bool> TransferOwnershipAsync(
			long ownerId,
			long restaurantId,
			long newOwnerId
		);

		Task<bool> RemoveCoownerAsync(
			long ownerId,
			long restaurantId,
			long coownerId
		);

		Task<bool> CloseRestaurantAsync(
			long ownerId,
			long restaurantId
		);
	}
}
