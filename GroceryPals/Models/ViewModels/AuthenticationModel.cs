using System.ComponentModel.DataAnnotations;

namespace GroceryPals.Models.ViewModels
{
	public class AuthenticationModel
	{
		[Required]
		
		public string Verification { get; set; }

		//[Required]
		//public string Email { get; set; }

		public string ReturnUrl { get; set; } = "/";
	}
}
