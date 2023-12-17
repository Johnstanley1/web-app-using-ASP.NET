using JAjagu_Assignment3._1.DataAccess;
using Microsoft.EntityFrameworkCore;
using Invoicing.Services;
using JAjagu_Assignment3._1.Services;

namespace JAjagu_Assignment3._1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			var connStr = builder.Configuration.GetConnectionString("PaymentManagerDb");
			builder.Services.AddDbContext<PaymentDbContext>(options => options.UseSqlServer(connStr));

			builder.Services.AddScoped<IPaymentManger, PaymentManager>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}