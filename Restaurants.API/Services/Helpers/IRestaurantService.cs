using System.Collections.Generic;
using Restaurants.API.Models.EntityFramework;
using System;

namespace Restaurants.API.Services.Helpers
{
	interface IRestaurantService
	{
		List<RestaurantObjects> GetOwnerRestaurants(
			long ownerId,
			int pageNumber,
			int pageSize
		);

		List<Employers> GetRestaurantOwners(
			long restaurantId,
			int pageNumber,
			int pageSize
		);

		RestaurantObjects GetRestaurant(
			long id
		);

		Tuple<RestaurantObjects, EmployersRestaurants> AddNewRestaurant(
			long ownerId,
			string restaurantName,
			string restaurantDescription
		);

		EmployersRestaurants AddCoowner(
			long ownerId,
			long restaurantId,
			long coownerId
		);

		RestaurantObjects UpdateRestaurant(
			long ownerId,
			long restaurantId,
			string restaurantName,
			string restaurantDescription
		);

		bool TransferOwnership(
			long ownerId,
			long restaurantId,
			long newOwnerId
		);

		bool RemoveCoowner(
			long ownerId,
			long restaurantId,
			long coownerId
		);

		bool CloseRestaurant(
			long ownerId,
			long restaurantId
		);
	}
}
