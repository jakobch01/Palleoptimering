namespace Palleoptimering.Models
{
    public class ElementService
    {
        public List<Element> SortByLength(List<Element> elements)
        {
            return elements.OrderByDescending(e => e.Length).ToList();
        }

        public List<Element> SortByWeight(List<Element> elements)
        {
            return elements.OrderBy(e => e.IsHeavyElement()).ToList();
        }

        public List<IGrouping<string, Element>> GroupElements(List<Element> elements)
        {
            return elements.GroupBy(e => e.Group).ToList();
        }
    }
}
