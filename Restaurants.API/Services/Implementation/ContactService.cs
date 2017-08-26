using Restaurants.API.Persistence.Implementation;
using Restaurants.API.Services.Helpers;
using System;
using System.Collections.Generic;
using Restaurants.API.Models.EntityFramework;
using System.Runtime.InteropServices;
using Restaurants.API.Models.Context;
using System.Threading.Tasks;

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

		public async Task<LocationContact> AddContactAddressAsync(long ownerId, long restaurantId, int floor, string steetNumber, string route, string locality, string country, int zipCode, float latitude, float longitude, [Optional] string administrativeAreaLevel1, [Optional] string administrativeAreaLevel2, [Optional] string googleLink)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);

			CheckTheLoggedInPerson();
			LocationPoints point = new LocationPoints
			{
				Latitude = latitude,
				Longitude = longitude
			};
			PointRepo.Add(point, ModifierId);

			LocationContact contact = new LocationContact() { LocationPointId = point.Id, RestaurantId = connection.TheRestaurant.Id };
			contact.FillOrUpdateFields(floor, steetNumber, route, locality, country, zipCode, administrativeAreaLevel1, administrativeAreaLevel2, googleLink);
			try
			{
				LocationRepo.Add(contact, ModifierId);
			}
			catch (Exception ex)
			{
				if (contact.Id <= 0)
				{
					contact = null;
					PointRepo.Remove(point);
				}

				throw ex;
			}

			return contact;
		}

		public async Task<PhoneContacts> AddContactNumberAsync(long ownerId, long restaurantId, string phoneNumber, string phoneDescription)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);

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

		public async Task<LocationContact> GetContactAddressByRestaurantIdAsync(long restaurantId)
		{
			return await LocationRepo.GetLocationsByRestaurantId(restaurantId);
		}

		public async Task<List<PhoneContacts>> GetAllContactNumbersAsync(long restaurantId, int pageNumber, int pageSize)
		{
			return await PhoneRepo.GetPhoneNumbersByRestaurantPaged(restaurantId, pageNumber, pageSize);
		}

		public async Task<bool> RemoveContactAddressAsync(long ownerId, long restaurantId, long contactId)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);

			LocationContact loc = await CheckLocationExistenceAsync(contactId);
			LocationPoints point = await CheckLocationPointExistenceAsync(loc.LocationPointId);

			CheckTheLoggedInPerson();
			PointRepo.Remove(point);
			LocationRepo.Remove(loc);

			return true;
		}

		public async Task<bool> RemoveContactNumberAsync(long ownerId, long restaurantId, long contactId)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);

			PhoneContacts phone = await CheckPhoneExistenceAsync(contactId);

			CheckTheLoggedInPerson();
			PhoneRepo.Remove(phone);

			return true;
		}

		public async Task<LocationContact> UpdateContactAddressAsync(long ownerId, long restaurantId, long contactId, int floor, string steetNumber, string route, string locality, string country, int zipCode, float latitude, float longitude, [Optional] string administrativeAreaLevel1, [Optional] string administrativeAreaLevel2, [Optional] string googleLink)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);

			LocationContact contact = await CheckLocationExistenceAsync(contactId);

			LocationPoints point = await CheckLocationPointExistenceAsync(contact.LocationPointId);
			point.Latitude = latitude;
			point.Longitude = longitude;

			CheckTheLoggedInPerson();
			PointRepo.Update(point, ModifierId);

			contact.FillOrUpdateFields(floor, steetNumber, route, locality, country, zipCode, administrativeAreaLevel1, administrativeAreaLevel2, googleLink);
			LocationRepo.Update(contact, ModifierId);

			return contact;
		}

		public async Task<PhoneContacts> UpdateContactNumberAsync(long ownerId, long restaurantId, long contactId, string phoneNumber, string phoneDescription)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			PhoneContacts phone = await CheckPhoneExistenceAsync(contactId);

			CheckTheLoggedInPerson();
			phone.PhoneNumber = phoneNumber;
			phone.PhoneDescription = phoneDescription;
			PhoneRepo.Update(phone, ModifierId);

			return phone;
		}

		public async Task<PhoneContacts> GetPhoneContactAsync(long contactId)
		{
			return await CheckPhoneExistenceAsync(contactId);
		}

		public async Task<LocationContact> GetLocationContactAsync(long contactId)
		{
			return await CheckLocationExistenceAsync(contactId);
		}

		private async Task<PhoneContacts> CheckPhoneExistenceAsync(long contactId)
		{
			PhoneContacts phone = await PhoneRepo.FindById(contactId);
			if (phone == null)
				throw new Exception(String.Format("There is no phone record with id {0}", contactId));

			return phone;
		}

		private async Task<LocationContact> CheckLocationExistenceAsync(long contactId)
		{
			LocationContact location = await LocationRepo.FindById(contactId);
			if (location == null)
				throw new Exception(String.Format("There is no location record with id {0}", contactId));

			return location;
		}

		private async Task<LocationPoints> CheckLocationPointExistenceAsync(long locationPointid)
		{
			LocationPoints point = await PointRepo.FindById(locationPointid);
			if (point == null)
				throw new Exception(String.Format("There is no location point record with id {0}", locationPointid));
			return point;
		}
	}
}
