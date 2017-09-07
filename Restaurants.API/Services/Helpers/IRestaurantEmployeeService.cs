using Restaurants.API.Models.Enums;
using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurants.API.Services.Helpers
{
	public interface IRestaurantEmployeeService
    {

		Task<Employees> AddEmployeeToRestaurantAsync(
			long ownerId,
			long restaurantId,
			long employeeId
		);

		Task<Employees> GetEmployeeDetailsAsync(
			long employeeId
		);

		Task<List<Employees>> GetEmployeeListForRestaurantAsync(
			long restaurantId,
			int pageNumber,
			int pageSize
		);

		Task<Employees> TransferEmployeeToAnotherRestaurantAsync(
			long ownerId,
			long restaurantId,
			long employeeId,
			long newRestaurantId
		);

		Task<Employees> FireEmployeeOutAsync(
			long ownerId,
			long restaurantId,
			long employeeId
		);

		Task<List<AssignedEmployeeTypes>> AssignResponsibilitiesForEmployeeAsync(
			long ownerId,
			long restaurantId,
			long employeeId,
			List<EmployeeTypeEnum> responsibilities
		);

		Task<List<AssignedEmployeeTypes>> GetEmployeeResponsibilitiesAsync(
			long employeeId
		);

		Task<List<EmployeeTypes>> GetEmployeeTypesEnumAsync();
    }
}
