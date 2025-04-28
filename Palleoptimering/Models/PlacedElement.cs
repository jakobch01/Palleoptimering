namespace Palleoptimering.Models
{
    public class PlacedElement
    {
        public Element Element { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Layer { get; set; }
        public bool Rotated { get; set; }
    }
}
