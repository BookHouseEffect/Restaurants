using System.Runtime.InteropServices;
using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurants.API.Services.Helpers
{
	interface IContactSerivce
	{
		Task<PhoneContacts> AddContactNumberAsync(
			long ownerId,
			long restaurantId,
			string phoneNumber,
			string phoneDescription
		);

		Task<PhoneContacts> GetPhoneContactAsync(
			long contactId
		);

		Task<List<PhoneContacts>> GetAllContactNumbersAsync(
			long restaurantId,
			int pageNumber,
			int pageSize
		);

		Task<PhoneContacts> UpdateContactNumberAsync(
			long ownerId,
			long restaurantId,
			long contactId,
			string phoneNumber,
			string phoneDescription
		);

		Task<bool> RemoveContactNumberAsync(
			long ownerId,
			long restaurantId,
			long contactId
		);

		Task<LocationContact> AddContactAddressAsync(
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

		Task<LocationContact> GetLocationContactAsync(
			long contactId
		);

		Task<LocationContact> GetContactAddressByRestaurantIdAsync(
			long restaurantId
		);

		Task<LocationContact> UpdateContactAddressAsync(
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

		Task<bool> RemoveContactAddressAsync(
			long ownerId,
			long restaurantId,
			long contactId
		);
	}
}


