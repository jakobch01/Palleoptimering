namespace Palleoptimering.Models.ViewModels
{
    public class PalletListViewModel
    {
        public IEnumerable<Pallet> Pallets { get; set; }
                = Enumerable.Empty<Pallet>();
    }
}
