using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace GroceryPals.Models
{
	public class SeedData
	{
		public static void EnsurePopulated(IApplicationBuilder app)
		{
			ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();

			context.Database.Migrate();

			if (!context.Products.Any())
			{
				context.Products.AddRange(
					new Product
					{
						Name = "Banana",
						Description = "Organic, Healthy..",
						Category = "Fruit",
						Price = 25.99,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Banana",
						Description = "Organic, Healthy..",
						Category = "Fruit",
						Price = 25.99,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Banana",
						Description = "Organic, Healthy..",
						Category = "Fruit",
						Price = 25.99,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Banana",
						Description = "Organic, Healthy..",
						Category = "Fruit",
						Price = 25.99,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Banana",
						Description = "Organic, Healthy..",
						Category = "Fruit",
						Price = 25.99,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Banana",
						Description = "Organic, Healthy..",
						Category = "Fruit",
						Price = 25.99,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Banana",
						Description = "Organic, Healthy..",
						Category = "Fruit",
						Price = 25.99,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Banana",
						Description = "Organic, Healthy..",
						Category = "Fruit",
						Price = 25.99,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Banana",
						Description = "Organic, Healthy..",
						Category = "Fruit",
						Price = 25.99,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Banana",
						Description = "Organic, Healthy..",
						Category = "Fruit",
						Price = 25.99,
						Year = "2020",
						FreeShip = "Free Ship"
					}
					);
				context.SaveChanges();
			}
		}
	}
}
