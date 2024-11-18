using Palleoptimering.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<PalletDbContext>(opts => {
	opts.UseSqlServer(
	builder.Configuration["ConnectionStrings:DefaultConnection"]);
});

builder.Services.AddScoped<IPalletRepository, EFPalletRepository>();


var app = builder.Build();
//app.MapGet("/", () => "Hello World!");
app.UseStaticFiles();
app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();
