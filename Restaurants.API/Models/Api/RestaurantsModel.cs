using System;
using System.Collections.Generic;
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
		public Int64 EmployerId { get; set; }
	}

	public class RestaurantCoownerModel : CoownerModel
	{
		[Required]
		public Int64 RestaurantId { get; set; }
	}

	public abstract class RestaurantModel
	{
		[Required]
		public Int64 RestaurantId { get; set; }
	}

	public class PhoneModel : RestaurantModel
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "Phone number can't be empty string.")]
		[DataType(DataType.PhoneNumber, ErrorMessage = "It must be phone number format.")]
		[Phone]
		public string PhoneNumber { get; set; }

		[Required]
		public string PhoneDescription { get; set; }
	}

	public class LocationModel : RestaurantModel
	{
		[Required] public Int32 Floor { get; set; }
		[Required] public string StreetNumber { get; set; }
		[Required] public string Route { get; set; }
		[Required] public string Locality { get; set; }
		[Required] public string Country { get; set; }
		[Required] public Int32 ZipCode { get; set; }
		[Required] public Coordinates TheLocationPoint { get; set; }

		public string AdministrativeAreaLevel2 { get; set; }
		public string AdministrativeAreaLevel1 { get; set; }
		public string GoogleLink { get; set; }
	}

	public class Coordinates
	{

		[Required]
		[Range(minimum: -90.0, maximum: 90.0)]
		public Single Latitude { get; set; }

		[Required]
		[Range(minimum: -180.0, maximum: 180.0)]
		public Single Longitude { get; set; }
	}

	public class LanguageModel : RestaurantModel
	{
		[Required]
		public Int64 LanguageId { get; set; }
	}

	public class CurrencyModel : RestaurantModel
	{
		[Required]
		public Int64 CurrencyId { get; set; }
	}

	public class CategoryModel : RestaurantModel
	{
		public CategoryModel()
		{
			this.CategoryName = new List<Tuple<long, string>>();
			this.CategoryDescription = new List<Tuple<long, string>>();
		}

		[Required]
		public List<Tuple<long, string>> CategoryName { get; set; }

		[Required]
		public List<Tuple<long, string>> CategoryDescription { get; set; }

		public Dictionary<long, string> GetNameDictionary()
		{
			Dictionary<long, string> dictionary = new Dictionary<long, string>();
			foreach (var item in CategoryName)
			{
				dictionary.Add(item.Item1, item.Item2);
			}
			return dictionary;
		}

		public Dictionary<long, string> GetDescriptionDictionary()
		{
			Dictionary<long, string> dictionary = new Dictionary<long, string>();
			foreach (var item in CategoryDescription)
			{
				dictionary.Add(item.Item1, item.Item2);
			}
			return dictionary;
		}
	}

}