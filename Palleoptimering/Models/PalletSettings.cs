namespace Palleoptimering.Models
{
    public class PalletSettings
    {
        public int Id { get; set; }
        public int MaxLayers { get; set; }
        public int MaxSpace { get; set; }
        public int WeightAllowedToTurnElement { get; set; }
        public double HeightWidthFactor { get; set; }
        public bool HeightWidthFactorOnlyForOneElement { get; set; }
        public double StackingMaxHeight { get; set; }
        public int EndPlate {  get; set; }
        public int AllowedStackingMaxWeight { get; set; }
        public bool AllowTurningOverMaxHeight { get; set; }
    }
}
