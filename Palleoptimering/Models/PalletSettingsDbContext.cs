using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Palleoptimering.Models
{
    public class PalletSettingsDbContext: DbContext
    {
        public PalletSettingsDbContext(DbContextOptions<PalletSettingsDbContext> options) : base(options) 
        { 
        }   

        public DbSet<PalletSettings> PalletSettings { get; set; }
    }
}
