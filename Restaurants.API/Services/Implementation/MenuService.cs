using System.Linq;
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

		CategoriesRepository CategoriesRepo;
		MenuCategoriesRepository MenuCategoriesRepo;

		public MenuService(AppDbContext dbContext, People logedInPerson)
			: base(dbContext, logedInPerson)
		{
			this.MenusRepo = new MenusRepository(dbContext);

			this.LanguageRepo = new LanguagesRepository(dbContext);
			this.MenuLanguagesRepo = new MenuLanguagesRepository(dbContext);

			this.CurrencyRepo = new CurrenciesRepository(dbContext);
			this.MenuCurrencyRepo = new MenuCurrenciesRepository(dbContext);

			this.CategoriesRepo = new CategoriesRepository(dbContext);
			this.MenuCategoriesRepo = new MenuCategoriesRepository(dbContext);
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
			//TODO: remove menuLanguages, categories, menuItemContents
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
			//TODO: remove menuCurrencies, menuItemValues
			throw new NotImplementedException();
		}
		#endregion

		#region MenuCategoriesRegion 

		public async Task<MenuCategories> AddMenuCategoryAsync(long ownerId, long restaurantId, Dictionary<long, string> categoryName, Dictionary<long, string> categoryDescription)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			Menus currentMenu = await CheckMenuExistanceAsync(restaurantId);
			List<MenuLanguages> menuLanguages = await MenuLanguagesRepo.GetItemsByMenuId(currentMenu.Id);

			MenuCategories menuCat = new MenuCategories();
			await MenuCategoriesRepo.AddAsync(menuCat, this.ModifierId);

			foreach (var menuLang in menuLanguages)
			{
				bool checkName = categoryName.TryGetValue(menuLang.Id, out string name);
				categoryDescription.TryGetValue(menuLang.Id, out string description);

				if (!checkName) 
					name = "<< no name >>";

				Categories cat = new Categories
				{
					CategoryName = name,
					CategoryDescription = description,
					MenuLanguageId = menuLang.Id,
					MenuCategoryId = menuCat.Id
				};

				await CategoriesRepo.AddAsync(cat, this.ModifierId);
			}
			
			return menuCat;
		}

		public async Task<MenuCategories> GetMenuCategoryAsync(long menuCategoriesId)
		{
			return await MenuCategoriesRepo.GetSingleMenuCategoryById(menuCategoriesId);
		}

		public async Task<List<MenuCategories>> GetAllMenuCategoriesAsync(long restaurantId, int pageNumber, int pageSize)
		{
			Menus currentMenu = await CheckMenuExistanceAsync(restaurantId);
			return await MenuCategoriesRepo.GetMenuCategoriesById(currentMenu.Id, pageNumber, pageSize);
		}

		public async Task<MenuCategories> UpdateMenuCategoryAsync(long ownerId, long restaurantId, long menuCategoryId, Dictionary<long, string> categoryName, Dictionary<long, string> categoryDescription)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			Menus currentMenu = await CheckMenuExistanceAsync(restaurantId);
			List<MenuLanguages> menuLanguages = await MenuLanguagesRepo.GetItemsByMenuId(currentMenu.Id);
			MenuCategories menuCategory = await CheckMenuCategoryExistance(menuCategoryId);
			List<Categories> categories = await CategoriesRepo.GetByMenuCategoryId(menuCategory.Id);

			foreach (var menuLang in menuLanguages)
			{
				bool checkName = categoryName.TryGetValue(menuLang.Id, out string name);
				categoryDescription.TryGetValue(menuLang.Id, out string description);

				if (!checkName) 
					name = "<< no name >>";

				Categories cat = categories.Where(x => x.MenuLanguageId == menuLang.Id).SingleOrDefault();
				if (cat == null)
				{
					cat = new Categories
					{
						CategoryName = name,
						CategoryDescription = description,
						MenuCategoryId = menuCategory.Id,
						MenuLanguageId = menuLang.Id
					};

					categories.Add(cat);
					await CategoriesRepo.AddAsync(cat, this.ModifierId);
				}
				else
				{
					cat.CategoryName = name;
					cat.CategoryDescription = description;

					await CategoriesRepo.UpdateAsync(cat, this.ModifierId);
				}
			}

			return menuCategory;
		}

		public bool RemoveMenuCategory(long ownerId, long restaurantId, long menuCategoryId)
		{
			//TODO: implement remove over menucategories, categories, items, menuItems
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

		private async Task<MenuCategories> CheckMenuCategoryExistance(long menuCategoryId)
		{
			MenuCategories menuCategory = await MenuCategoriesRepo.FindById(menuCategoryId);
			if (menuCategory == null)
				throw new Exception("Non existing entry");
			return menuCategory;
		}

		#endregion
	}
}
