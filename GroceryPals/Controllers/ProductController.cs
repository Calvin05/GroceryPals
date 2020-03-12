using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GroceryPals.Models;
using GroceryPals.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GroceryPals.Controllers
{
	public class ProductController : Controller 
	{
		private IProductRepository repository;

		public int PageSize = 4;
		private readonly Expression<Func<Product, object>> lenght;



		public ProductController(IProductRepository repo)
		{
			repository = repo;
		}

        //Index
        public ViewResult Index()
        {
            return View();
        }

		// List of all items
		[AllowAnonymous]
		public ViewResult List(int productPage = 1)
			=> View(new ProductListViewModel
			{

				Products = repository.Products
				.Skip((productPage - 1) * PageSize)
				.Take(PageSize),
			
				PagingInfo = new PagingInfo
				{
					CurrentPage = productPage,
					ItemsPerPage = PageSize,
					TotalItems = repository.Products.Count()
				}
			});


        [AllowAnonymous]
        public ViewResult Search1(String searchString, int productPage = 1)
            => View(new ProductListViewModel
            {

             //   String content="";
               // Products = repository.Products.SelectMany(Product=>Product.Name)

                Products = repository.Products
                .Skip((productPage - 1) * PageSize)
                ////.OrderBy(p=> repository.Products.ToArray().Length)
                .Take(PageSize).Where(p=>p.Name.Contains(searchString)),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
            });

        [AllowAnonymous]
        public ViewResult Search(String prodCat, String searchString,String prodPrice, int productPage = 1)
           
            {
            IQueryable<string> genreQuery = from m in repository.Products
                                            orderby m.Category
                                            select m.Category;

            //   String content="";
            // Products = repository.Products.SelectMany(Product=>Product.Name)

            var products = from m in repository.Products
                           select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(prodCat))
            {
               products = products.Where(x => x.Category == prodCat);
            }

            
            if (!string.IsNullOrEmpty(prodPrice))
            {
                string[] someArray = prodPrice.Split(new char[] { ',' });
                double low = Convert.ToDouble(someArray[0]);
                double hight = Convert.ToDouble(someArray[1]);
                products = products.Where(x => x.Price >=low);
                products = products.Where(x => x.Price <= hight);
            }

            var prodVM = new ProductListViewModel
            {
                Cats = new SelectList(genreQuery.Distinct().ToList()),
                Products = products.ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
            };

            return View(prodVM);
        }

        //public ViewResult List() => View(repository.Products);

        //Form to add item

        public ViewResult Reform(int productId) =>
		View(repository.Products
		.FirstOrDefault(p => p.ProductID == productId));


		[HttpPost]
		public IActionResult ReForm(Product product)
		{
			if (ModelState.IsValid)
			{
				//Product pro = new Product();
				//pro.Name = product.Name;
				//pro.Price = product.Price;
				//pro.ProductID = product.ProductID;
				//pro.Category = product.Category ?? "<None>";
				//pro.Year = product.Year ?? "<None>";
				//pro.Description = product.Description ?? "<None>";
				//pro.FreeShip = product.FreeShip;

				repository.SaveProduct(product);
				TempData["message"] = $"{product.Name} has been saved";
				return RedirectToAction("List");

			}
			else
			{
				//there is a vadilation error

				return View(product);
			}


		}

		public ViewResult Create() => View("ReForm", new Product());

		[HttpPost]
		public IActionResult Delete(int productId)
		{
			Product deletedProduct = repository.DeleteProduct(productId);
			if (deletedProduct != null)
			{
				TempData["message"] = $"{deletedProduct.Name} was deleted";
			}
			return RedirectToAction("List");
		}


     


        public ViewResult AddComment(int productId) =>
		View(repository.Products
		.FirstOrDefault(p => p.ProductID == productId));

		[HttpPost]
		public IActionResult AddComment(Product product)
		{
			if (ModelState.IsValid)
			{
				//Product pro = new Product();
				//pro.Name = product.Name;
				//pro.Price = product.Price;
				//pro.ProductID = product.ProductID;
				//pro.Category = product.Category ?? "<None>";
				//pro.Year = product.Year ?? "<None>";
				//pro.Description = product.Description ?? "<None>";
				//pro.FreeShip = product.FreeShip;

				repository.AddReview(product);
				
				return RedirectToAction("List");

			}
			else
			{
				//there is a vadilation error

				return View(product);
			}
		}

        [HttpPost]
        public IActionResult DeleteReview(int productId)
        {
            Product deletedProduct = repository.DeleteReview(productId);
            
            return RedirectToAction("List");
        }
    }
}
