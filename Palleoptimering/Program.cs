using Microsoft.AspNetCore.Identity;
using Palleoptimering.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddScoped<ElementService>();
builder.Services.AddScoped<XmlElementLoader>();

builder.Services.AddSingleton<XmlOrderLoader>();

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
.AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PalletDbContext>(opts => {
	opts.UseSqlServer(
	builder.Configuration["ConnectionStrings:DefaultConnection"]);
});

builder.Services.AddScoped<IPalletRepository, EFPalletRepository>();

builder.Services.AddDbContext<PalletSettingsDbContext>(opts =>
{
    opts.UseSqlServer(
     builder.Configuration["ConnectionStrings:DefaultConnection"]);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Element}/{action=Index}/{id?}");


app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);


app.Run();
