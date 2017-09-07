using Restaurants.API.Models.EntityFramework;
using Restaurants.API.Models.Context;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Restaurants.API.Models.Api;
using System;

namespace Restaurants.API.Persistence.Implementation
{
	public class RestaurantRepository : CrudRepository<RestaurantObjects>
	{
		public RestaurantRepository(AppDbContext dbContext) : base(dbContext) { }

		public Task<List<RestaurantObjects>> GetRestaurantsByOwnerIdPaged(long ownerId, int pageNumber, int pageSize)
		{
			return this.DbSet
			.Where(x => x.TheRestaurantEmployers.Any(y => y.EmployerId == ownerId))
			.Include(x => x.CreatedBy)
			.Include(x => x.ModifiedBy)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.ToListAsync();
		}

		internal Task<List<SearchRestaurantResult>> GetRestaurantsPagedListFilteredByCurrentLocation(string searchTerm, float currentLatitude, float currentLongitude, int pageSize, int pageNumber)
		{
			string[] words;
			if (string.IsNullOrEmpty(searchTerm) || string.IsNullOrWhiteSpace(searchTerm))
			{
				words = new string[1];
				words[0] = "";
			}
			else
				words = System.Text.RegularExpressions.Regex.Split(searchTerm.ToLower(), @"\s+");

			var time = DateTimeOffset.UtcNow.TimeOfDay;
			var day = DateTimeOffset.UtcNow.DayOfWeek;
			var R = 6371; //Result in kilometers
			var PiOver180 = Math.PI / 180.0;

			return this._dbContext
				.RestaurantObjects
				.Where(
					x => x.TheRestaurantsOpenHours.LongCount() > 0
						 && x.TheRestaurantsOpenHours.Any(o => day >= o.StartDay && day <= o.EndDay && time >= o.StartTime && time <= o.EndTime)
						 && x.TheRestaurantLocationAddress != null && words.Any(w => (
								x.Name.ToLower().Contains(w)
								|| x.TheRestaurantLocationAddress.AdministrativeAreaLevel1 != null
									&& x.TheRestaurantLocationAddress.AdministrativeAreaLevel1.ToLower().Contains(w)
								|| x.TheRestaurantLocationAddress.AdministrativeAreaLevel2 != null
									&& x.TheRestaurantLocationAddress.AdministrativeAreaLevel2.ToLower().Contains(w)
								|| x.TheRestaurantLocationAddress.Country.ToLower().Contains(w)
								|| x.TheRestaurantLocationAddress.Locality.ToLower().Contains(w)
								|| x.TheRestaurantLocationAddress.Route.ToLower().Contains(w)
							)
						 )
				).Select(x => new SearchRestaurantResult
				{
					RestaurantId = x.Id,
					Distance = Math.Sqrt(
						Math.Pow(
							PiOver180 * (x.TheRestaurantLocationAddress.TheLocationPoint.Longitude - currentLongitude) *
							Math.Cos(PiOver180 * (x.TheRestaurantLocationAddress.TheLocationPoint.Latitude + currentLatitude) / 2.0), 2.0) +
						Math.Pow(PiOver180 * (x.TheRestaurantLocationAddress.TheLocationPoint.Latitude - currentLatitude), 2.0)) * R
				}).OrderBy(y => y.Distance)
				.ToListAsync();
		}
	}
}
