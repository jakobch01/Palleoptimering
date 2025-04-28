using Microsoft.EntityFrameworkCore;

namespace Palleoptimering.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<Pallet> Pallets { get; set; }
        public DbSet<PalletSettings> PalletSettings { get; set; }
    }
}
