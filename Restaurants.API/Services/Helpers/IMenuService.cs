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

		Task<List<Currencies>> GetAllAvailableCurrenciesAsync();

		Task<MenuCurrencies> AddMenuCurrencyAsync(
			long ownerId,
			long restaurantId,
			long currencyId
		);

		Task<MenuCurrencies> GetMenuCurrencyAsync(
			long menuCurrencyId
		);

		Task<List<MenuCurrencies>> GetMenuCurrenciesAsync(
			long restaurantId
		);

		Task<List<MenuCurrencies>> GetMenuCurrenciesPagedAsync(
			long restaurantId,
			int pageNumber,
			int pageSize
		);

		Task<MenuCurrencies> UpdateMenuCurrenciesAsync(
			long ownerId,
			long restaurantId,
			long menuCurrencyId,
			long newCurrencyId
		);

		bool RemoveMenuCurrency(
			long ownerId,
			long restaurantId,
			long menuCurrencyId
		);

		Task<MenuCategories> AddMenuCategoryAsync(
			long ownerId,
			long restaurantId,
			Dictionary<long, string> categoryName,
			Dictionary<long, string> categoryDescription
		);

		Task<MenuCategories> GetMenuCategoryAsync(
			long menuCategoriesId
		);

		Task<List<MenuCategories>> GetAllMenuCategoriesAsync(
			long restaurantId,
			int pageNumber,
			int pageSize
		);

		Task<MenuCategories> UpdateMenuCategoryAsync(
			long ownerId,
			long restaurantId,
			long menuCategoryId,
			Dictionary<long, string> categoryName,
			Dictionary<long, string> categoryDescription
		);

		bool RemoveMenuCategory(
			long ownerId,
			long restaurantId,
			long menuCategoryId
		);
	}
}
