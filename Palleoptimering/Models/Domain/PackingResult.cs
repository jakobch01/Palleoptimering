namespace Palleoptimering.Models.Domain
{
    public class PackingResult
    {
        public Pallet Pallet { get; set; }
        public List<Element> Elements { get; set; } = new();
        public int CurrentHeight { get; set; }
        public decimal CurrentWeight { get; set; }
        public int CurrentLayers { get; set; } = 1;
    }
}
