using Palleoptimering.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class OrderViewModel
{
    [Required]
    public string Customer { get; set; }

    public List<Element> Elements { get; set; } = new List<Element>();
}
