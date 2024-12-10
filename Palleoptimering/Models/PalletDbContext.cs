using Microsoft.EntityFrameworkCore;

namespace Palleoptimering.Models
{
	public class PalletDbContext : DbContext
	{
		public PalletDbContext(DbContextOptions<PalletDbContext> options)
			: base(options) { }

		public DbSet<Pallet> Pallets => Set<Pallet>();
		
	}
}
