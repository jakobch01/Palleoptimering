namespace Palleoptimering.Models
{
	public interface IPalletRepository
	{
		IQueryable<Pallet> Pallets { get; }
	}
}
