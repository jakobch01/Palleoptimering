namespace Palleoptimering.Models
{
    public class PalletPacking
    {
        public int PalletId { get; set; }
        public List<Element> Elements { get; set; } = new();
        public List<PackedElement> PackedElements { get; set; } = new(); 
    }

}
