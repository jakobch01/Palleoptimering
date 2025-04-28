namespace Palleoptimering.Models
{
    public class Placement
    {
        public Element Element { get; set; }
        public Pallet Pallet { get; set; }
        public Position Position { get; set; }
        public bool IsRotated { get; set; }
        public int Layer { get; set; }
    }
}
