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
						Price = 2.99,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Mango",
						Description = "Organic, Healthy..",
						Category = "Fruit",
						Price = 3.50,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Pineapple",
						Description = "Organic, Healthy..",
						Category = "Fruit",
						Price = 6.99,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Lemons",
						Description = "Organic, Healthy..",
						Category = "Fruit",
						Price = 1.49,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Skinless Chicken Breast",
						Description = "Boneless, raised without antibiotics",
						Category = "Meat",
						Price = 16.99,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Sirloin Club Steak",
						Description = "2 Steaks per tray",
						Category = "Meat",
						Price = 10.09,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Sliced Pork Belly",
						Description = "Organic, Healthy..",
						Category = "Meat",
						Price = 4.85,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Lean Ground Beef",
						Description = "0.5kg 96% lean beef",
						Category = "Meat",
						Price = 8.99,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "Lays Classic Chips",
						Description = "Family size",
						Category = "Grocery",
						Price = 2.99,
						Year = "2020",
						FreeShip = "Free Ship"
					},
					new Product
					{
						Name = "2% Plain Greek Yogurt",
						Description = "Healthy, plain probiotic",
						Category = "Dairy",
						Price = 4.99,
						Year = "2020",
						FreeShip = "Free Ship"
					},
                    new Product
                    {
                        Name = "Honey Nut Cheerios",
                        Description = "Cereal",
                        Category = "Grocery",
                        Price = 5.49,
                        Year = "2020",
                        FreeShip = "Free Ship"
                    },
                    new Product
                    {
                        Name = "Frozen Thin Crust Pizza",
                        Description = "Margherita, delissio",
                        Category = "Grocery",
                        Price = 2.99,
                        Year = "2020",
                        FreeShip = "Free Ship"
                    },

                    new Product
                    {
                        Name = "Tostada",
                        Description = "Mexican Corn Chip",
                        Category = "Snacks",
                        Price = 6.99,
                        Year = "2021",
                        FreeShip = "No Free Ship"
                    },

                    new Product
                    {
                        Name = "Grammy's Chicken Legs",
                        Description = "Fried Chicken Legs",
                        Category = "Meat",
                        Price = 10.99,
                        Year = "2020",
                        FreeShip = "Free Shipping"
                    },

                    new Product
                    {
                        Name = "Celary",
                        Description = "Healthy Celary Slices",
                        Category = "Vegetable",
                        Price = 4.99,
                        Year = "2022",
                        FreeShip = "Free Shipping"
                    },

                    new Product
                    {
                        Name = "Breyer's Ice Cream",
                        Description = "Chocolate Ice Cream",
                        Category = "Frozen",
                        Price = 7.99,
                        Year = "2020",
                        FreeShip = "Free Shipping"
                    },

                    new Product
                    {
                        Name = "X-treme Carrots",
                        Description = "Sliced Carrot Sticks",
                        Category = "Vegetable",
                        Price = 6.99,
                        Year = "2021",
                        FreeShip = "Free Shipping"
                    }
                    );
				context.SaveChanges();
			}
		}
	}
}
