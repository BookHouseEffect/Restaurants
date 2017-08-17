using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Restaurants.API.Models.Context;
using Restaurants.API.Models.EntityFramework;
using System;
using System.Collections.Generic;
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

		protected List<string> GetErrorList(ModelStateDictionary modelState)
		{
			List<ModelErrorCollection> list = modelState
				.Select(x => x.Value.Errors)
				.Where(x => x.Count > 0)
				.ToList();

			List<string> result = new List<string>();
			foreach (ModelErrorCollection modelerror in list)
			{
				foreach (var error in modelerror)
				{
					result.Add(error.ErrorMessage);
				}
			}

			return result;
		}

		protected List<string> GetErrorList(Exception ex)
		{
			List<string> list = new List<string>
			{
				ex.Message
			};
			return list;
		}
	}
}