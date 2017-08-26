using Restaurants.API.Models.EntityFramework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurants.API.Services.Helpers
{
	interface IMenuService
    {
		Task<List<Languages>> GetAllAvailableLanguagesAsync();

		Task<MenuLanguages> AddMenuLanguageAsync(
			long ownerId,
			long restaurantId,
			long languageId
		);

		Task<MenuLanguages> GetMenuLanguageAsync(
			long menuLanguageId
		);

		Task<List<MenuLanguages>> GetMenuLanguagesAsync(
			long restaurantId
		);

		Task<List<MenuLanguages>> GetMenuLanguagesPagedAsync(
			long restaurantId,
			int pageNumber,
			int pageSize
		);

		Task<MenuLanguages> UpdateMenuLanguageAsync(
			long ownerId,
			long restaurantId,
			long menuLanguageId,
			long newLanguageId
		);

		bool RemoveMenuLanguage(
			long ownerId,
			long restaurantId,
			long menuLanguageId
		);
    }
}
