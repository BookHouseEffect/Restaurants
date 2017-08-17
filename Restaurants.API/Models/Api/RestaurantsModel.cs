using System.ComponentModel.DataAnnotations;

namespace Restaurants.API.Models.Api
{
	public class RestaurantsModel
	{
		[Required]
		[DataType(DataType.Text, ErrorMessage = "Must be string type")]
		[MaxLength(100, ErrorMessage = "Must not exceed 100 characters")]
		public string Name { get; set; }

		[Required]
		[DataType(DataType.Text, ErrorMessage = "Must be string type")]
		[MaxLength(1000, ErrorMessage = "Must not exceed 1000 charaters")]
		public string Description { get; set; }
	}

	public class CoownerModel
	{
		[Required]
		public long EmployerId { get; set; }
	}

	public class RestaurantCoownerModel : CoownerModel
	{
		[Required]
		public long RestaurantId { get; set; }

	}

}
