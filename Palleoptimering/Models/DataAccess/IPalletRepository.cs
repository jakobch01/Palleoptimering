using Palleoptimering.Models.Domain;

namespace Palleoptimering.Models.DataAccess
{
    public interface IPalletRepository
    {
        IQueryable<Pallet> Pallets { get; }
        void AddPallet(Pallet pallet);
        void DeletePallet(Pallet pallet);
        void UpdatePallet(Pallet pallet);
    }
}
