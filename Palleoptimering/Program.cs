using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Palleoptimering.Models;

var builder = WebApplication.CreateBuilder(args);

// Tilføj DbContext og konfiguration for Identity
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))); // Sørg for at denne connection string findes i appsettings.json

// Konfigurer Identity med brugerdefineret DbContext
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders(); // Standard token providers til ting som password reset

// Tilføj MVC og controller views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Konfigurer HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Developer exception page til fejlfindingsformål i udvikling
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Håndtering af fejl i produktion
    app.UseHsts(); // HTTP Strict Transport Security (kun i produktion)
}

app.UseHttpsRedirection(); // Force HTTPS for sikkerhed
app.UseStaticFiles(); // Gør statiske filer tilgængelige (som CSS, JS, billeder)

app.UseRouting(); // Brug routing middleware til at håndtere requests

app.UseAuthentication();  // Sørg for at authentication middleware er tilføjet
app.UseAuthorization();   // Sørg for at authorization middleware er tilføjet

// Konfigurere controller routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
