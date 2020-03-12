using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GroceryPals.Models.ViewModels
{
	public class ProductListViewModel
	{
		public IEnumerable<Product> Products { get; set; }
		public PagingInfo PagingInfo { get; set; }

        public string SearchString { get; set; }

        public SelectList Cats { get; set; }
        public string ProdCat { get; set; }


        public SelectList Prices { get; set; }
        public string ProdPrice { get; set; }

    }
}
