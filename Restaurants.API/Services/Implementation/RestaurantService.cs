using Restaurants.API.Services.Helpers;
using System;
using System.Collections.Generic;
using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Models.Context;

namespace Restaurants.API.Services.Implementation
{
	public class RestaurantService : BaseService, IRestaurantService
	{

		public RestaurantService(AppDbContext dbContext, People logedInPerson)
			: base(dbContext, logedInPerson) { }

		public EmployersRestaurants AddCoowner(long ownerId, long restaurantId, long coownerId)
		{
			CheckTheLoggedInPerson();

			EmployersRestaurants currentConnection = CheckEmployerRestaurant(ownerId, restaurantId);
			Employers newEmployer = CheckEmployerExistence(coownerId);

			EmployersRestaurants item = new EmployersRestaurants
			{
				EmployerId = newEmployer.Id,
				RestaurantId = currentConnection.TheRestaurant.Id
			};

			EmployerRestaurantRepo.Add(item, ModifierId);

			return item;
		}

		public Tuple<RestaurantObjects, EmployersRestaurants> AddNewRestaurant(long ownerId, string restaurantName, string restaurantDescription)
		{
			CheckTheLoggedInPerson();

			Employers currentEmployer = CheckEmployerExistence(ownerId);

			RestaurantObjects restaurantItem = new RestaurantObjects
			{
				Name = restaurantName,
				Description = restaurantDescription
			};

			RestaurantRepo.Add(restaurantItem, ModifierId);

			EmployersRestaurants item = new EmployersRestaurants
			{
				EmployerId = currentEmployer.Id,
				RestaurantId = restaurantItem.Id
			};

			EmployerRestaurantRepo.Add(item, ModifierId);

			return new Tuple<RestaurantObjects, EmployersRestaurants>(restaurantItem, item);
		}

		public bool CloseRestaurant(long ownerId, long restaurantId)
		{
			EmployersRestaurants currentConnection = CheckEmployerRestaurant(ownerId, restaurantId);

			List<EmployersRestaurants> dataToRemove = EmployerRestaurantRepo.GetByRestaurantId(restaurantId);
			foreach (var data in dataToRemove)
				EmployerRestaurantRepo.Remove(data);

			return true;
		}

		public List<RestaurantObjects> GetOwnerRestaurants(long ownerId, int pageNumber, int pageSize)
		{
			return RestaurantRepo.GetRestaurantsByOwnerIdPaged(ownerId, pageNumber, pageSize);
		}

		public RestaurantObjects GetRestaurant(long id)
		{
			return RestaurantRepo.FindById(id);
		}

		public List<EmployersRestaurants> GetRestaurantOwners(long restaurantId, int pageNumber, int pageSize)
		{
			return EmployerRestaurantRepo.GetOwnersByRestaurantIdPaged(restaurantId, pageNumber, pageSize);
		}

		public bool RemoveCoowner(long ownerId, long restaurantId, long coownerId)
		{
			EmployersRestaurants currentConnection = CheckEmployerRestaurant(ownerId, restaurantId);
			Employers employerToRemove = CheckEmployerExistence(coownerId);

			long count = EmployerRestaurantRepo.GetNumbetOfEmployers(restaurantId);
			if (count == 1)
				throw new Exception("There's ony one owner to the restaurant. Use Close restaurant instead");

			if (currentConnection.TheEmployer.Id == employerToRemove.Id)
				throw new Exception("The owner cannot remove itself. Use Transfer ownership instead");

			EmployersRestaurants data = EmployerRestaurantRepo.GetByRestaurantIdAndEmployerId(restaurantId, coownerId);
			if (data == null)
				throw new Exception(String.Format("The given owner with id:{0} can't be removed beacause is not an owner", coownerId));

			EmployerRestaurantRepo.Remove(data);

			return true;
		}

		public bool TransferOwnership(long ownerId, long restaurantId, long newOwnerId)
		{
			EmployersRestaurants currentConnection = CheckEmployerRestaurant(ownerId, restaurantId);
			Employers newEmployer = CheckEmployerExistence(newOwnerId);

			EmployersRestaurants oldData = EmployerRestaurantRepo.GetByRestaurantIdAndEmployerId(restaurantId, ownerId);
			if (oldData == null)
				throw new Exception(String.Format("The given owner with id:{0} is not an owner.", ownerId));

			EmployersRestaurants newData = EmployerRestaurantRepo.GetByRestaurantIdAndEmployerId(restaurantId, newOwnerId);
			if (newData != null)
				throw new Exception(String.Format("The given new owner with id:{0} is already an owner.", newOwnerId));

			EmployerRestaurantRepo.Add
			(
				new EmployersRestaurants
				{
					RestaurantId = restaurantId,
					EmployerId = newOwnerId
				},
				ModifierId
			);

			EmployerRestaurantRepo.Remove(oldData);

			return true;
		}

		public RestaurantObjects UpdateRestaurant(long ownerId, long restaurantId, string restaurantName, string restaurantDescription)
		{
			EmployersRestaurants currentConnection = CheckEmployerRestaurant(ownerId, restaurantId);
			RestaurantObjects currentRestaurant = currentConnection.TheRestaurant;

			currentRestaurant.Name = restaurantName;
			currentRestaurant.Description = restaurantDescription;

			CheckTheLoggedInPerson();

			RestaurantRepo.Update(currentRestaurant, ModifierId);

			return currentRestaurant;
		}
	}
}
