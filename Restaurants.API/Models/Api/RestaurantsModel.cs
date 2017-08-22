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
		public long? EmployerId { get; set; }
	}

	public class RestaurantCoownerModel : CoownerModel
	{
		[Required]
		public long? RestaurantId { get; set; }

	}

	public class PhoneModel {

		[Required]
		public long? RestaurantId { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage ="Phone number can't be empty string.")]
		[DataType(DataType.PhoneNumber, ErrorMessage ="It must be phone number format.")]
		[Phone]
		public string PhoneNumber { get; set; }

		[Required]
		public string PhoneDescription { get; set; }
	}

	public class LocationModel {

		[Required] public long? RestaurantId { get; set; }
		[Required] public int? Floor { get; set; }
		[Required] public string StreetNumber { get; set; }
		[Required] public string Route { get; set; }
		[Required] public string Locality { get; set; }
		[Required] public string Country { get; set; }
		[Required] public int? ZipCode { get; set; }
		[Required] public Coordinates TheLocationPoint { get; set; }

		public string AdministrativeAreaLevel2 { get; set; }
		public string AdministrativeAreaLevel1 { get; set; }
		public string GoogleLink { get; set; }
	}

	public class Coordinates {

		[Required]
		[Range(minimum: -90.0, maximum: 90.0)]
		public float? Latitude { get; set; }

		[Required]
		[Range(minimum: -180.0, maximum: 180.0)]
		public float? Longitude { get; set; }
	}

}
