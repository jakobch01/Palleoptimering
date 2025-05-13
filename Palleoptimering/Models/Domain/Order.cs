using System.ComponentModel.DataAnnotations;

namespace Palleoptimering.Models.Domain
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Customer { get; set; }

        public List<Element> Elements { get; set; } = new List<Element>();
    }
}
