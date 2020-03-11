using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using GroceryPals.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace GroceryPals
{
	public class Startup
	{
		public Startup(IConfiguration configuration) =>
			Configuration = configuration;
		public IConfiguration Configuration { get; }
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(
			Configuration["Data:NetworkEquipmentProduct:ConnectionString"]));

			services.AddDbContext<AppIdentityDbContext>(options =>
				options.UseSqlServer(
				Configuration["Data:VCNNetworkEquipmentIdentity:ConnectionString"]));
			services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<AppIdentityDbContext>()
				.AddDefaultTokenProviders();
			services.AddTransient<IProductRepository, EFProductRepository>();
			services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddTransient<IOrderRepository, EFOrderRepository>();
			services.AddMvc();
			services.AddMemoryCache();
			services.AddSession();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseDeveloperExceptionPage();
			app.UseStatusCodePages();
			app.UseStaticFiles();
			app.UseSession();
			app.UseAuthentication();
			app.UseMvc(routes => {
				routes.MapRoute(
				name: "default",
				template: "{controller=Product}/{action=Index}/{id?}");
			});

			
			SeedData.EnsurePopulated(app);
			IdentitySeedData.EnsurePopulated(app);
			
		}
	}
}
