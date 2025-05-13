using Palleoptimering.Models.Domain;

namespace Palleoptimering.Models.DataAccess
{
    public class EFPalletRepository : IPalletRepository
    {
        private AppDbContext _db;

        public EFPalletRepository(AppDbContext db)
        {
            _db = db;
        }

        public IQueryable<Pallet> Pallets => _db.Pallets;

        public void AddPallet(Pallet pallet)
        {
            _db.Add(pallet);
            _db.SaveChanges();
        }

        public void DeletePallet(Pallet pallet)
        {
            _db.Remove(pallet);
            _db.SaveChanges();
        }

        public void UpdatePallet(Pallet pallet)
        {
            _db.Update(pallet);
            _db.SaveChanges();
        }
    }
}
