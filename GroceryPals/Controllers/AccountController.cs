using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GroceryPals.Models.ViewModels;
using GroceryPals.Models;
using System.Net.Mail;
using System;

namespace GroceryPals.Controllers
{
	[Authorize]
	public class AccountController : Controller
	{
		private UserManager<AppUser> userManager;
		private SignInManager<AppUser> signInManager;
		private RoleManager<IdentityRole> roleManager;
		private int VerifiticationCode = 4973;
		//public string code = "";

		//public int Code { get; set; }


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
			VerifiticationCode = new Random().Next(1000, 9999);
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
			Verification.Code = new Random().Next(1000, 9999);
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
						try
						{

							MailMessage mail = new MailMessage();
							mail.To.Add(user.Email);
							mail.From = new MailAddress("palsgrocery@gmail.com");
							mail.Subject = "Verification";
							mail.Body = "Your code is: " + Verification.Code;
							mail.IsBodyHtml = true;
							SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
							smtp.UseDefaultCredentials = false;
							smtp.Credentials = new System.Net.NetworkCredential("palsgrocery@gmail.com", "Secret123$");
							smtp.EnableSsl = true;
							smtp.Send(mail);
							return Redirect("/Account/Confirm");
						}
						catch (Exception e)
						{
							ModelState.AddModelError("", "An error occured during the process, please try again!");
							return View(loginModel);
						}

					}
				}
			}
			ModelState.AddModelError("", "Invalid name or password");
			return View(loginModel);
		}

		
		public ViewResult Confirm(string returnUrl)
		{
			return View(new AuthenticationModel
			{
				ReturnUrl = returnUrl
			});
		}

		[HttpPost]
		
		public IActionResult Confirm(AuthenticationModel model)
		{
			if(ModelState.IsValid)
			{
				System.Diagnostics.Debug.WriteLine("debug: " + Verification.Code + " and " + VerifiticationCode.ToString() + " and " );
				if(model.Verification.Equals(Verification.Code.ToString()))
				{
					return Redirect("/Product/Index");

				} else
				{
					ModelState.AddModelError("", "Invalid Code");
					return View(model);
				}

			}
			ModelState.AddModelError("", "Invalid Code");
			return View(model);
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
					UserName = model.Name,
					Email = model.Email
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

	public class Verification
	{
		public static int Code;
	}
}