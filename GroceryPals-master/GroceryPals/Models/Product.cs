using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryPals.Models
{
	public class Product
	{
        [Required(ErrorMessage = "Please enter product Name")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Please enter Product ID")]
        public int ProductID { get; set; }
		
        public string Description { get; set; }


        [Required(ErrorMessage = "Please enter product Price.")]
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Please enter valid Price")]
        public double Price { get; set; }


        public string Category { get; set; }

        public string Year { get; set; }

		[Required(ErrorMessage = "Please specify the shipping status")]
		public String FreeShip { get; set; }

		
		public string Comment { get; set; }

		public string CusName { get; set; }
	}
}
