using Restaurants.API.Services.Helpers;
using Restaurants.API.Services.Implementation;
using System;
using System.Collections.Generic;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System.Runtime.InteropServices;

namespace Restaurants.API.Services
{
	public class ApplicationServices : 
	BaseService,
	IRestaurantService, 
	IContactSerivce
    {
		private IRestaurantService RestaurantService;
		private IContactSerivce ContactService;

		public ApplicationServices(AppDbContext dbContext, People logedInPerson) 
			: base(dbContext, logedInPerson) {
			this.RestaurantService = new RestaurantService(dbContext, logedInPerson);
			this.ContactService = new ContactService(dbContext, logedInPerson);
		}

		public LocationContact AddContactAddress(long ownerId, long restaurantId, int floor, string steetNumber, string route, string locality, string country, int zipCode, float latitude, float longitude, [Optional] string administrativeAreaLevel1, [Optional] string administrativeAreaLevel2, [Optional] string googleLink)
		{
			return ContactService.AddContactAddress(ownerId, restaurantId, floor, steetNumber, route, locality, country, zipCode, latitude, longitude, administrativeAreaLevel1, administrativeAreaLevel2, googleLink);
		}

		public PhoneContacts AddContactNumber(long ownerId, long restaurantId, string phoneNumber, string phoneDescription)
		{
			return ContactService.AddContactNumber(ownerId, restaurantId, phoneNumber, phoneDescription);
		}

		public EmployersRestaurants AddCoowner(long ownerId, long restaurantId, long coownerId)
		{
			return RestaurantService.AddCoowner(ownerId, restaurantId, coownerId);
		}

		public Tuple<RestaurantObjects, EmployersRestaurants> AddNewRestaurant(long ownerId, string restaurantName, string restaurantDescription)
		{
			return RestaurantService.AddNewRestaurant(ownerId, restaurantName, restaurantDescription);
		}

		public bool CloseRestaurant(long ownerId, long restaurantId)
		{
			return RestaurantService.CloseRestaurant(ownerId, restaurantId);
		}

		public List<PhoneContacts> GetAllContactNumbers(long restaurantId, int pageNumber, int pageSize)
		{
			return ContactService.GetAllContactNumbers(restaurantId, pageNumber, pageSize);
		}

		public LocationContact GetContactAddressByRestaurantId(long restaurantId)
		{
			return ContactService.GetContactAddressByRestaurantId(restaurantId);
		}

		public LocationContact GetLocationContact(long contactId)
		{
			return ContactService.GetLocationContact(contactId);
		}

		public List<RestaurantObjects> GetOwnerRestaurants(long ownerId, int pageNumber, int pageSize)
		{
			return RestaurantService.GetOwnerRestaurants(ownerId, pageNumber, pageSize);
		}

		public PhoneContacts GetPhoneContact(long contactId)
		{
			return ContactService.GetPhoneContact(contactId);
		}

		public RestaurantObjects GetRestaurant(long id)
		{
			return RestaurantService.GetRestaurant(id);
		}

		public List<EmployersRestaurants> GetRestaurantOwners(long restaurantId, int pageNumber, int pageSize)
		{
			return RestaurantService.GetRestaurantOwners(restaurantId, pageNumber, pageSize);
		}

		public bool RemoveContactAddress(long ownerId, long restaurantId, long contactId)
		{
			return ContactService.RemoveContactAddress(ownerId, restaurantId, contactId);
		}

		public bool RemoveContactNumber(long ownerId, long restaurantId, long contactId)
		{
			return ContactService.RemoveContactNumber(ownerId, restaurantId, contactId);
		}

		public bool RemoveCoowner(long ownerId, long restaurantId, long coownerId)
		{
			return RestaurantService.RemoveCoowner(ownerId, restaurantId, coownerId);
		}

		public bool TransferOwnership(long ownerId, long restaurantId, long newOwnerId)
		{
			return RestaurantService.TransferOwnership(ownerId, restaurantId, newOwnerId);
		}

		public LocationContact UpdateContactAddress(long ownerId, long restaurantId, long contactId, int floor, string steetNumber, string route, string locality, string country, int zipCode, float latitude, float longitude, [Optional] string administrativeAreaLevel1, [Optional] string administrativeAreaLevel2, [Optional] string googleLink)
		{
			return ContactService.UpdateContactAddress(ownerId, restaurantId, contactId, floor, steetNumber, route, locality, country, zipCode, latitude, longitude, administrativeAreaLevel1, administrativeAreaLevel2, googleLink);
		}

		public PhoneContacts UpdateContactNumber(long ownerId, long restaurantId, long contactId, string phoneNumber, string phoneDescription)
		{
			return ContactService.UpdateContactNumber(ownerId, restaurantId, contactId, phoneNumber, phoneDescription);
		}

		public RestaurantObjects UpdateRestaurant(long ownerId, long restaurantId, string restaurantName, string restaurantDescription)
		{
			return RestaurantService.UpdateRestaurant(ownerId, restaurantId, restaurantName, restaurantDescription);
		}
	}
}
