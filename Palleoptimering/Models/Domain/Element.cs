using System.ComponentModel.DataAnnotations;

public class Element
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Elementnavn er påkrævet")]
    [Display(Name = "Elementnavn")]
    [StringLength(100, ErrorMessage = "Navnet må ikke overstige 100 tegn")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Bredde er påkrævet")]
    [Display(Name = "Bredde (mm)")]
    [Range(1, 10000, ErrorMessage = "Bredde skal være mellem 1 og 10.000 mm")]
    public int Width { get; set; }

    [Required(ErrorMessage = "Højde er påkrævet")]
    [Display(Name = "Højde (mm)")]
    [Range(1, 10000, ErrorMessage = "Højde skal være mellem 1 og 10.000 mm")]
    public int Height { get; set; }

    [Required(ErrorMessage = "Dybde er påkrævet")]
    [Display(Name = "Dybde (mm)")]
    [Range(1, 10000, ErrorMessage = "Dybde skal være mellem 1 og 10.000 mm")]
    public int Depth { get; set; }

    [Required(ErrorMessage = "Vægt er påkrævet")]
    [Display(Name = "Vægt (kg)")]
    [Range(0.01, 10000, ErrorMessage = "Vægt skal være mellem 0,01 og 10.000 kg")]
    public decimal Weight { get; set; }

    [Required(ErrorMessage = "Ordre ID er påkrævet")]
    [Display(Name = "Ordre ID")]
    public int OrderId { get; set; }


    [Display(Name = "Serie/Batch")]
    [StringLength(50, ErrorMessage = "Serie må ikke overstige 50 tegn")]
    public string Series { get; set; }

    [Required(ErrorMessage = "Rotation er påkrævet")]
    [Display(Name = "Rotation")]
    public RotationBehavior Rotation { get; set; } = RotationBehavior.NotAllowed;

    [Display(Name = "Special palle")]
    public bool RequiresSpecialPallet { get; set; } = false;

    [Display(Name = "Maks elementer pr. palle")]
    [Range(1, 100, ErrorMessage = "Antal skal være mellem 1 og 100")]
    public int? MaxElementsPerPallet { get; set; }

    [Display(Name = "Palletype")]
    [StringLength(50, ErrorMessage = "Palletype må ikke overstige 50 tegn")]
    public string PalletType { get; set; }

    [Display(Name = "Geometrisk element")]
    public bool IsGeometric { get; set; } = false;

    [Display(Name = "Optimeringstype")]
    public string OptimizationGroup { get; set; }

    // Beregnede egenskaber (ikke gemt i databasen)
    [Display(Name = "Størrelse (L×B×H)")]
    public string Dimensions => $"{Depth} × {Width} × {Height} mm";

    [Display(Name = "Grundflade")]
    public int Footprint => Width * Depth;

    [Display(Name = "Volumen")]
    public decimal Volume => (Width * Height * Depth) / 1000000m; // i m³
}

public enum RotationBehavior
{
    [Display(Name = "Må ikke roteres")]
    NotAllowed,

    [Display(Name = "Må roteres")]
    Allowed,

    [Display(Name = "Skal roteres")]
    Required
}