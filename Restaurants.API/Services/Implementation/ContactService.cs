using Restaurants.API.Persistence.Implementation;
using Restaurants.API.Services.Helpers;
using System;
using System.Collections.Generic;
using Restaurants.API.Models.EntityFramework;
using System.Runtime.InteropServices;
using Restaurants.API.Models.Context;

namespace Restaurants.API.Services.Implementation
{
	public class ContactService : BaseService, IContactSerivce
	{
		private PhoneRepository PhoneRepo;
		private LocationRepository LocationRepo;
		private LocationPointRepository PointRepo;

		public ContactService(AppDbContext dbContext, People logedInPerson)
			: base(dbContext, logedInPerson)
		{
			this.PhoneRepo = new PhoneRepository(dbContext);
			this.LocationRepo = new LocationRepository(dbContext);
			this.PointRepo = new LocationPointRepository(dbContext);
		}

		public LocationContact AddContactAddress(long ownerId, long restaurantId, int floor, string steetNumber, string route, string locality, string country, int zipCode, float latitude, float longitude, [Optional] string administrativeAreaLevel1, [Optional] string administrativeAreaLevel2, [Optional] string googleLink)
		{
			EmployersRestaurants connection = CheckEmployerRestaurant(ownerId, restaurantId);

			CheckTheLoggedInPerson();
			LocationPoints point = new LocationPoints
			{
				Latitude = latitude,
				Longitude = longitude
			};
			PointRepo.Add(point, ModifierId);

			LocationContact contact = new LocationContact() { LocationPointId = point.Id };
			contact.FillOrUpdateFields(floor, steetNumber, route, locality, country, zipCode, administrativeAreaLevel1, administrativeAreaLevel2, googleLink);
			LocationRepo.Add(contact, ModifierId);

			return contact;
		}

		public PhoneContacts AddContactNumber(long ownerId, long restaurantId, string phoneNumber, string phoneDescription)
		{
			EmployersRestaurants connection = CheckEmployerRestaurant(ownerId, restaurantId);

			PhoneContacts phone = new PhoneContacts
			{
				PhoneNumber = phoneNumber,
				PhoneDescription = phoneDescription,
				RestaurantId = connection.RestaurantId
			};

			CheckTheLoggedInPerson();
			PhoneRepo.Add(phone, ModifierId);

			return phone;
		}

		public List<LocationContact> GetAllContactAddresses(long restaurantId, int pageNumber, int pageSize)
		{
			return LocationRepo.GetLocationsByRestaurantPaged(restaurantId, pageNumber, pageSize);
		}

		public List<PhoneContacts> GetAllContactNumbers(long restaurantId, int pageNumber, int pageSize)
		{
			return PhoneRepo.GetPhoneNumbersByRestaurantPaged(restaurantId, pageNumber, pageSize);
		}

		public bool RemoveContactAddress(long ownerId, long restaurantId, long contactId)
		{
			EmployersRestaurants connection = CheckEmployerRestaurant(ownerId, restaurantId);
			LocationContact loc = CheckLocationExistence(contactId);
			LocationPoints point = loc.TheLocationPoint;

			CheckTheLoggedInPerson();
			LocationRepo.Remove(loc);
			PointRepo.Remove(point);

			return true;
		}

		public bool RemoveContactNumber(long ownerId, long restaurantId, long contactId)
		{
			EmployersRestaurants connection = CheckEmployerRestaurant(ownerId, restaurantId);
			PhoneContacts phone = CheckPhoneExistence(contactId);

			CheckTheLoggedInPerson();
			PhoneRepo.Remove(phone);

			return true;
		}

		public LocationContact UpdateContactAddress(long ownerId, long restaurantId, long contactId, int floor, string steetNumber, string route, string locality, string country, int zipCode, float latitude, float longitude, [Optional] string administrativeAreaLevel1, [Optional] string administrativeAreaLevel2, [Optional] string googleLink)
		{
			EmployersRestaurants connection = CheckEmployerRestaurant(ownerId, restaurantId);
			LocationContact contact = CheckLocationExistence(contactId);

			CheckTheLoggedInPerson();
			LocationPoints point = contact.TheLocationPoint;
			point.Latitude = latitude;
			point.Longitude = longitude;
			PointRepo.Update(point, ModifierId);

			contact.FillOrUpdateFields(floor, steetNumber, route, locality, country, zipCode, administrativeAreaLevel1, administrativeAreaLevel2, googleLink);
			LocationRepo.Update(contact, ModifierId);

			return contact;
		}

		public PhoneContacts UpdateContactNumber(long ownerId, long restaurantId, long contactId, string phoneNumber, string phoneDescription)
		{
			EmployersRestaurants connection = CheckEmployerRestaurant(ownerId, restaurantId);
			PhoneContacts phone = CheckPhoneExistence(contactId);

			CheckTheLoggedInPerson();
			phone.PhoneNumber = phoneNumber;
			phone.PhoneDescription = phoneDescription;
			PhoneRepo.Update(phone, ModifierId);

			return phone;
		}

		public PhoneContacts GetPhoneContact(long contactId)
		{
			return CheckPhoneExistence(contactId);
		}

		public LocationContact GetLocationContact(long contactId)
		{
			return CheckLocationExistence(contactId);
		}

		private PhoneContacts CheckPhoneExistence(long contactId)
		{
			PhoneContacts phone = PhoneRepo.FindById(contactId);
			if (phone == null)
				throw new Exception(String.Format("There is no phone record with id {0}", contactId));

			return phone;
		}

		private LocationContact CheckLocationExistence(long contactId)
		{
			LocationContact location = LocationRepo.FindById(contactId);
			if (location == null)
				throw new Exception(String.Format("There is no location record with id {0}", contactId));

			return location;
		}
	}
}
