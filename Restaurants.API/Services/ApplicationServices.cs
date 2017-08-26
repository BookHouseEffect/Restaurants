using Restaurants.API.Services.Helpers;
using Restaurants.API.Services.Implementation;
using System;
using System.Collections.Generic;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Restaurants.API.Services
{
	public class ApplicationServices : 
	BaseService,
	IRestaurantService, 
	IContactSerivce,
	IScheduleService,
	IMenuService
    {
		private IRestaurantService RestaurantService;
		private IContactSerivce ContactService;
		private IScheduleService ScheduleSerice;
		private IMenuService MenuService;

		public ApplicationServices(AppDbContext dbContext, People logedInPerson) 
			: base(dbContext, logedInPerson) {
			this.RestaurantService = new RestaurantService(dbContext, logedInPerson);
			this.ContactService = new ContactService(dbContext, logedInPerson);
			this.ScheduleSerice = new ScheduleService(dbContext, logedInPerson);
			this.MenuService = new MenuService(dbContext, logedInPerson);
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

		public Task<bool> CloseRestaurantAsync(long ownerId, long restaurantId)
		{
			return RestaurantService.CloseRestaurantAsync(ownerId, restaurantId);
		}

		public Task<List<Languages>> GetAllAvailableLanguagesAsync()
		{
			return MenuService.GetAllAvailableLanguagesAsync();
		}

		public Task<List<PhoneContacts>> GetAllContactNumbersAsync(long restaurantId, int pageNumber, int pageSize)
		{
			return ContactService.GetAllContactNumbersAsync(restaurantId, pageNumber, pageSize);
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

		public Task<LocationContact> GetLocationContactAsync(long contactId)
		{
			return ContactService.GetLocationContactAsync(contactId);
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

		public bool RemoveMenuLanguage(long ownerId, long restaurantId, long menuLanguageId)
		{
			return MenuService.RemoveMenuLanguage(ownerId, restaurantId, menuLanguageId);
		}

		public Task<bool> RemoveOutOfScheduleIntervalAsync(long employerId, long restaurantId, long scheduleId)
		{
			return ScheduleSerice.RemoveOutOfScheduleIntervalAsync(employerId, restaurantId, scheduleId);
		}

		public Task<bool> RemoveWorkingIntervalAsync(long employerId, long restaurantId, long scheduleId)
		{
			return ScheduleSerice.RemoveWorkingIntervalAsync(employerId, restaurantId, scheduleId);
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
