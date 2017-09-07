using Restaurants.API.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Restaurants.API.Models.Api;

namespace Restaurants.API.Controllers
{
	[Produces("application/json")]
    [Route("api/restaurantEmployees")]
    public class RestaurantEmployeeController : BaseController
    {
		[AllowAnonymous]
		[HttpGet("tasks/list")]
		public async Task<IActionResult> GetEmployeeTypeListAsync()
		{

			List<EmployeeTypes> list;
			try
			{
				list = await Services.GetEmployeeTypesEnumAsync();
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(list);
		}

		[AllowAnonymous]
		[HttpGet("{id}")]
		public async Task<IActionResult> GetEmployeeDetailsAsync(long id)
		{
			Employees existingEmployee;
			try
			{
				existingEmployee = await Services.GetEmployeeDetailsAsync(id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingEmployee);
		}

		[AllowAnonymous]
		[HttpGet("")]
		public async Task<IActionResult> GetEmployeeList(long restaurantId, int pageNumber=1, int pageSize = 10)
		{
			List<Employees> existingEmployeeList;
			try
			{
				existingEmployeeList = await Services.GetEmployeeListForRestaurantAsync(restaurantId, pageNumber, pageSize);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingEmployeeList);
		}

		[AllowAnonymous]
		[HttpGet("tasks")]
		public async Task<IActionResult> GetEmployeeTasks(long employeeId)
		{
			List<AssignedEmployeeTypes> existingTask;
			try
			{
				existingTask = await Services.GetEmployeeResponsibilitiesAsync(employeeId);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingTask);
		}

		[HttpPost("")]
		public async Task<IActionResult> AddEmployeeToRestaurant([FromBody] EmployeeModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			Employees newEmployee;
			try
			{
				People currentUser = GetCurrentUser();
				newEmployee = await Services.AddEmployeeToRestaurantAsync(currentUser.ThePersonAsEmployer.Id, 
						model.RestaurantId, model.EmployeeId);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(newEmployee);
		}

		[HttpPost("tasks")]
		public async Task<IActionResult> AddTasks([FromBody] TaskModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			List<AssignedEmployeeTypes> types;
			try
			{
				People currentUser = GetCurrentUser();
				types = await Services.AssignResponsibilitiesForEmployeeAsync(currentUser.ThePersonAsEmployer.Id,
						model.RestaurantId, model.EmployeeId, model.Tasks);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(types);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> TransferEmployee(long id, [FromBody] TransferEmployeeModel model)
		{
			if (!ModelState.IsValid)
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ModelState));

			Employees existingEmployee;
			try
			{
				People currentUser = GetCurrentUser();
				existingEmployee = await Services.TransferEmployeeToAnotherRestaurantAsync(currentUser.ThePersonAsEmployer.Id, model.RestaurantId, id, model.NewRestaurantId);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(existingEmployee);
		}

		[HttpDelete("{id}")]
		public IActionResult FireEmployee(long id, long restaurantId)
		{
			try
			{
				People currentUser = GetCurrentUser();
				Services.FireEmployeeOutAsync(currentUser.ThePersonAsEmployer.Id, restaurantId, id);
			}
			catch (Exception ex)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError, GetErrorList(ex));
			}

			return Ok(true);
		}

	}
}
