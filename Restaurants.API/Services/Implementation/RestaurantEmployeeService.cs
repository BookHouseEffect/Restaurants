using Restaurants.API.Persistence.Implementation;
using Restaurants.API.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Models.Enums;

namespace Restaurants.API.Services.Implementation
{
	public class RestaurantEmployeeService : BaseService, IRestaurantEmployeeService
	{
		private EmployeesRepository EmployeesRepo;
		private EmployeeTypesRepository TypeRepo;
		private AssignedEmployeeTypesRepository AssignedTypeRepo;

		public RestaurantEmployeeService(AppDbContext dbContext, People logedInPerson) : base(dbContext, logedInPerson)
		{
			this.EmployeesRepo = new EmployeesRepository(dbContext);
			this.TypeRepo = new EmployeeTypesRepository(dbContext);
			this.AssignedTypeRepo = new AssignedEmployeeTypesRepository(dbContext);
		}

		public async Task<Employees> AddEmployeeToRestaurantAsync(long ownerId, long restaurantId, long employeeId)
		{
			this.CheckTheLoggedInPerson();

			EmployersRestaurants employersRestaurants = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			Employees employee = await CheckEmployeesExistenceAsync(employeeId);

			if (employee.RestaurantId.HasValue)
				throw new Exception("Employee already employed! Use transfer to another restaurant instead.");

			employee.RestaurantId = restaurantId;
			await EmployeesRepo.UpdateAsync(employee, ModifierId);

			return employee;
		}

		public async Task<List<AssignedEmployeeTypes>> AssignResponsibilitiesForEmployeeAsync(long ownerId, long restaurantId, long employeeId, List<EmployeeTypeEnum> responsibilities)
		{
			this.CheckTheLoggedInPerson();

			EmployersRestaurants employersRestaurants = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			Employees employee = await CheckEmployeesExistenceAsync(employeeId);

			List<AssignedEmployeeTypes> assigned = await AssignedTypeRepo.GetTypesForEmployee(employee.Id);
			foreach (var item in assigned){
				if (responsibilities.Where(x => (long)x == item.Id).LongCount() == 0)
					await AssignedTypeRepo.RemoveAsync(item);
			}

			foreach (var item in responsibilities){
				if (assigned.Where(x=>x.Id == (long)item).LongCount() == 0){
					AssignedEmployeeTypes newItem = new AssignedEmployeeTypes
					{
						EmployeeId = employee.Id,
						TypeId = (long)item
					};
					await AssignedTypeRepo.AddAsync(newItem, this.ModifierId);
				}
			}

			return await AssignedTypeRepo.GetTypesForEmployee(employee.Id);
		}

		public async Task<Employees> FireEmployeeOutAsync(long ownerId, long restaurantId, long employeeId)
		{
			this.CheckTheLoggedInPerson();

			EmployersRestaurants employersRestaurants = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			Employees employee = await CheckEmployeesExistenceAsync(employeeId);

			if (!employee.RestaurantId.HasValue)
				throw new Exception("Can't fire non-employed employee.");

			employee.RestaurantId = null;
			await EmployeesRepo.UpdateAsync(employee, ModifierId);

			return employee;
		}

		public async Task<Employees> GetEmployeeDetailsAsync(long employeeId)
		{
			return await EmployeesRepo.FindById(employeeId);
		}

		public async Task<List<Employees>> GetEmployeeListForRestaurantAsync(long restaurantId, int pageNumber, int pageSize)
		{
			return await EmployeesRepo.GetEmployeesByRestaurantId(restaurantId, pageNumber, pageSize);
		}

		public async Task<List<AssignedEmployeeTypes>> GetEmployeeResponsibilitiesAsync(long employeeId)
		{
			return await AssignedTypeRepo.GetTypesForEmployee(employeeId);
		}

		public async Task<List<EmployeeTypes>> GetEmployeeTypesEnumAsync()
		{
			return await TypeRepo.GetEmployeeTypeList();
		}

		public async Task<Employees> TransferEmployeeToAnotherRestaurantAsync(long ownerId, long restaurantId, long employeeId, long newRestaurantId)
		{
			this.CheckTheLoggedInPerson();

			EmployersRestaurants currentEmployerRestaurant = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			EmployersRestaurants newEmployerRestaurant = await CheckEmployerRestaurantAsync(ownerId, newRestaurantId);
			Employees employee = await CheckEmployeesExistenceAsync(employeeId);

			if (!employee.RestaurantId.HasValue)
				throw new Exception("Cannot transfer non-employed employee.");

			if (employee.RestaurantId.Value != restaurantId)
				throw new Exception("Employee not employed in the given restaurant.");

			employee.RestaurantId = newRestaurantId;
			await EmployeesRepo.UpdateAsync(employee, ModifierId);

			return employee;
		}

		private async Task<Employees> CheckEmployeesExistenceAsync(long employeeId){
			Employees employee = await EmployeesRepo.FindById(employeeId);
			if (employee == null)
				throw new Exception("Non existing entry!");
			return employee;
		}
	}
}
