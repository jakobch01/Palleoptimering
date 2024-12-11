using Microsoft.AspNetCore.Identity;
using Palleoptimering.Models;
using Microsoft.EntityFrameworkCore;
using Polly;
using Microsoft.Data.SqlClient;

try
{
	var builder = WebApplication.CreateBuilder(args);



	// Load configuration
	var configuration = builder.Configuration;

	var defaultConnection = "DefaultConnection";


	// Configure database contexts
	builder.Services.AddDbContext<AppIdentityDbContext>(options =>
		options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));

	builder.Services.AddDbContext<PalletDbContext>(options =>
		options.UseSqlServer(configuration.GetConnectionString(defaultConnection)));

	builder.Services.AddDbContext<PalletSettingsDbContext>(options =>
		options.UseSqlServer(configuration.GetConnectionString(defaultConnection)));

	// Configure identity
	builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
	{
		// Configure password settings
		options.Password.RequireDigit = false;
		options.Password.RequiredLength = 8;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequireUppercase = false;
		options.Password.RequireLowercase = true;
		options.Password.RequiredUniqueChars = 0;
	})
	.AddEntityFrameworkStores<AppIdentityDbContext>()
	.AddDefaultTokenProviders();

	// Add repositories
	builder.Services.AddScoped<IPalletRepository, EFPalletRepository>();

	// Add MVC services
	builder.Services.AddControllersWithViews();

	var app = builder.Build();

	// Configure the HTTP request pipeline
	if (app.Environment.IsDevelopment())
	{
		app.UseDeveloperExceptionPage();
	}
	else
	{
		app.UseExceptionHandler("/Home/Error");
		app.UseHsts();
	}

	app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
	app.UseStaticFiles();
	app.UseRouting();
	app.UseAuthentication();
	app.UseAuthorization();

	


	app.MapControllerRoute(
		name: "default",
		pattern: "{controller=User}/{action=Login}/{id?}");

	Policy
		.Handle<SqlException>()
		.WaitAndRetry(new[]
		{
		TimeSpan.FromSeconds(5),
		TimeSpan.FromSeconds(10),
		TimeSpan.FromSeconds(15)
		}).Execute(() =>
		{
			SeedData.EnsurePopulated(app);
		});


	using var scope = app.Services.CreateScope();
	var services = scope.ServiceProvider;
	var logger = services.GetRequiredService<ILogger<Program>>();

	try
	{
		// Ensure migrations are applied for each context
		var identityContext = services.GetRequiredService<AppIdentityDbContext>();
		await identityContext.Database.MigrateAsync();

		var palletContext = services.GetRequiredService<PalletDbContext>();
		await palletContext.Database.MigrateAsync();

		var palletSettingsContext = services.GetRequiredService<PalletSettingsDbContext>();
		await palletSettingsContext.Database.MigrateAsync();

		logger.LogInformation("Migrations applied successfully.");
	}
	catch (Exception ex)
	{
		logger.LogError($"An error occurred while applying migrations: {ex.Message}");
		throw;
	}


	app.Run();

	logger.LogInformation("Application has started successfully.");

} catch (Exception ex)
{
	Console.WriteLine($"Application startup failed: {ex.Message}");
	throw;
}

