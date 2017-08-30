using Restaurants.API.Persistence.Implementation;
using Restaurants.API.Services.Helpers;
using System;
using System.Collections.Generic;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System.Threading.Tasks;

namespace Restaurants.API.Services.Implementation
{
	public class MenuService : BaseService, IMenuService
	{
		MenusRepository MenusRepo;

		LanguagesRepository LanguageRepo;
		MenuLanguagesRepository MenuLanguagesRepo;

		CurrenciesRepository CurrencyRepo;
		MenuCurrenciesRepository MenuCurrencyRepo;

		public MenuService(AppDbContext dbContext, People logedInPerson)
			: base(dbContext, logedInPerson)
		{
			this.MenusRepo = new MenusRepository(dbContext);

			this.LanguageRepo = new LanguagesRepository(dbContext);
			this.MenuLanguagesRepo = new MenuLanguagesRepository(dbContext);

			this.CurrencyRepo = new CurrenciesRepository(dbContext);
			this.MenuCurrencyRepo = new MenuCurrenciesRepository(dbContext);
		}

		#region MenuLanguages

		public async Task<MenuLanguages> AddMenuLanguageAsync(long ownerId, long restaurantId, long languageId)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			Menus currentMenu = await CheckMenuExistanceAsync(restaurantId);
			Languages languageToAdd = await CheckLanguageExistance(languageId);

			CheckTheLoggedInPerson();
			MenuLanguages item = new MenuLanguages { MenuId = currentMenu.Id, LanguageId = languageToAdd.Id };
			await MenuLanguagesRepo.AddAsync(item, this.ModifierId);

			return item;
		}

		public async Task<List<Languages>> GetAllAvailableLanguagesAsync()
		{
			return await LanguageRepo.GetLanguaseList();
		}

		public async Task<MenuLanguages> GetMenuLanguageAsync(long menuLanguageId)
		{
			return await MenuLanguagesRepo.FindById(menuLanguageId);
		}

		public async Task<List<MenuLanguages>> GetMenuLanguagesAsync(long restaurantId)
		{
			Menus menu = await CheckMenuExistanceAsync(restaurantId);
			return await MenuLanguagesRepo.GetItemsByMenuId(menu.Id);
		}

		public async Task<List<MenuLanguages>> GetMenuLanguagesPagedAsync(long restaurantId, int pageNumber, int pageSize)
		{
			Menus menu = await CheckMenuExistanceAsync(restaurantId);
			return await MenuLanguagesRepo.GetItemsByMenuIdPaged(menu.Id, pageNumber, pageSize);
		}

		public bool RemoveMenuLanguage(long ownerId, long restaurantId, long menuLanguageId)
		{
			//TODO: remove categories, menuItemContent and menulanguages corresponding to given menuLanguage
			throw new NotImplementedException();
		}

		public async Task<MenuLanguages> UpdateMenuLanguageAsync(long ownerId, long restaurantId, long menuLanguageId, long newLanguageId)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			MenuLanguages menuLanguage = await CheckMenuLanguageExistence(menuLanguageId);
			Languages newLanguage = await CheckLanguageExistance(newLanguageId);

			CheckTheLoggedInPerson();
			menuLanguage.LanguageId = newLanguage.Id;
			menuLanguage.TheLanguage = newLanguage;
			await MenuLanguagesRepo.UpdateAsync(menuLanguage, this.ModifierId);

			return menuLanguage;
		}
		#endregion

		#region MenuCurrencies

		public async Task<List<Currencies>> GetAllAvailableCurrenciesAsync()
		{
			return await CurrencyRepo.GetCurrencyList();
		}

		public async Task<MenuCurrencies> AddMenuCurrencyAsync(long ownerId, long restaurantId, long currencyId)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			Menus currentMenu = await CheckMenuExistanceAsync(restaurantId);
			Currencies currencyToAdd = await CheckCurrencyExistance(currencyId);

			CheckTheLoggedInPerson();
			MenuCurrencies item = new MenuCurrencies { MenuId = currentMenu.Id, CurrencyId = currencyToAdd.Id };
			await MenuCurrencyRepo.AddAsync(item, this.ModifierId);

			return item;
		}

		public async Task<MenuCurrencies> GetMenuCurrencyAsync(long menuCurrencyId)
		{
			return await MenuCurrencyRepo.FindById(menuCurrencyId);
		}

		public async Task<List<MenuCurrencies>> GetMenuCurrenciesAsync(long restaurantId)
		{
			Menus menu = await CheckMenuExistanceAsync(restaurantId);
			return await MenuCurrencyRepo.GetItemsByMenuId(menu.Id);
		}

		public async Task<List<MenuCurrencies>> GetMenuCurrenciesPagedAsync(long restaurantId, int pageNumber, int pageSize)
		{
			Menus menu = await CheckMenuExistanceAsync(restaurantId);
			return await MenuCurrencyRepo.GetItemsByMenuIdPaged(menu.Id, pageNumber, pageSize);
		}

		public async Task<MenuCurrencies> UpdateMenuCurrenciesAsync(long ownerId, long restaurantId, long menuCurrencyId, long newCurrencyId)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			MenuCurrencies menuCurrency = await CheckMenuCurrencyExistence(menuCurrencyId);
			Currencies newCurrency = await CheckCurrencyExistance(newCurrencyId);

			CheckTheLoggedInPerson();
			menuCurrency.CurrencyId = newCurrency.Id;
			menuCurrency.TheCurrency = newCurrency;
			await MenuCurrencyRepo.UpdateAsync(menuCurrency, this.ModifierId);

			return menuCurrency;
		}

		public bool RemoveMenuCurrency(long ownerId, long restaurantId, long menuLanguageId)
		{
			//TODO: remove categories, menuItemContent and menuCurrencies corresponding to given menuLanguage
			throw new NotImplementedException();
		}
		#endregion

		#region Private functions

		private async Task<Menus> CheckMenuExistanceAsync(long restaurantId)
		{
			Menus menu = await MenusRepo.GetMenuByRestaurantId(restaurantId);
			if (menu == null)
			{
				RestaurantObjects currentRestaurant = await CheckRestaurantExistenceAsync(restaurantId);
				menu = new Menus { RestaurantId = currentRestaurant.Id };
				await MenusRepo.AddAsync(menu, this.ModifierId);
			}
			return menu;
		}

		private async Task<Languages> CheckLanguageExistance(long languageId)
		{
			Languages language = await LanguageRepo.GetLanguageById(languageId);
			if (language == null)
				throw new Exception("Invalid language!");
			return language;
		}

		private async Task<MenuLanguages> CheckMenuLanguageExistence(long menuLanguageId)
		{
			MenuLanguages menuLanguage = await MenuLanguagesRepo.FindById(menuLanguageId);
			if (menuLanguage == null)
				throw new Exception("Non existing entry");
			return menuLanguage;
		}

		private async Task<Currencies> CheckCurrencyExistance(long currencyId)
		{
			Currencies currency = await CurrencyRepo.GetCurrencyById(currencyId);
			if (currency == null)
				throw new Exception("Invalid currency!");
			return currency;
		}

		private async Task<MenuCurrencies> CheckMenuCurrencyExistence(long menuCurrencyId)
		{
			MenuCurrencies menuCurrency = await MenuCurrencyRepo.FindById(menuCurrencyId);
			if (menuCurrency == null)
				throw new Exception("Non existing entry");
			return menuCurrency;
		}

		#endregion
	}
}
