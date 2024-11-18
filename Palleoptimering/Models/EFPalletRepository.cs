namespace Palleoptimering.Models
{
	public class EFPalletRepository : IPalletRepository
	{
		private PalletDbContext _db;

		public EFPalletRepository(PalletDbContext db) 
		{
			_db = db;
		}

		public IQueryable<Pallet> Pallets => _db.Pallets;

	}
}
