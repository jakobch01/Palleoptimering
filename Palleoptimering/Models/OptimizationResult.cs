namespace Palleoptimering.Models
{
    public class OptimizationResult
    {
        public List<Pallet> Pallets { get; set; } = new List<Pallet>();
        public List<Element> UnplaceableElements { get; set; } = new List<Element>();
        public double TotalPalletMeters { get; set; }
        public double SpaceUtilizationPercentage { get; set; }
    }
}
