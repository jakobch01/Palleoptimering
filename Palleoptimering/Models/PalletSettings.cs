using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Palleoptimering.Models
{
    public class PalletSettings
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Maks. antal lag")]
        [Range(1, 10, ErrorMessage = "Maks. lag skal være mellem 1 og 10")]
        [DefaultValue(1)]
        public int MaxLayers { get; set; } = 1;

        [DisplayName("Maks. plads (mm)")]
        [Range(100, 10000, ErrorMessage = "Maks. plads skal være mellem 100 og 10.000 mm")]
        public int MaxSpace { get; set; }

        [DisplayName("Maks. vægt for rotation (kg)")]
        [Range(0, 1000, ErrorMessage = "Vægt for rotation skal være mellem 0 og 1.000 kg")]
        public int MaxWeightAllowedToRotate { get; set; }

        [DisplayName("Højde/bredde faktor")]
        [Range(0.1, 10.0, ErrorMessage = "Faktor skal være mellem 0,1 og 10,0")]
        public double HeightWidthFactor { get; set; } = 1.5;

        [DisplayName("Kun for enkelt elementer")]
        [DefaultValue(true)]
        public bool HeightWidthFactorOnlyForSingleElements { get; set; } = true;

        [DisplayName("Maks. stablehøjde (mm)")]
        [Range(100, 5000, ErrorMessage = "Maks. højde skal være mellem 100 og 5.000 mm")]
        public int MaxStackingHeight { get; set; }

        [DisplayName("Tillæg for endeplade (mm)")]
        [Range(0, 500, ErrorMessage = "Endeplade skal være mellem 0 og 500 mm")]
        public int EndPlateAddition { get; set; }

        [DisplayName("Maks. stablevægt (kg)")]
        [Range(1, 5000, ErrorMessage = "Maks. vægt skal være mellem 1 og 5.000 kg")]
        public int MaxAllowedStackingWeight { get; set; }

        [DisplayName("Tillad rotation over maks. højde")]
        public bool AllowRotationWhenExceedingMaxHeight { get; set; }

        [DisplayName("Rækkefølge for elementplacering")]
        public RowDistributionType RowDistribution { get; set; } = RowDistributionType.LongestOutside;

        [DisplayName("Maks. overhæng (mm)")]
        [Range(0, 500, ErrorMessage = "Overhæng skal være mellem 0 og 500 mm")]
        public int MaxOverhang { get; set; } = 50;

        [DisplayName("Luft mellem elementer (mm)")]
        [Range(0, 100, ErrorMessage = "Luft skal være mellem 0 og 100 mm")]
        public int SpacingBetweenElements { get; set; } = 10;
    }

    public enum RowDistributionType
    {
        [Display(Name = "Længste elementer yderst")]
        LongestOutside,

        [Display(Name = "Balanceret vægtfordeling")]
        WeightBalanced,

        [Display(Name = "Standard rækkefølge (1,2,3,4,5)")]
        Sequential
    }
}
