using Microsoft.EntityFrameworkCore;
namespace Palleoptimering.Models
{
	public static class SeedData
	{
		public static void EnsurePopulated(IApplicationBuilder app)
		{
			PalletDbContext context = app.ApplicationServices
				.CreateScope().ServiceProvider.GetService<PalletDbContext>();

			if (context.Database.GetPendingMigrations().Any())
			{
				context.Database.Migrate();
			}

			if (!context.Pallets.Any()) 
			{
				context.Pallets.AddRange(
					new Pallet
					{
						Column = 1,
						PalletDescription = "Noget",
						Length = 10,
						Width = 10,
						Height = 10,
						PalletGroup = 1,
						PalletType = "træ",
						Weight = 10,
						MaxHeight = 20,
						MaxWidth = 20,
						Overmeasure = 20,
						AvailableSpaces = 3,
						SpecialPallet = false,
						SpaceBetweenElements = 3,
						Active = true
					},
					new Pallet
					{
						Column = 2,
						PalletDescription = "Noget2",
						Length = 20,
						Width = 20,
						Height = 20,
						PalletGroup = 2,
						PalletType = "metal",
						Weight = 20,
						MaxHeight = 40,
						MaxWidth = 40,
						Overmeasure = 40,
						AvailableSpaces = 6,
						SpecialPallet = false,
						SpaceBetweenElements = 6,
						Active = true
					}
					);
				context.SaveChanges();
			}
		}
	}
}
