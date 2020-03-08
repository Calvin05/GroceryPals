using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryPals.Models
{
	public class IdentitySeedData
	{
		private const string adminUser = "Admin";
		private const string adminPassword = "Secret123$";
		private const string adminRole = "Admin";

		private const string generalUser = "User";
		private const string userPassword = "Secret456$";
		private const string userRole = "User";

		public static async void EnsurePopulated(IApplicationBuilder app)
		{
			UserManager<AppUser> userManager = app.ApplicationServices
			.GetRequiredService<UserManager<AppUser>>();
			var roleManager = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();
			AppUser user = await userManager.FindByIdAsync(adminUser);
			if (user == null)
			{
				if (await roleManager.FindByNameAsync(adminRole) == null)
				{
					await roleManager.CreateAsync(new IdentityRole(adminRole));
				}
				user = new AppUser();
				user.UserName = "Admin";
				
				var result = await userManager.CreateAsync(user, adminPassword);
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, adminRole);
				}
			}
			AppUser user1 = await userManager.FindByIdAsync(generalUser);
			if (user1 == null)
			{
				if (await roleManager.FindByNameAsync(userRole) == null)
				{
					await roleManager.CreateAsync(new IdentityRole(userRole));
				}
				user1 = new AppUser();
				user1.UserName = "User";

				var result1 = await userManager.CreateAsync(user1, userPassword);
				if (result1.Succeeded)
				{
					await userManager.AddToRoleAsync(user1, "User");
				}
			}
		}
	}
}
