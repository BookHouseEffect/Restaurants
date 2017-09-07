using Restaurants.API.Services.Helpers;
using Restaurants.API.Services.Implementation;
using System;
using System.Collections.Generic;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Restaurants.API.Models.Enums;

namespace Restaurants.API.Services
{
	public class ApplicationServices : 
	BaseService,
	IRestaurantService, 
	IContactSerivce,
	IScheduleService,
	IMenuService,
	IRestaurantEmployeeService
	{
		private IRestaurantService RestaurantService;
		private IContactSerivce ContactService;
		private IScheduleService ScheduleSerice;
		private IMenuService MenuService;
		private IRestaurantEmployeeService RestaurantEmployeeService;

		public ApplicationServices(AppDbContext dbContext, People logedInPerson) 
			: base(dbContext, logedInPerson) {
			this.RestaurantService = new RestaurantService(dbContext, logedInPerson);
			this.ContactService = new ContactService(dbContext, logedInPerson);
			this.ScheduleSerice = new ScheduleService(dbContext, logedInPerson);
			this.MenuService = new MenuService(dbContext, logedInPerson);
			this.RestaurantEmployeeService = new RestaurantEmployeeService(dbContext, logedInPerson);
		}

		public Task<LocationContact> AddContactAddressAsync(long ownerId, long restaurantId, int floor, string steetNumber, string route, string locality, string country, int zipCode, float latitude, float longitude, [Optional] string administrativeAreaLevel1, [Optional] string administrativeAreaLevel2, [Optional] string googleLink)
		{
			return ContactService.AddContactAddressAsync(ownerId, restaurantId, floor, steetNumber, route, locality, country, zipCode, latitude, longitude, administrativeAreaLevel1, administrativeAreaLevel2, googleLink);
		}

		public Task<PhoneContacts> AddContactNumberAsync(long ownerId, long restaurantId, string phoneNumber, string phoneDescription)
		{
			return ContactService.AddContactNumberAsync(ownerId, restaurantId, phoneNumber, phoneDescription);
		}

		public Task<EmployersRestaurants> AddCoownerAsync(long ownerId, long restaurantId, long coownerId)
		{
			return RestaurantService.AddCoownerAsync(ownerId, restaurantId, coownerId);
		}

		public Task<Employees> AddEmployeeToRestaurantAsync(long ownerId, long restaurantId, long employeeId)
		{
			return RestaurantEmployeeService.AddEmployeeToRestaurantAsync(ownerId, restaurantId, employeeId);
		}

		public Task<MenuCategories> AddMenuCategoryAsync(long ownerId, long restaurantId, Dictionary<long, string> categoryName, Dictionary<long, string> categoryDescription)
		{
			return MenuService.AddMenuCategoryAsync(ownerId, restaurantId, categoryName, categoryDescription);
		}

		public Task<MenuCurrencies> AddMenuCurrencyAsync(long ownerId, long restaurantId, long currencyId)
		{
			return MenuService.AddMenuCurrencyAsync(ownerId, restaurantId, currencyId);
		}

		public Task<MenuItems> AddMenuItemAsync(long ownerId, long restaurantId, long menuCategoryId, Dictionary<long, string> itemName, Dictionary<long, string> itemDescription, Dictionary<long, string> itemWarnings, Dictionary<long, float> itemPrice)
		{
			return MenuService.AddMenuItemAsync(ownerId, restaurantId, menuCategoryId, itemName, itemDescription, itemWarnings, itemPrice);
		}

		public Task<MenuLanguages> AddMenuLanguageAsync(long ownerId, long restaurantId, long languageId)
		{
			return MenuService.AddMenuLanguageAsync(ownerId, restaurantId, languageId);
		}

		public Task<Tuple<RestaurantObjects, EmployersRestaurants>> AddNewRestaurantAsync(long ownerId, string restaurantName, string restaurantDescription)
		{
			return RestaurantService.AddNewRestaurantAsync(ownerId, restaurantName, restaurantDescription);
		}

		public Task<OutOfSchedulePeriods> AddOutOfScheduleIntervalAsync(long employerId, long restaurantId, long openScheduleId, DateTimeOffset startOn, DateTimeOffset endsOn, string description)
		{
			return ScheduleSerice.AddOutOfScheduleIntervalAsync(employerId, restaurantId, openScheduleId, startOn, endsOn, description);
		}

		public Task<OpenHoursSchedule> AddWorkingIntervalAsync(long employerId, long restaurantId, DayOfWeek startDay, TimeSpan startTime, DayOfWeek endDay, TimeSpan endTime)
		{
			return ScheduleSerice.AddWorkingIntervalAsync(employerId, restaurantId, startDay, startTime, endDay, endTime);
		}

		public Task<List<AssignedEmployeeTypes>> AssignResponsibilitiesForEmployeeAsync(long ownerId, long restaurantId, long employeeId, List<EmployeeTypeEnum> responsibilities)
		{
			return RestaurantEmployeeService.AssignResponsibilitiesForEmployeeAsync(ownerId, restaurantId, employeeId, responsibilities);
		}

		public Task<bool> CloseRestaurantAsync(long ownerId, long restaurantId)
		{
			return RestaurantService.CloseRestaurantAsync(ownerId, restaurantId);
		}

		public Task<Employees> FireEmployeeOutAsync(long ownerId, long restaurantId, long employeeId)
		{
			return RestaurantEmployeeService.FireEmployeeOutAsync(ownerId, restaurantId, employeeId);
		}

		public Task<List<Currencies>> GetAllAvailableCurrenciesAsync()
		{
			return MenuService.GetAllAvailableCurrenciesAsync();
		}

		public Task<List<Languages>> GetAllAvailableLanguagesAsync()
		{
			return MenuService.GetAllAvailableLanguagesAsync();
		}

		public Task<List<PhoneContacts>> GetAllContactNumbersAsync(long restaurantId, int pageNumber, int pageSize)
		{
			return ContactService.GetAllContactNumbersAsync(restaurantId, pageNumber, pageSize);
		}

		public Task<List<MenuCategories>> GetAllMenuCategoriesAsync(long restaurantId, int pageNumber, int pageSize)
		{
			return MenuService.GetAllMenuCategoriesAsync(restaurantId, pageNumber, pageSize);
		}

		public Task<List<MenuItems>> GetAllMenuItemsPagedAsync(long restaurantId, long menuCategoryId, int pageNumber, int pageSize)
		{
			return MenuService.GetAllMenuItemsPagedAsync(restaurantId, menuCategoryId, pageNumber, pageSize);
		}

		public Task<List<OutOfSchedulePeriods>> GetAllOutOfScheduleIntervalsAsync(long restaurantId, int pageNumber, int pageSize)
		{
			return ScheduleSerice.GetAllOutOfScheduleIntervalsAsync(restaurantId, pageNumber, pageSize);
		}

		public Task<List<OpenHoursSchedule>> GetAllWorkingIntervalsAsync(long restaurantId)
		{
			return ScheduleSerice.GetAllWorkingIntervalsAsync(restaurantId);
		}

		public Task<LocationContact> GetContactAddressByRestaurantIdAsync(long restaurantId)
		{
			return ContactService.GetContactAddressByRestaurantIdAsync(restaurantId);
		}

		public Task<Employees> GetEmployeeDetailsAsync(long employeeId)
		{
			return RestaurantEmployeeService.GetEmployeeDetailsAsync(employeeId);
		}

		public Task<List<Employees>> GetEmployeeListForRestaurantAsync(long restaurantId, int pageNumber, int pageSize)
		{
			return RestaurantEmployeeService.GetEmployeeListForRestaurantAsync(restaurantId, pageNumber, pageSize);
		}

		public Task<List<AssignedEmployeeTypes>> GetEmployeeResponsibilitiesAsync(long employeeId)
		{
			return RestaurantEmployeeService.GetEmployeeResponsibilitiesAsync(employeeId);
		}

		public Task<List<EmployeeTypes>> GetEmployeeTypesEnumAsync()
		{
			return RestaurantEmployeeService.GetEmployeeTypesEnumAsync();
		}

		public Task<LocationContact> GetLocationContactAsync(long contactId)
		{
			return ContactService.GetLocationContactAsync(contactId);
		}

		public Task<MenuCategories> GetMenuCategoryAsync(long menuCategoriesId)
		{
			return MenuService.GetMenuCategoryAsync(menuCategoriesId);
		}

		public Task<List<MenuCurrencies>> GetMenuCurrenciesAsync(long restaurantId)
		{
			return MenuService.GetMenuCurrenciesAsync(restaurantId);
		}

		public Task<List<MenuCurrencies>> GetMenuCurrenciesPagedAsync(long restaurantId, int pageNumber, int pageSize)
		{
			return MenuService.GetMenuCurrenciesPagedAsync(restaurantId, pageNumber, pageSize);
		}

		public Task<MenuCurrencies> GetMenuCurrencyAsync(long menuCurrencyId)
		{
			return MenuService.GetMenuCurrencyAsync(menuCurrencyId);
		}

		public Task<MenuItems> GetMenuItemAsync(long menuItemId)
		{
			return MenuService.GetMenuItemAsync(menuItemId);
		}

		public Task<MenuLanguages> GetMenuLanguageAsync(long menuLanguageId)
		{
			return MenuService.GetMenuLanguageAsync(menuLanguageId);
		}

		public Task<List<MenuLanguages>> GetMenuLanguagesAsync(long restaurantId)
		{
			return MenuService.GetMenuLanguagesAsync(restaurantId);
		}

		public Task<List<MenuLanguages>> GetMenuLanguagesPagedAsync(long restaurantId, int pageNumber, int pageSize)
		{
			return MenuService.GetMenuLanguagesPagedAsync(restaurantId, pageNumber, pageSize);
		}

		public Task<List<RestaurantObjects>> GetOwnerRestaurantsAsync(long ownerId, int pageNumber, int pageSize)
		{
			return RestaurantService.GetOwnerRestaurantsAsync(ownerId, pageNumber, pageSize);
		}

		public Task<PhoneContacts> GetPhoneContactAsync(long contactId)
		{
			return ContactService.GetPhoneContactAsync(contactId);
		}

		public Task<RestaurantObjects> GetRestaurantAsync(long id)
		{
			return RestaurantService.GetRestaurantAsync(id);
		}

		public Task<List<EmployersRestaurants>> GetRestaurantOwnersAsync(long restaurantId, int pageNumber, int pageSize)
		{
			return RestaurantService.GetRestaurantOwnersAsync(restaurantId, pageNumber, pageSize);
		}

		public Task<OpenHoursSchedule> GetWorkTimeByIdAsync(long id)
		{
			return ScheduleSerice.GetWorkTimeByIdAsync(id);
		}

		public Task<bool> RemoveContactAddressAsync(long ownerId, long restaurantId, long contactId)
		{
			return ContactService.RemoveContactAddressAsync(ownerId, restaurantId, contactId);
		}

		public Task<bool> RemoveContactNumberAsync(long ownerId, long restaurantId, long contactId)
		{
			return ContactService.RemoveContactNumberAsync(ownerId, restaurantId, contactId);
		}

		public Task<bool> RemoveCoownerAsync(long ownerId, long restaurantId, long coownerId)
		{
			return RestaurantService.RemoveCoownerAsync(ownerId, restaurantId, coownerId);
		}

		public Task<bool> RemoveMenuCategoryAsync(long ownerId, long restaurantId, long menuCategoryId)
		{
			return MenuService.RemoveMenuCategoryAsync(ownerId, restaurantId, menuCategoryId);
		}

		public Task<bool> RemoveMenuCurrencyAsync(long ownerId, long restaurantId, long menuCurrencyId)
		{
			return MenuService.RemoveMenuCurrencyAsync(ownerId, restaurantId, menuCurrencyId);
		}

		public Task<bool> RemoveMenuItemAsync(long ownerId, long restaurantId, long menuItemId)
		{
			return MenuService.RemoveMenuItemAsync(ownerId, restaurantId, menuItemId);
		}

		public Task<bool> RemoveMenuLanguageAsync(long ownerId, long restaurantId, long menuLanguageId)
		{
			return MenuService.RemoveMenuLanguageAsync(ownerId, restaurantId, menuLanguageId);
		}

		public Task<bool> RemoveOutOfScheduleIntervalAsync(long employerId, long restaurantId, long scheduleId)
		{
			return ScheduleSerice.RemoveOutOfScheduleIntervalAsync(employerId, restaurantId, scheduleId);
		}

		public Task<bool> RemoveWorkingIntervalAsync(long employerId, long restaurantId, long scheduleId)
		{
			return ScheduleSerice.RemoveWorkingIntervalAsync(employerId, restaurantId, scheduleId);
		}

		public Task<Employees> TransferEmployeeToAnotherRestaurantAsync(long ownerId, long restaurantId, long employeeId, long newRestaurantId)
		{
			return RestaurantEmployeeService.TransferEmployeeToAnotherRestaurantAsync(ownerId, restaurantId, employeeId, newRestaurantId);
		}

		public Task<bool> TransferOwnershipAsync(long ownerId, long restaurantId, long newOwnerId)
		{
			return RestaurantService.TransferOwnershipAsync(ownerId, restaurantId, newOwnerId);
		}

		public Task<LocationContact> UpdateContactAddressAsync(long ownerId, long restaurantId, long contactId, int floor, string steetNumber, string route, string locality, string country, int zipCode, float latitude, float longitude, [Optional] string administrativeAreaLevel1, [Optional] string administrativeAreaLevel2, [Optional] string googleLink)
		{
			return ContactService.UpdateContactAddressAsync(ownerId, restaurantId, contactId, floor, steetNumber, route, locality, country, zipCode, latitude, longitude, administrativeAreaLevel1, administrativeAreaLevel2, googleLink);
		}

		public Task<PhoneContacts> UpdateContactNumberAsync(long ownerId, long restaurantId, long contactId, string phoneNumber, string phoneDescription)
		{
			return ContactService.UpdateContactNumberAsync(ownerId, restaurantId, contactId, phoneNumber, phoneDescription);
		}

		public Task<MenuCategories> UpdateMenuCategoryAsync(long ownerId, long restaurantId, long menuCategoryId, Dictionary<long, string> categoryName, Dictionary<long, string> categoryDescription)
		{
			return MenuService.UpdateMenuCategoryAsync(ownerId, restaurantId, menuCategoryId, categoryName, categoryDescription);
		}

		public Task<MenuCurrencies> UpdateMenuCurrenciesAsync(long ownerId, long restaurantId, long menuCurrencyId, long newCurrencyId)
		{
			return MenuService.UpdateMenuCurrenciesAsync(ownerId, restaurantId, menuCurrencyId, newCurrencyId);
		}

		public Task<MenuItems> UpdateMenuItemAsync(long ownerId, long restaurantId, long menuItemId, Dictionary<long, string> itemName, Dictionary<long, string> itemDescription, Dictionary<long, string> itemWarnings, Dictionary<long, float> itemPrice)
		{
			return MenuService.UpdateMenuItemAsync(ownerId, restaurantId, menuItemId, itemName, itemDescription, itemWarnings, itemPrice);
		}

		public Task<MenuLanguages> UpdateMenuLanguageAsync(long ownerId, long restaurantId, long menuLanguageId, long newLanguageId)
		{
			return MenuService.UpdateMenuLanguageAsync(ownerId, restaurantId, menuLanguageId, newLanguageId);
		}

		public Task<OutOfSchedulePeriods> UpdateOutOfScheduleIntervalsAsync(long employerId, long restaurantId, long scheduleId, DateTimeOffset startOn, DateTimeOffset endsOn, string description)
		{
			return ScheduleSerice.UpdateOutOfScheduleIntervalsAsync(employerId, restaurantId, scheduleId, startOn, endsOn, description);
		}

		public Task<RestaurantObjects> UpdateRestaurantAsync(long ownerId, long restaurantId, string restaurantName, string restaurantDescription)
		{
			return RestaurantService.UpdateRestaurantAsync(ownerId, restaurantId, restaurantName, restaurantDescription);
		}

		public Task<OpenHoursSchedule> UpdateWokingIntervalAsync(long employerId, long restaurantId, long scheduleId, DayOfWeek startDay, TimeSpan startTime, DayOfWeek endDay, TimeSpan endTime)
		{
			return ScheduleSerice.UpdateWokingIntervalAsync(employerId, restaurantId, scheduleId, startDay, startTime, endDay, endTime);
		}
	}
}
