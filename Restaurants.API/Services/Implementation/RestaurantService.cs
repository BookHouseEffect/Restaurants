using Restaurants.API.Services.Helpers;
using System;
using System.Collections.Generic;
using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Models.Context;
using System.Threading.Tasks;

namespace Restaurants.API.Services.Implementation
{
	public class RestaurantService : BaseService, IRestaurantService
	{

		public RestaurantService(AppDbContext dbContext, People logedInPerson)
			: base(dbContext, logedInPerson) { }

		public async Task<EmployersRestaurants> AddCoownerAsync(long ownerId, long restaurantId, long coownerId)
		{
			CheckTheLoggedInPerson();

			EmployersRestaurants currentConnection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			Employers newEmployer = await CheckEmployerExistenceAsync(coownerId);

			EmployersRestaurants item = new EmployersRestaurants
			{
				EmployerId = newEmployer.Id,
				RestaurantId = currentConnection.TheRestaurant.Id
			};

			await EmployerRestaurantRepo.AddAsync(item, ModifierId);

			return item;
		}

		public async Task<Tuple<RestaurantObjects, EmployersRestaurants>> AddNewRestaurantAsync(long ownerId, string restaurantName, string restaurantDescription)
		{
			CheckTheLoggedInPerson();

			Employers currentEmployer = await CheckEmployerExistenceAsync(ownerId);

			RestaurantObjects restaurantItem = new RestaurantObjects
			{
				Name = restaurantName,
				Description = restaurantDescription
			};

			await RestaurantRepo.AddAsync(restaurantItem, ModifierId);

			EmployersRestaurants item = new EmployersRestaurants
			{
				EmployerId = currentEmployer.Id,
				RestaurantId = restaurantItem.Id
			};

			await EmployerRestaurantRepo.AddAsync(item, ModifierId);

			return new Tuple<RestaurantObjects, EmployersRestaurants>(restaurantItem, item);
		}

		public async Task<bool> CloseRestaurantAsync(long ownerId, long restaurantId)
		{
			EmployersRestaurants currentConnection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);

			List<EmployersRestaurants> dataToRemove = await EmployerRestaurantRepo.GetByRestaurantId(restaurantId);
			foreach (var data in dataToRemove)
				await EmployerRestaurantRepo.RemoveAsync(data);

			return true;
		}

		public async Task<List<RestaurantObjects>> GetOwnerRestaurantsAsync(long ownerId, int pageNumber, int pageSize)
		{
			return await RestaurantRepo.GetRestaurantsByOwnerIdPaged(ownerId, pageNumber, pageSize);
		}

		public async Task<RestaurantObjects> GetRestaurantAsync(long id)
		{
			return await RestaurantRepo.FindById(id);
		}

		public async Task<List<EmployersRestaurants>> GetRestaurantOwnersAsync(long restaurantId, int pageNumber, int pageSize)
		{
			return await EmployerRestaurantRepo.GetOwnersByRestaurantIdPaged(restaurantId, pageNumber, pageSize);
		}

		public async Task<bool> RemoveCoownerAsync(long ownerId, long restaurantId, long coownerId)
		{
			EmployersRestaurants currentConnection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			Employers employerToRemove = await CheckEmployerExistenceAsync(coownerId);

			long count = await EmployerRestaurantRepo.GetNumbetOfEmployers(restaurantId);
			if (count == 1)
				throw new Exception("There's ony one owner to the restaurant. Use Close restaurant instead");

			if (currentConnection.TheEmployer.Id == employerToRemove.Id)
				throw new Exception("The owner cannot remove itself. Use Transfer ownership instead");

			EmployersRestaurants data = await EmployerRestaurantRepo.GetByRestaurantIdAndEmployerId(restaurantId, coownerId);
			if (data == null)
				throw new Exception(String.Format("The given owner with id:{0} can't be removed beacause is not an owner", coownerId));

			await EmployerRestaurantRepo.RemoveAsync(data);

			return true;
		}

		public async Task<bool> TransferOwnershipAsync(long ownerId, long restaurantId, long newOwnerId)
		{
			EmployersRestaurants currentConnection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			Employers newEmployer = await CheckEmployerExistenceAsync(newOwnerId);

			EmployersRestaurants oldData = await EmployerRestaurantRepo.GetByRestaurantIdAndEmployerId(restaurantId, ownerId);
			if (oldData == null)
				throw new Exception(String.Format("The given owner with id:{0} is not an owner.", ownerId));

			EmployersRestaurants newData = await EmployerRestaurantRepo.GetByRestaurantIdAndEmployerId(restaurantId, newOwnerId);
			if (newData != null)
				throw new Exception(String.Format("The given new owner with id:{0} is already an owner.", newOwnerId));

			await EmployerRestaurantRepo.AddAsync
			(
				new EmployersRestaurants
				{
					RestaurantId = restaurantId,
					EmployerId = newOwnerId
				},
				ModifierId
			);

			await EmployerRestaurantRepo.RemoveAsync(oldData);

			return true;
		}

		public async Task<RestaurantObjects> UpdateRestaurantAsync(long ownerId, long restaurantId, string restaurantName, string restaurantDescription)
		{
			EmployersRestaurants currentConnection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			RestaurantObjects currentRestaurant = currentConnection.TheRestaurant;

			currentRestaurant.Name = restaurantName;
			currentRestaurant.Description = restaurantDescription;

			CheckTheLoggedInPerson();

			await RestaurantRepo.UpdateAsync(currentRestaurant, ModifierId);

			return currentRestaurant;
		}
	}
}
