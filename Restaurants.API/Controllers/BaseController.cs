using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System.Linq;

namespace Restaurants.API.Controllers
{
	public abstract class BaseController : Controller
    {
		protected People GetCurrentUser()
		{
			People p;
			using (var context = new AppDbContext())
			{
				p = context.People.Include(x => x.ThePersonAsEmployer).Where(x => x.Id == 11).Single();
			}

			return p;
		}
    }
}