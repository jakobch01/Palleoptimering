using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Palleoptimering.Models
{
    public class Pallet
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Pallebeskrivelse er påkrævet")]
        [DisplayName("Beskrivelse")]
        [StringLength(100, ErrorMessage = "Beskrivelsen må ikke overstige 100 tegn")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Bredde er påkrævet")]
        [DisplayName("Bredde (mm)")]
        [Range(100, 5000, ErrorMessage = "Bredde skal være mellem 100 og 5000 mm")]
        public int Width { get; set; }

        [Required(ErrorMessage = "Højde er påkrævet")]
        [DisplayName("Højde (mm)")]
        [Range(50, 1000, ErrorMessage = "Højde skal være mellem 50 og 1000 mm")]
        public int Height { get; set; }

        [Required(ErrorMessage = "Længde er påkrævet")]
        [DisplayName("Længde (mm)")]
        [Range(100, 5000, ErrorMessage = "Længde skal være mellem 100 og 5000 mm")]
        public int Length { get; set; }

        [DisplayName("Pallegruppe")]
        public PalletGroup Group { get; set; }

        [Required(ErrorMessage = "Palletype er påkrævet")]
        [DisplayName("Palletype")]
        public PalletType Type { get; set; }

        [Required(ErrorMessage = "Vægt er påkrævet")]
        [DisplayName("Vægt (kg)")]
        [Range(0, 500, ErrorMessage = "Vægt skal være mellem 0 og 500 kg")]
        public decimal Weight { get; set; }

        [DisplayName("Maks. højde (mm)")]
        [Range(100, 5000, ErrorMessage = "Maks. højde skal være mellem 100 og 5000 mm")]
        public int? MaxHeight { get; set; }

        [DisplayName("Maks. vægt (kg)")]
        [Range(1, 10000, ErrorMessage = "Maks. vægt skal være mellem 1 og 10000 kg")]
        public int? MaxWeight { get; set; }

        [DisplayName("Overmål (mm)")]
        [Range(0, 500, ErrorMessage = "Overmål skal være mellem 0 og 500 mm")]
        [DefaultValue(0)]
        public int Overhang { get; set; } = 0;

        [DisplayName("Antal pladser")]
        [Range(1, 100, ErrorMessage = "Antal pladser skal være mellem 1 og 100")]
        public int? AvailableSpaces { get; set; }

        [DisplayName("Special palle")]
        [DefaultValue(false)]
        public bool IsSpecial { get; set; } = false;

        [DisplayName("Luft mellem elementer (mm)")]
        [Range(0, 100, ErrorMessage = "Luft mellem elementer skal være mellem 0 og 100 mm")]
        [DefaultValue(10)]
        public int SpacingBetweenElements { get; set; } = 10;

        [DisplayName("Aktiv")]
        [DefaultValue(true)]
        public bool IsActive { get; set; } = true;

        // Beregnede egenskaber (ikke gemt i databasen)
        [DisplayName("Grundflade")]
        public int Area => Width * Length;

        [DisplayName("Volumen")]
        public decimal Volume => (Width * Length * Height) / 1000000m; // i m³

        [DisplayName("Maks. elementhøjde")]
        public int MaxElementHeight => MaxHeight.HasValue ? MaxHeight.Value - Height : int.MaxValue;
    }

    public enum PalletGroup
    {
        [Display(Name = "Standard (80'er)")]
        Standard80,

        [Display(Name = "Euro (120'er)")]
        Euro120,

        [Display(Name = "Industri (75'er)")]
        Industrial75,

        [Display(Name = "Special")]
        Special
    }

    public enum PalletType
    {
        [Display(Name = "Træpalle")]
        Wooden,

        [Display(Name = "Plastpalle")]
        Plastic,

        [Display(Name = "Metalpalle")]
        Metal,

        [Display(Name = "Glasstelspalle")]
        GlassFrame,

        [Display(Name = "Specialpalle")]
        Special
    }
}
