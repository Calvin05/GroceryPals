using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GroceryPals.Models
{
	public class Order
	{
		[BindNever]
		public int OrderID { get; set; }
		[BindNever]
		public ICollection<CartLine> Lines { get; set; }
		[BindNever]
		public bool Shipped { get; set; }
		[Required(ErrorMessage = "Please enter a name")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Please enter the address")]
		public string Address { get; set; }
		[Required(ErrorMessage = "Please enter a city name")]
		public string City { get; set; }
		[Required(ErrorMessage = "Please enter a state name")]
		public string Province { get; set; }
		public string PCode { get; set; }
		[Required(ErrorMessage = "Please enter a country name")]
		public string Country { get; set; }
		public bool GiftWrap { get; set; }

		public string Customer { get; set; }
	}
}
