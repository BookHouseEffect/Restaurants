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
		LanguagesRepository LanguageRepo;
		MenusRepository MenusRepo;
		MenuLanguagesRepository MenuLanguagesRepo;

		public MenuService(AppDbContext dbContext, People logedInPerson) 
			: base(dbContext, logedInPerson)
		{
			this.LanguageRepo = new LanguagesRepository(dbContext);
			this.MenusRepo = new MenusRepository(dbContext);
			this.MenuLanguagesRepo = new MenuLanguagesRepository(dbContext);
		}

		public async Task<MenuLanguages> AddMenuLanguageAsync(long ownerId, long restaurantId, long languageId)
		{
			EmployersRestaurants connection = await CheckEmployerRestaurantAsync(ownerId, restaurantId);
			Menus currentMenu = await CheckMenuExistanceAsync(restaurantId);
			Languages languageToAdd = await CheckLanguageExistance(languageId);

			CheckTheLoggedInPerson();
			MenuLanguages item = new MenuLanguages { MenuId = currentMenu.Id, LanguageId = languageToAdd.Id };
			MenuLanguagesRepo.Add(item, this.ModifierId);

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
			MenuLanguagesRepo.Update(menuLanguage, this.ModifierId);

			return menuLanguage;
		}

		private async Task<Menus> CheckMenuExistanceAsync(long restaurantId)
		{
			Menus menu = await MenusRepo.GetMenuByRestaurantId(restaurantId);
			if (menu == null){
				RestaurantObjects currentRestaurant = await CheckRestaurantExistenceAsync(restaurantId);
				menu = new Menus { RestaurantId = currentRestaurant.Id };
				MenusRepo.Add(menu, this.ModifierId);
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

		private async Task<MenuLanguages> CheckMenuLanguageExistence(long menuLanguageId){
			MenuLanguages menuLanguage = await MenuLanguagesRepo.FindById(menuLanguageId);
			if (menuLanguage == null)
				throw new Exception("Non existing entry");
			return menuLanguage;
		}
	}
}
