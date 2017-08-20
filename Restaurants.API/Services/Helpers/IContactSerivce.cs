using System.Runtime.InteropServices;
using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;

namespace Restaurants.API.Services.Helpers
{
	interface IContactSerivce
	{
		PhoneContacts AddContactNumber(
			long ownerId,
			long restaurantId,
			string phoneNumber,
			string phoneDescription
		);

		PhoneContacts GetPhoneContact(
			long contactId
		);

		List<PhoneContacts> GetAllContactNumbers(
			long restaurantId,
			int pageNumber,
			int pageSize
			);

		PhoneContacts UpdateContactNumber(
			long ownerId,
			long restaurantId,
			long contactId,
			string phoneNumber,
			string phoneDescription
			);

		bool RemoveContactNumber(
			long ownerId,
			long restaurantId,
			long contactId
			);

		LocationContact AddContactAddress(
			long ownerId,
			long restaurantId,
			int floor,
			string steetNumber,
			string route,
			string locality,
			string country,
			int zipCode,
			float latitude,
			float longitude,
			[Optional] string administrativeAreaLevel1,
			[Optional] string administrativeAreaLevel2,
			[Optional] string googleLink
			);

		LocationContact GetLocationContact(
			long contactId
		);

		List<LocationContact> GetAllContactAddresses(
			long restaurantId,
			int pageNumber,
			int pageSize
			);

		LocationContact UpdateContactAddress(
			long ownerId,
			long restaurantId,
			long contactId,
			int floor,
			string steetNumber,
			string route,
			string locality,
			string country,
			int zipCode,
			float latitude,
			float longitude,
			[Optional] string administrativeAreaLevel1,
			[Optional] string administrativeAreaLevel2,
			[Optional] string googleLink
			);

		bool RemoveContactAddress(
			long ownerId,
			long restaurantId,
			long contactId
			);
	}
}


