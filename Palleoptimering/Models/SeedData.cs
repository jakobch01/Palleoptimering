using Microsoft.EntityFrameworkCore;

namespace Palleoptimering.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider.GetService<AppDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Pallets.Any())
            {
                context.Pallets.AddRange(
                    new Pallet
                    {
                        Description = "Noget",
                        Length = 1200,
                        Width = 800,
                        Height = 150,
                        Group = PalletGroup.Standard80,
                        Type = PalletType.Wooden,
                        Weight = 25,
                        MaxHeight = 1800,
                        MaxWeight = 1000,
                        Overhang = 10,
                        AvailableSpaces = 5,
                        IsSpecial = false,
                        SpacingBetweenElements = 10,
                        IsActive = true
                    },
                    new Pallet
                    {
                        Description = "Noget2",
                        Length = 1000,
                        Width = 1000,
                        Height = 200,
                        Group = PalletGroup.Industrial75,
                        Type = PalletType.Metal,
                        Weight = 30,
                        MaxHeight = 2000,
                        MaxWeight = 1200,
                        Overhang = 15,
                        AvailableSpaces = 4,
                        IsSpecial = true,
                        SpacingBetweenElements = 15,
                        IsActive = true
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
