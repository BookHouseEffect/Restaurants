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

		MenuItemsRepository MenuItemRepo;
		MenuItemContentRepository MenuItemContentRepo;
		MenuItemValueRepository MenuItemValueRepo;

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

			this.MenuItemRepo = new MenuItemsRepository(dbContext);
			this.MenuItemContentRepo = new MenuItemContentRepository(dbContext);
			this.MenuItemValueRepo = new MenuItemValueRepository(dbContext);
		}

		#region Menu Languages

		public async Task<MenuLanguages> AddMenuLanguageAsync(long ownerId, long restaurantId, long languageId)
		{
			CheckTheLoggedInPerson();

			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			Menus currentMenu = await CheckMenuExistanceAsync(restaurantId);
			Languages languageToAdd = await CheckLanguageExistance(languageId);

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

		public async Task<bool> RemoveMenuLanguageAsync(long ownerId, long restaurantId, long menuLanguageId)
		{
			CheckTheLoggedInPerson();

			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			MenuLanguages menuLanguage = await CheckMenuLanguageExistence(menuLanguageId);

			return await RemoveMenuLanguageByIdAsync(menuLanguage.Id);
		}

		public async Task<MenuLanguages> UpdateMenuLanguageAsync(long ownerId, long restaurantId, long menuLanguageId, long newLanguageId)
		{
			CheckTheLoggedInPerson();

			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			MenuLanguages menuLanguage = await CheckMenuLanguageExistence(menuLanguageId);
			Languages newLanguage = await CheckLanguageExistance(newLanguageId);

			menuLanguage.LanguageId = newLanguage.Id;
			menuLanguage.TheLanguage = newLanguage;
			await MenuLanguagesRepo.UpdateAsync(menuLanguage, this.ModifierId);

			return menuLanguage;
		}
		#endregion

		#region Menu Currencies

		public async Task<List<Currencies>> GetAllAvailableCurrenciesAsync()
		{
			return await CurrencyRepo.GetCurrencyList();
		}

		public async Task<MenuCurrencies> AddMenuCurrencyAsync(long ownerId, long restaurantId, long currencyId)
		{
			CheckTheLoggedInPerson();

			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			Menus currentMenu = await CheckMenuExistanceAsync(restaurantId);
			Currencies currencyToAdd = await CheckCurrencyExistance(currencyId);

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
			CheckTheLoggedInPerson();

			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			MenuCurrencies menuCurrency = await CheckMenuCurrencyExistence(menuCurrencyId);
			Currencies newCurrency = await CheckCurrencyExistance(newCurrencyId);

			menuCurrency.CurrencyId = newCurrency.Id;
			menuCurrency.TheCurrency = newCurrency;
			await MenuCurrencyRepo.UpdateAsync(menuCurrency, this.ModifierId);

			return menuCurrency;
		}

		public async Task<bool> RemoveMenuCurrencyAsync(long ownerId, long restaurantId, long menuCurrencyId)
		{
			CheckTheLoggedInPerson();

			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			MenuCurrencies menuCurrency = await CheckMenuCurrencyExistence(menuCurrencyId);

			return await RemoveMenuCurrencyByIdAsync(menuCurrency.Id);
		}
		#endregion

		#region Menu Categories

		public async Task<MenuCategories> AddMenuCategoryAsync(long ownerId, long restaurantId, Dictionary<long, string> categoryName, Dictionary<long, string> categoryDescription)
		{
			CheckTheLoggedInPerson();

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
			return await MenuCategoriesRepo.GetMenuCategoriesByMenuIdPaged(currentMenu.Id, pageNumber, pageSize);
		}

		public async Task<MenuCategories> UpdateMenuCategoryAsync(long ownerId, long restaurantId, long menuCategoryId, Dictionary<long, string> categoryName, Dictionary<long, string> categoryDescription)
		{
			CheckTheLoggedInPerson();

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

		public async Task<bool> RemoveMenuCategoryAsync(long ownerId, long restaurantId, long menuCategoryId)
		{
			CheckTheLoggedInPerson();

			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			MenuCategories menuCategory = await CheckMenuCategoryExistance(menuCategoryId);

			return await RemoveMenuCategoryByIdAsync(menuCategory.Id);
		}

		#endregion

		#region Menu Items

		public async Task<MenuItems> AddMenuItemAsync(long ownerId, long restaurantId, long menuCategoryId, Dictionary<long, string> itemName, Dictionary<long, string> itemDescription, Dictionary<long, string> itemWarnings, Dictionary<long, float> itemPrice)
		{
			CheckTheLoggedInPerson();

			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			Menus currentMenu = await CheckMenuExistanceAsync(restaurantId);
			MenuCategories category = await CheckMenuCategoryExistance(menuCategoryId);

			MenuItems menuItem = new MenuItems
			{
				MenuId = currentMenu.Id,
				MenuCategoryId = category.Id,
			};
			await MenuItemRepo.AddAsync(menuItem, this.ModifierId);

			List<MenuLanguages> menuLanguages = await MenuLanguagesRepo.GetItemsByMenuId(currentMenu.Id);
			foreach (var menuLang in menuLanguages)
			{
				bool checkName = itemName.TryGetValue(menuLang.Id, out string name);
				bool checkDescription = itemDescription.TryGetValue(menuLang.Id, out string description);
				itemWarnings.TryGetValue(menuLang.Id, out string warning);

				if (!checkName) name = "<< no name >>";
				if (!checkDescription) description = "<< no description >>";

				MenuItemContents contents = new MenuItemContents
				{
					ItemName = name,
					ItemDescription = description,
					ItemWarnings = warning,
					MenuItemId = menuItem.Id,
					MenuLanguageId = menuLang.Id
				};

				await MenuItemContentRepo.AddAsync(contents, this.ModifierId);
			}

			List<MenuCurrencies> menuCurrencies = await MenuCurrencyRepo.GetItemsByMenuId(currentMenu.Id);
			foreach (var currency in menuCurrencies)
			{
				bool checkValue = itemPrice.TryGetValue(currency.Id, out float price);

				if (!checkValue) price = 0F;

				MenuItemValues value = new MenuItemValues
				{
					Price = price,
					MenuCurrencyId = currency.Id,
					MenuItemId = menuItem.Id
				};

				await MenuItemValueRepo.AddAsync(value, this.ModifierId);
			}

			return menuItem;
		}

		public async Task<MenuItems> GetMenuItemAsync(long menuItemId)
		{
			return await MenuItemRepo.GetSingleById(menuItemId);
		}

		public async Task<List<MenuItems>> GetAllMenuItemsPagedAsync(long restaurantId, long menuCategoryId, int pageNumber, int pageSize)
		{
			Menus currentMenu = await CheckMenuExistanceAsync(restaurantId);
			return await MenuItemRepo.GetItemsByMenuIdAndMenuCategoryIdPaged(
					currentMenu.Id, menuCategoryId, pageNumber, pageSize);
		}

		public async Task<MenuItems> UpdateMenuItemAsync(long ownerId, long restaurantId, long menuItemId, Dictionary<long, string> itemName, Dictionary<long, string> itemDescription, Dictionary<long, string> itemWarnings, Dictionary<long, float> itemPrice)
		{
			CheckTheLoggedInPerson();

			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			Menus currentMenu = await CheckMenuExistanceAsync(restaurantId);
			MenuItems menuItem = await CheckMenuItemExistance(menuItemId);

			List<MenuLanguages> menuLanguages = await MenuLanguagesRepo.GetItemsByMenuId(currentMenu.Id);
			List<MenuItemContents> itemContents = await MenuItemContentRepo.GetByMenuItemId(menuItem.Id);
			foreach (var menuLang in menuLanguages)
			{
				bool checkName = itemName.TryGetValue(menuLang.Id, out string name);
				bool checkDescription = itemDescription.TryGetValue(menuLang.Id, out string description);
				itemWarnings.TryGetValue(menuLang.Id, out string warning);

				if (!checkName) name = "<< no name >>";
				if (!checkDescription) description = "<< no description >>";

				MenuItemContents contents = itemContents.Where(x => x.MenuLanguageId == menuLang.Id).SingleOrDefault();
				if (contents == null)
				{
					contents = new MenuItemContents
					{
						ItemName = name,
						ItemDescription = description,
						ItemWarnings = warning,
						MenuItemId = menuItem.Id,
						MenuLanguageId = menuLang.Id
					};
					itemContents.Add(contents);
					await MenuItemContentRepo.AddAsync(contents, this.ModifierId);
				}
				else
				{
					contents.ItemName = name;
					contents.ItemDescription = description;
					contents.ItemWarnings = warning;
					await MenuItemContentRepo.UpdateAsync(contents, this.ModifierId);
				}
			}

			List<MenuCurrencies> menuCurrencies = await MenuCurrencyRepo.GetItemsByMenuId(currentMenu.Id);
			List<MenuItemValues> itemValues = await MenuItemValueRepo.GetByMenuItemId(menuItem.Id);
			foreach (var currency in menuCurrencies)
			{
				bool checkValue = itemPrice.TryGetValue(currency.Id, out float price);

				if (!checkValue) price = 0F;

				MenuItemValues value = itemValues.Where(x => x.MenuCurrencyId == currency.Id).SingleOrDefault();
				if (value == null)
				{
					value = new MenuItemValues
					{
						Price = price,
						MenuCurrencyId = currency.Id,
						MenuItemId = menuItem.Id
					};
					itemValues.Add(value);
					await MenuItemValueRepo.AddAsync(value, this.ModifierId);
				}
				else
				{
					value.Price = price;
					await MenuItemValueRepo.UpdateAsync(value, this.ModifierId);
				}
			}

			return menuItem;
		}

		public async Task<bool> RemoveMenuItemAsync(long ownerId, long restaurantId, long menuItemId)
		{
			CheckTheLoggedInPerson();

			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			MenuItems menuItem = await CheckMenuItemExistance(menuItemId);

			return await RemoveMenuItemById(menuItem.Id);
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

		private async Task<MenuItems> CheckMenuItemExistance(long menuItemId)
		{
			MenuItems menuItem = await MenuItemRepo.FindById(menuItemId);
			if (menuItem == null)
				throw new Exception("Non existing entry");
			return menuItem;
		}

		#endregion

		#region remove private region

		private async Task<bool> RemoveItemContentByItemIdOrLanguageIdAsync(long id, bool isItemId = false, bool isLangId = false)
		{
			List<MenuItemContents> list;
			if (isItemId && !isLangId)
				list = await MenuItemContentRepo.GetByMenuItemId(id);
			else if (isLangId && !isItemId)
				list = await MenuItemContentRepo.GetByMenuLanguageId(id);
			else
				return false;

			foreach (var item in list)
				await MenuItemContentRepo.RemoveAsync(item);

			return true;
		}

		private async Task<bool> RemoveCategoryByLanguageIdOrMenuCategoryIdAsync(long id, bool isLangId = false, bool isMenuCategoryId = false){
			List<Categories> list;
			if (isLangId && !isMenuCategoryId)
				list = await CategoriesRepo.GetByMenuLanguageId(id);
			else if (isMenuCategoryId && !isLangId)
				list = await CategoriesRepo.GetByMenuCategoryId(id);
			else
				return false;

			foreach (var item in list)
				await CategoriesRepo.RemoveAsync(item);

			return true;
		}

		private async Task<bool> RemoveMenuLanguageByIdAsync(long menuLanguageId){
			MenuLanguages language = await MenuLanguagesRepo.FindById(menuLanguageId);

			bool value = await RemoveItemContentByItemIdOrLanguageIdAsync(language.Id, isLangId: true);
			value &= await RemoveCategoryByLanguageIdOrMenuCategoryIdAsync(language.Id, isLangId: true);
			await MenuLanguagesRepo.RemoveAsync(language);

			return value;
		}

		private async Task<bool> RemoveItemValueByItemIdOrCurrencyId(long id, bool isItemId = false, bool isCurrencyId = false)
		{
			List<MenuItemValues> list;
			if (isItemId && !isCurrencyId)
				list = await MenuItemValueRepo.GetByMenuItemId(id);
			else if (isCurrencyId && !isItemId)
				list = await MenuItemValueRepo.GetByMenuCurrencyId(id);
			else
				return false;

			foreach (var item in list)
				await MenuItemValueRepo.RemoveAsync(item);

			return true;
		}

		private async Task<bool> RemoveMenuCurrencyByIdAsync(long menuCurrencyId)
		{
			MenuCurrencies currency = await MenuCurrencyRepo.FindById(menuCurrencyId);

			bool value = await RemoveItemValueByItemIdOrCurrencyId(currency.Id, isCurrencyId: true);
			await MenuCurrencyRepo.RemoveAsync(currency);

			return value;
		}

		private async Task<bool> RemoveMenuItemByMenuCategoryIdAsync(long menuCategoryId){
			List<MenuItems> list = await MenuItemRepo.GetItemByCategoryId(menuCategoryId);

			bool value = true;
			foreach (var item in list)
			{
				value &= await RemoveItemContentByItemIdOrLanguageIdAsync(item.Id, isItemId: true);
				value &= await RemoveItemValueByItemIdOrCurrencyId(item.Id, isItemId: true);
				await MenuItemRepo.RemoveAsync(item);
			}

			return value;
		}

		private async Task<bool> RemoveMenuCategoryByIdAsync(long menuCategoryId){
			MenuCategories menuCategory = await MenuCategoriesRepo.FindById(menuCategoryId);

			bool value = await RemoveCategoryByLanguageIdOrMenuCategoryIdAsync(menuCategory.Id, isMenuCategoryId: true);
			value &= await RemoveMenuItemByMenuCategoryIdAsync(menuCategory.Id);
			await MenuCategoriesRepo.RemoveAsync(menuCategory);

			return value;
		}

		private async Task<bool> RemoveMenuItemById(long menuItemId){
			MenuItems menuItem = await MenuItemRepo.FindById(menuItemId);

			bool value = await RemoveItemContentByItemIdOrLanguageIdAsync(menuItem.Id, isItemId: true);
			value &= await RemoveItemValueByItemIdOrCurrencyId(menuItem.Id, isItemId: true);
			await MenuItemRepo.RemoveAsync(menuItem);

			return value;
		}

		#endregion
	}
}
