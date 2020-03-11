using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryPals.Models
{
	public class EFProductRepository : IProductRepository
	{
		private ApplicationDbContext context;

		public EFProductRepository(ApplicationDbContext ctx)
		{
			context = ctx;
		}
		public IQueryable<Product> Products => context.Products;

		public void SaveProduct(Product product)
		{
			if (product.ProductID == 0)
			{
				context.Products.Add(product);
				

			}
			else
			{
				Product dbEntry = context.Products
				.FirstOrDefault(p => p.ProductID == product.ProductID);
				if (dbEntry != null)
				{
					dbEntry.Name = product.Name;
					dbEntry.Description = product.Description;
					dbEntry.Price = product.Price;
					dbEntry.Category = product.Category;
					dbEntry.Year = product.Year;
				}
			}
			context.SaveChanges();
		}

		public Product DeleteProduct(int productID)
		{
			Product dbEntry = context.Products
			.FirstOrDefault(p => p.ProductID == productID);
			if (dbEntry != null)
			{
				context.Products.Remove(dbEntry);
				context.SaveChanges();
			}
			return dbEntry;
		}

		public void AddReview(Product product)
		{
			if (product.ProductID == 0)
			{

				context.Products.Add(product);
			}
			else
			{
				Product dbEntry = context.Products
				.FirstOrDefault(p => p.ProductID == product.ProductID);
				if (dbEntry != null)
				{
					dbEntry.Name = product.Name;
					dbEntry.Description = product.Description;
					dbEntry.Price = product.Price;
					dbEntry.Category = product.Category;
					dbEntry.Year = product.Year;
					dbEntry.Comment = product.Comment;
					dbEntry.CusName = product.CusName;
				}
			}
			context.SaveChanges();
		}

        public Product DeleteReview(int productID)
        {
            Product dbEntry = context.Products
            .FirstOrDefault(p => p.ProductID == productID);
            if (dbEntry != null)
            {
                dbEntry.Comment = null;
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
	
}
