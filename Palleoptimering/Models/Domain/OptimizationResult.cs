namespace Palleoptimering.Models.Domain
{
	public class OptimizationResult
	{
		public List<PackingResult> PackedPallets { get; set; } = new();
		public List<Element> UnplacedElements { get; set; } = new();

		public void Add(PackingResult result) => PackedPallets.Add(result);
		public IEnumerator<PackingResult> GetEnumerator() => PackedPallets.GetEnumerator();
	}

}
