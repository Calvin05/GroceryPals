using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GroceryPals.Models.ViewModels;
using GroceryPals.Models;

namespace GroceryPals.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private UserManager<AppUser> userManager;
		private SignInManager<AppUser> signInManager;
		private RoleManager<IdentityRole> roleManager;
		public AccountController(UserManager<AppUser> userMgr,
		SignInManager<AppUser> signInMgr, RoleManager<IdentityRole> roleMgr)
		{
			userManager = userMgr;
			signInManager = signInMgr;
			roleManager = roleMgr;
		}

		public ViewResult Index() => View(userManager.Users);
	

		[AllowAnonymous]
		public ViewResult Login(string returnUrl)
		{
			return View(new LoginModel
			{
				ReturnUrl = returnUrl
			});
		}
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginModel loginModel)
		{
			if (ModelState.IsValid)
			{
				AppUser user =
				await userManager.FindByNameAsync(loginModel.Name);
				if (user != null)
				{
					await signInManager.SignOutAsync();
					if ((await signInManager.PasswordSignInAsync(user,
					loginModel.Password, false, false)).Succeeded)
					{
						
						return Redirect(loginModel?.ReturnUrl ?? "/Product/Index");
					}
				}
			}
			ModelState.AddModelError("", "Invalid name or password");
			return View(loginModel);
		}
		public async Task<RedirectResult> Logout(string returnUrl = "/")
		{
			await signInManager.SignOutAsync();
			return Redirect(returnUrl);
		}

		[AllowAnonymous]
		public ViewResult Create() => View();
		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Create(CreateModel model)
		{
			if (ModelState.IsValid)
			{
				AppUser user = new AppUser
				{
					UserName = model.Name
				};
				IdentityResult result
				= await userManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, model.Role);
					return RedirectToAction("Login");
				}
				else
				{
					foreach (IdentityError error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
				}
			}
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(string id)
		{
			AppUser user = await userManager.FindByIdAsync(id);
			if (user != null)
			{
				IdentityResult result = await userManager.DeleteAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				else
				{
					AddErrorsFromResult(result);
				}
			}
			else
			{
				ModelState.AddModelError("", "User Not Found");
			}
			return View("Index", userManager.Users);
		}
		private void AddErrorsFromResult(IdentityResult result)
		{
			foreach (IdentityError error in result.Errors)
			{
				ModelState.AddModelError("", error.Description);
			}
		}
	} // emd of main class
}